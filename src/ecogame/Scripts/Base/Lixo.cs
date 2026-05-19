namespace EcoGame;

public partial class Lixo : Item 
{
    // O construtor do Lixo repassa os dados para o construtor do Item
    public Lixo(string nome, int quantidade, MaterialBase material) 
        : base(nome, quantidade, material, 0) { } //lixo não tem pontos
}