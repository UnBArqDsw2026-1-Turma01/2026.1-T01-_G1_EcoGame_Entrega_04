using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class BaldeComposite : Ferramenta
{
    private int _volumeMax; //depois setar um volumeMax -> mudar o retorno de volumeMax em BaldeBuilder
    private List<Item> _itens = new List<Item>();

    public BaldeComposite(string nome, int quantidade, MaterialBase material, int pontosBase, int volumeMax) 
        : base(nome, quantidade, material, pontosBase)
    {
        _volumeMax = volumeMax;
    }

public override string GetNome() 
{
    if (_itens.Count == 0) return base.GetNome() + " (Vazio)";
    return $"{base.GetNome()} com {_itens.Count} itens dentro";
}
    public override int GetPontos()

    {
        // Pontos do balde + a soma dos pontos de tudo que está dentro
		int pontosBasedeBalde = base.GetPontos();
		int pontosItensDoBalde = _itens.Sum(i => i.GetPontos());
        return pontosBasedeBalde + pontosItensDoBalde;
    }

    public override int GetQuantidade()
    {
        // No caso do balde, a quantidade dele é 1, mas ele contém outros itens
		int quantidadeBalde = 1;
        return quantidadeBalde; 
    }


	//métodos específicos do baldeComposite
    public bool AdicionarItem(Item item)
    {
        // lógica de volume:
        if (GetVolumeOcupado() + 1 > _volumeMax) return false; // Exemplo simples de volume
        
        _itens.Add(item);
        return true;
    }

    public void RemoverItem(Item item)
    {
        if (_itens.Contains(item)) _itens.Remove(item);
    }

    public float GetPesoTotal()
    {
        // Assume que cada item tem um peso ou apenas soma a quantidade
        // classe Item não tem peso! 
        return _itens.Sum(i => i.GetQuantidade() * 0.5f); // Exemplo: cada unidade pesa 0.5kg
    }

    public int GetVolumeOcupado() => _itens.Count;

    public void LimparBalde() => _itens.Clear();

    // Getters para a UI***
    public int GetVolumeMax() => _volumeMax;
    public List<Item> GetItensInternos() => [.. _itens];
 
public List<Item> ExtrairConteudo() 
{
    // Cria uma cópia dos itens para a máquina processar
    List<Item> copia = [.. _itens]; 
    
    // Esvazia o balde depois que a máquina "pegou" os materiais
    _itens.Clear(); 
    
    return copia;
}

}