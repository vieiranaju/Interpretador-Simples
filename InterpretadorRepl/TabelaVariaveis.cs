using System.Collections.Generic;

public class TabelaVariaveis {
    private Dictionary<string, int> tabela = new();

    public void Definir(string nome, int valor) => tabela[nome] = valor;
    public int Obter(string nome) => tabela[nome];
    public bool Contem(string nome) => tabela.ContainsKey(nome);

    public void MostrarTodas() {
        foreach (var par in tabela)
            Console.WriteLine($"{par.Key} = {par.Value}");
    }
}
