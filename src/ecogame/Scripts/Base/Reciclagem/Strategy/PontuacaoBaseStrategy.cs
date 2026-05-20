namespace EcoGame;

public class PontuacaoBaseStrategy : PontuacaoStrategy
{
    public int CalcularPontos(Item item) => item.GetPontos();
    public string GetNomeEstrategia() => "Base";
}