using System;
using System.Text;

public class AnalisadorLexico {
    private readonly string entrada;
    private int pos = 0;

    public AnalisadorLexico(string entrada) {
        this.entrada = entrada;
    }

    public char ProximoChar() => pos < entrada.Length ? entrada[pos] : '\0';
    private void Avancar() => pos++;

    public Token ProximoToken() {
        while (char.IsWhiteSpace(ProximoChar())) Avancar();

        char atual = ProximoChar();

        if (char.IsDigit(atual)) {
            StringBuilder numero = new();
            while (char.IsDigit(ProximoChar())) {
                numero.Append(ProximoChar());
                Avancar();
            }
            return new Token(TipoToken.NUM, numero.ToString());
        }

        if (char.IsLetter(atual)) {
            StringBuilder id = new();
            while (char.IsLetterOrDigit(ProximoChar())) {
                id.Append(ProximoChar());
                Avancar();
            }
            return new Token(TipoToken.ID, id.ToString());
        }

        return atual switch {
            '+' => RetornarSimbolo(TipoToken.MAIS),
            '-' => RetornarSimbolo(TipoToken.MENOS),
            '*' => RetornarSimbolo(TipoToken.MULT),
            '/' => RetornarSimbolo(TipoToken.DIV),
            '=' => RetornarSimbolo(TipoToken.IGUAL),
            '(' => RetornarSimbolo(TipoToken.ABREPAR),
            ')' => RetornarSimbolo(TipoToken.FECHAPAR),
            '\0' => new Token(TipoToken.EOF, ""),
            _ => throw new Exception($"Caractere inválido: {atual}")
        };
    }

    private Token RetornarSimbolo(TipoToken tipo) {
        char c = ProximoChar();
        Avancar();
        return new Token(tipo, c.ToString());
    }
}
