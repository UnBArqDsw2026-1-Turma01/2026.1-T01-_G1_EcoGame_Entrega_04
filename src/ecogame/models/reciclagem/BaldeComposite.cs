using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class BaldeComposite : Ferramenta
{
    private int _volumeMax;
    private List<Item> _itens = new List<Item>();

    public BaldeComposite(string nome, int quantidade, MaterialBase material, int volumeMax)
        : base(nome, quantidade, material)
    {
        _volumeMax = volumeMax;
    }

    public override string GetNome()
    {
        if (_itens.Count == 0) return base.GetNome() + " (Vazio)";
        return $"{base.GetNome()} com {_itens.Count} itens dentro";
    }

    public override int GetPontos() => base.GetPontos() + _itens.Sum(i => i.GetPontos());
    public override int GetQuantidade() => 1;

    public bool AdicionarItem(Item item)
    {
        if (GetVolumeOcupado() + 1 > _volumeMax) return false;
        _itens.Add(item);
        return true;
    }

    public void RemoverItem(Item item)
    {
        if (_itens.Contains(item)) _itens.Remove(item);
    }

    public int GetVolumeOcupado() => _itens.Count;
    public void LimparBalde() => _itens.Clear();
    public int GetVolumeMax() => _volumeMax;
    public List<Item> GetItensInternos() => [.. _itens];

    public List<Item> ExtrairConteudo()
    {
        List<Item> copia = [.. _itens];
        _itens.Clear();
        return copia;
    }
}
