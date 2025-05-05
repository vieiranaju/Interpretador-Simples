public class Parser {
    private AnalisadorLexico lexico;
    private Token atual;

    public Parser(AnalisadorLexico lexico) {
        this.lexico = lexico;
        atual = lexico.ProximoToken();
    }

    private void Consumir(TipoToken esperado) {
        if (atual.Tipo == esperado)
            atual = lexico.ProximoToken();
        else
            throw new Exception($"Esperado {esperado}, mas veio {atual.Tipo}");
    }

    public Expressao Comando() {
        if (atual.Tipo == TipoToken.ID) {
            Token id = atual;
            Token proximo = lexico.ProximoToken();

            if (proximo.Tipo == TipoToken.IGUAL) {
                atual = lexico.ProximoToken();
                var valor = Expressao();
                return new Atribuicao(id.Texto, valor);
            } else {
                string entrada2 = id.Texto + " " + proximo.Texto;
                while (proximo.Tipo != TipoToken.EOF) {
                    proximo = lexico.ProximoToken();
                    entrada2 += " " + proximo.Texto;
                }

                var novoLexico = new AnalisadorLexico(entrada2);
                var novoParser = new Parser(novoLexico);
                return novoParser.Expressao();
            }
        }

        return Expressao();
    }


    private Expressao Expressao() {
        var expr = Termo();
        while (atual.Tipo == TipoToken.MAIS || atual.Tipo == TipoToken.MENOS) {
            string op = atual.Texto;
            Consumir(atual.Tipo);
            expr = new Operador(op, expr, Termo());
        }
        return expr;
    }

    private Expressao Termo() {
        var termo = Fator();
        while (atual.Tipo == TipoToken.MULT || atual.Tipo == TipoToken.DIV) {
            string op = atual.Texto;
            Consumir(atual.Tipo);
            termo = new Operador(op, termo, Fator());
        }
        return termo;
    }

    private Expressao Fator() {
        if (atual.Tipo == TipoToken.NUM) {
            int valor = int.Parse(atual.Texto);
            Consumir(TipoToken.NUM);
            return new Numero(valor);
        } else if (atual.Tipo == TipoToken.ID) {
            string nome = atual.Texto;
            Consumir(TipoToken.ID);
            return new Variavel(nome);
        } else if (atual.Tipo == TipoToken.ABREPAR) {
            Consumir(TipoToken.ABREPAR);
            var expr = Expressao();
            Consumir(TipoToken.FECHAPAR);
            return expr;
        } else {
            throw new Exception("Expressão inválida");
        }
    }
}
