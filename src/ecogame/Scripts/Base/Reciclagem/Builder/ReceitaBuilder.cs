namespace EcoGame;

public interface ReceitaBuilder
{
    bool EhCompativel(Item i);
    void AdicionarIngrediente(Item i);
    bool ValidarIngredientes();
    Item Construir();
}