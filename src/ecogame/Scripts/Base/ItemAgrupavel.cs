namespace EcoGame;

public interface ItemAgrupavel
{
    string GetNome();
    MaterialBase GetMaterial();
    int GetQuantidade();
    int GetPontos();
}