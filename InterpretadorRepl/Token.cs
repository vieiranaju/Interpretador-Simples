public enum TipoToken {
    NUM,
    ID,
    MAIS,
    MENOS,
    MULT,
    DIV,
    IGUAL,
    ABREPAR,
    FECHAPAR,
    EOF
}

public class Token {
    public TipoToken Tipo { get; }
    public string Texto { get; }

    public Token(TipoToken tipo, string texto) {
        Tipo = tipo;
        Texto = texto;
    }

    public override string ToString() => $"Token({Tipo}, \"{Texto}\")";
}
