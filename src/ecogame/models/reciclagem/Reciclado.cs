namespace EcoGame;

// Reciclado é o produto final da MaquinaReciclagem. Pontos via GetPontos() herdado de Item.
public partial class Reciclado : Item
{
    public Reciclado(string nome, int quantidade, MaterialBase material)
        : base(nome, quantidade, material) { }
}
