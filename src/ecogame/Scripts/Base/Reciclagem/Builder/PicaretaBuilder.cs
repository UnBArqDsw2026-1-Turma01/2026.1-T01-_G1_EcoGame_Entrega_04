using System.Collections.Generic;
using System.Linq;

namespace EcoGame;
public class PicaretaBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();

    public bool EhCompativel(Item i)
    {
        // Aceita se for Reciclado E for ou Metal ou Organico
        return i is Reciclado && (i.GetMaterial() == MaterialBase.METAL || i.GetMaterial() == MaterialBase.ORGANICO); //ORGANICO VAI SER MADEIRA FUTURAMENTE
    }

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes()
    {
        // Filtra e soma quanto temos de cada ingrediente
        int totalMetal = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.METAL).Sum(i => i.GetQuantidade());
        int totalOrganico = _ingredientes.Where(i => i.GetMaterial() == MaterialBase.ORGANICO).Sum(i => i.GetQuantidade());

        // A receita: 2 Metais E 2 Organicos
        return totalMetal >= 2 && totalOrganico >= 1;
    }

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        var resultado = new Ferramenta("Picareta", 1, MaterialBase.METAL, 60);
        Reset();
        return resultado;
    }

    public void Reset() => _ingredientes.Clear();
}