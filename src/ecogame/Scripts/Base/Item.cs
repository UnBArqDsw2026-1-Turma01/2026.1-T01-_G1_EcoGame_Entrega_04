namespace EcoGame;

// Implementa Information Hiding e o suporte para o sistema de XP unificado.
public abstract partial class Item // Adicionado 'abstract' conforme modelagem
{
    private string _nome;
    private int _quantidade;
    private MaterialBase _material;

    public Item(string nome, int quantidade, MaterialBase material)
    {
        _nome = nome;
        _quantidade = quantidade;
        _material = material;
    }

    public string GetNome() => _nome;
    public int GetQuantidade() => _quantidade;
    public MaterialBase GetMaterial() => _material;

    // Novo método da Versão 03 para o sistema de XP polimórfico
    public virtual int GetPontos()
    {
        // Lógica base de pontos
        return 10;
    }

    // Hook protegido usado pelo padrão Object Pool (LixoPool)
    protected void Reconfigurar(string nome, int quantidade, MaterialBase material)
    {
        _nome = nome;
        _quantidade = quantidade;
        _material = material;
    }
}
