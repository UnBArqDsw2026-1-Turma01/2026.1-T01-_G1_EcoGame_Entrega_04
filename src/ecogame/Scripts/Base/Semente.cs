namespace EcoGame;

public partial class Semente : Item 
{
    // O construtor de Semente repassa os dados para o construtor do Item
    public Semente(string nome, int quantidade, MaterialBase material, int pontos) 
        : base(nome, quantidade, material, pontos) { }
}