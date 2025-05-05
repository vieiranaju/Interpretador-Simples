public abstract class Expressao {
    public abstract int Avaliar(TabelaVariaveis variaveis);
}

public class Numero : Expressao {
    public int Valor;
    public Numero(int valor) { Valor = valor; }
    public override int Avaliar(TabelaVariaveis variaveis) => Valor;
}

public class Variavel : Expressao {
    public string Nome;
    public Variavel(string nome) { Nome = nome; }

    public override int Avaliar(TabelaVariaveis variaveis) {
        if (!variaveis.Contem(Nome))
            throw new Exception($"Variável '{Nome}' não definida.");
        return variaveis.Obter(Nome);
    }
}

public class Operador : Expressao {
    public string Op;
    public Expressao Esq, Dir;

    public Operador(string op, Expressao esq, Expressao dir) {
        Op = op;
        Esq = esq;
        Dir = dir;
    }

    public override int Avaliar(TabelaVariaveis variaveis) {
        int e = Esq.Avaliar(variaveis);
        int d = Dir.Avaliar(variaveis);
        return Op switch {
            "+" => e + d,
            "-" => e - d,
            "*" => e * d,
            "/" => d != 0 ? e / d : throw new DivideByZeroException(),
            _ => throw new Exception($"Operador desconhecido: {Op}")
        };
    }
}

public class Atribuicao : Expressao {
    public string Nome;
    public Expressao Valor;

    public Atribuicao(string nome, Expressao valor) {
        Nome = nome;
        Valor = valor;
    }

    public override int Avaliar(TabelaVariaveis variaveis) {
        int v = Valor.Avaliar(variaveis);
        variaveis.Definir(Nome, v);
        return v;
    }
}
