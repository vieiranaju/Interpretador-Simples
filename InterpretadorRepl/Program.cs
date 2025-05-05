using System;

class Program {
    static void Main() {
        var variaveis = new TabelaVariaveis();

        while (true) {
            Console.Write(">>> ");
            string? linha = Console.ReadLine();
            if (linha == null || linha.Trim().ToLower() == "sair") break;
            if (linha == "variaveis") {
                variaveis.MostrarTodas();
                continue;
            }

            try {
                var lexico = new AnalisadorLexico(linha);
                var parser = new Parser(lexico);
                var comando = parser.Comando();
                int resultado = comando.Avaliar(variaveis);
                Console.WriteLine(resultado);
            } catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}

