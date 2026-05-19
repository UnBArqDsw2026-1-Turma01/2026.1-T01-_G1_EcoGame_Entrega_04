namespace EcoGame;

public partial class Ferramenta : Item 
{
    // O construtor de Ferramenta repassa os dados para o construtor do Item
    public Ferramenta(string nome, int quantidade, MaterialBase material, int pontos) 
        : base(nome, quantidade, material, pontos) { }
}