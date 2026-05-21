namespace EcoGame;

public class PontuacaoRaridadeStrategy : PontuacaoStrategy
{
    public int CalcularPontos(Item item)
    {
        float multiplicador = item.GetMaterial() switch
        {
            MaterialBase.VIDRO       => 2.0f,
            MaterialBase.METAL       => 1.75f,
            MaterialBase.GARRAFA_PET => 1.25f,
            MaterialBase.ORGANICO    => 0.75f,
            _                        => 1.0f
        };

        if(item is BaldeComposite)
        {
            multiplicador += 0.5f;
        }
        else if(item is Ferramenta)
        {
            multiplicador = 0.1f;
        }
        return (int)(item.GetPontos() * multiplicador);
    }

    public string GetNomeEstrategia() => "Raridade";
}