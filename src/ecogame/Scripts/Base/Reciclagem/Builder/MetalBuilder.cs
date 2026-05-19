using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class MetalBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();
    private const int METAL_NECESSARIO = 3;

    public bool EhCompativel(Item i) => i is Lixo && i.GetMaterial() == MaterialBase.METAL;

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes() => _ingredientes.Sum(item => item.GetQuantidade()) >= METAL_NECESSARIO;

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        var resultado = new Reciclado("Bloco de Metal", 1, MaterialBase.METAL, 15);
        Reset();
        return resultado;
    }

    public void Reset() => _ingredientes.Clear();
}