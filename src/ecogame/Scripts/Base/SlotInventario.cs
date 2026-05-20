using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class SlotInventario : Item
{
    private List<Item> _itens = new List<Item>();
    private int _limiteStack = 12;
    private int _indice;

    public SlotInventario(int indice) : base("Slot", 0, MaterialBase.OUTROS, 0)
    {
        _indice = indice;
    }

    public override string GetNome()
    {
        return _itens.Count > 0 ? _itens[0].GetNome() : "Vazio";
    }

    public override MaterialBase GetMaterial()
    {
        return _itens.Count > 0 ? _itens[0].GetMaterial() : MaterialBase.OUTROS;
    }

    public override int GetPontos()
    {
        return _itens.Sum(item => item.GetPontos());
    }

    public override int GetQuantidade()
    {
        return _itens.Sum(item => item.GetQuantidade());
    }

    public bool AdicionarItem(Item item)
    {
        if (EstaLotado()) return false;
        if (_itens.Count > 0 && _itens[0].GetNome() != item.GetNome())
            return false;
        _itens.Add(item);
        return true;
    }

    public bool RemoverItem(int quantidade)
    {
        if (quantidade > _itens.Count) return false;
        _itens.RemoveRange(_itens.Count - quantidade, quantidade);
        return true;
    }

    public bool EstaVazio() => _itens.Count == 0;
    public bool EstaLotado() => _itens.Count >= _limiteStack;
    public List<Item> GetItens() => [.. _itens];
    public int GetIndice() => _indice;
}