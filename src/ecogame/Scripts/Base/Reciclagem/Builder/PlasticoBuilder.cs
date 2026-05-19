using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class PlasticoBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();
    private const int PLASTICO_NECESSARIO = 3;

    public bool EhCompativel(Item i) => i is Lixo && i.GetMaterial() == MaterialBase.PLASTICO;

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes() => _ingredientes.Sum(item => item.GetQuantidade()) >= PLASTICO_NECESSARIO;

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        var resultado = new Reciclado("Bloco de Plastico", 1, MaterialBase.PLASTICO, 15);
        Reset();
        return resultado;
    }

    public void Reset() => _ingredientes.Clear();
}