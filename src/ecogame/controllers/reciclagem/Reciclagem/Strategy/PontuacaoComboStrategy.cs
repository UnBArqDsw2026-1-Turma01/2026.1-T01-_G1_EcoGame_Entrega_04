namespace EcoGame;

public class PontuacaoComboStrategy : PontuacaoStrategy
{
    public int CalcularPontos(Item item)
    {
        float bonus = item.GetQuantidade() switch
        {
            >= 10 => 1.5f,
            >= 6  => 1.25f,
            >= 3  => 1.10f,
            _     => 1.0f
        };
        return (int)(item.GetPontos() * bonus);
    }

    public string GetNomeEstrategia() => "Combo";
}