using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class BaldeBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();

    public bool EhCompativel(Item i)
    {
        return i is Reciclado && (i.GetMaterial() == MaterialBase.METAL || i.GetMaterial() == MaterialBase.GARRAFA_PET);
    }

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes()
    {
        int totalMetal = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.METAL).Sum(i => i.GetQuantidade());
        int totalPlastico = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.GARRAFA_PET).Sum(i => i.GetQuantidade());
        return totalMetal >= 2 && totalPlastico >= 1;
    }

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;
        var novoBalde = new BaldeComposite("Balde de Ferro", 1, MaterialBase.METAL, volumeMax: 5);
        Reset();
        return novoBalde;
    }

    public void Reset() => _ingredientes.Clear();
}
