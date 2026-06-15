namespace EcoGame;

public interface PontuacaoStrategy
{
    int CalcularPontos(Item item);
    string GetNomeEstrategia();
}