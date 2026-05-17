using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class OrganicoBuilder : ReceitaBuilder
{
    private List<Item> _itensNoBalde = new List<Item>();

    public bool EhCompativel(Item i)
    {
        return i is Lixo && i.GetMaterial() == MaterialBase.ORGANICO;
    }

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _itensNoBalde.Add(i);
    }

    public bool ValidarIngredientes()
    {
        // soma a quantidades de todos os itens no balde
        int totalOrganico = 0;
        foreach (var item in _itensNoBalde)
        {
            totalOrganico += item.GetQuantidade();
        }
        
        return totalOrganico >= 3; // Precisa de 3 unidades de plástico no total 
    }

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        _itensNoBalde.Clear();
        
        int pontosDeReceita = 15;
        // Retorna um novo Reciclado
        return new Reciclado("Bloco de Adubo", 1, MaterialBase.ORGANICO, pontosDeReceita);
    }
}