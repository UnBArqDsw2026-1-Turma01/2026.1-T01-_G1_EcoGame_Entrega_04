using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class SlotInventario : ItemAgrupavel
{
    private List<ItemAgrupavel> _itens = new List<ItemAgrupavel>();
    private int _limiteStack = 12;
    private int _indice;

    public SlotInventario(int indice)
    {
        _indice = indice;
    }

    public string GetNome()
    {
        return _itens.Count > 0 ? _itens[0].GetNome() : "Vazio";
    }

    public MaterialBase GetMaterial()
    {
        return _itens.Count > 0 ? _itens[0].GetMaterial() : MaterialBase.OUTROS;
    }

    public int GetPontos()
    {
        return _itens.Sum(item => item.GetPontos());
    }

    public int GetQuantidade()
    {
        return _itens.Sum(item => item.GetQuantidade());
    }

    public bool AdicionarItem(ItemAgrupavel item)
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
    public List<ItemAgrupavel> GetItens() => new List<ItemAgrupavel>(_itens);
    public int GetIndice() => _indice;
}