using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class VidroBuilder : ReceitaBuilder
{
    private List<Item> _ingredientes = new List<Item>();
    private const int VIDRO_NECESSARIO = 3;

    public bool EhCompativel(Item i) => i is Lixo && i.GetMaterial() == MaterialBase.VIDRO;

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _ingredientes.Add(i);
    }

    public bool ValidarIngredientes() => _ingredientes.Sum(item => item.GetQuantidade()) >= VIDRO_NECESSARIO;

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        var resultado = new Reciclado("Bloco de Vidro", 1, MaterialBase.VIDRO, 15);
        Reset();
        return resultado;
    }

    public void Reset() => _ingredientes.Clear();
}