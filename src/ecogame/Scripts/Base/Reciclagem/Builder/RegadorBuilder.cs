using System.Collections.Generic;
using System.Linq;

namespace EcoGame;
public class RegadorBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();

    public bool EhCompativel(Item i)
    {
        // Aceita se for Reciclado E for ou Metal ou Plastico
        return i is Reciclado && (i.GetMaterial() == MaterialBase.METAL || i.GetMaterial() == MaterialBase.GARRAFA_PET); //Plastico VAI SER MADEIRA FUTURAMENTE
    }

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes()
    {
        // Filtra e soma quanto temos de cada ingrediente
        int totalMetal = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.METAL).Sum(i => i.GetQuantidade());
        int totalPlastico = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.GARRAFA_PET).Sum(i => i.GetQuantidade());

        // A receita: 2 Metais E 2 Plásticos
        return totalMetal >= 1 && totalPlastico >= 2;
    }

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        var resultado = new Ferramenta("Regador", 1, MaterialBase.GARRAFA_PET, 30);
        Reset();
        return resultado;
    }

    public void Reset() => _ingredientes.Clear();
}