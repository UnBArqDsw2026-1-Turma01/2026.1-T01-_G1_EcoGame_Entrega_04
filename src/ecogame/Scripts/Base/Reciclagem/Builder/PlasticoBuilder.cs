using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class PlasticoBuilder : ReceitaBuilder
{
    private List<Item> _itensNoBalde = new List<Item>();

    public bool EhCompativel(Item i)
    {
        return i is Lixo && i.GetMaterial() == MaterialBase.PLASTICO;
    }

    public void AdicionarIngrediente(Item i)
    {
        if (EhCompativel(i)) _itensNoBalde.Add(i);
    }

    public bool ValidarIngredientes()
    {
        // soma a quantidades de todos os itens no balde
        int totalPlastico = 0;
        foreach (var item in _itensNoBalde)
        {
            totalPlastico += item.GetQuantidade();
        }
        
        return totalPlastico >= 3; // Precisa de 3 unidades de plástico no total 
    }

    public Item Construir()
    {
        if (!ValidarIngredientes()) return null;

        _itensNoBalde.Clear();
        //pontos da receita de criar bloco de plástico
        
         int pontosDeReceita = 15;
        // Retorna um novo Reciclado
        return new Reciclado("Bloco de Plástico", 1, MaterialBase.PLASTICO, pontosDeReceita);
    }
}