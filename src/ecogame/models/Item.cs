namespace EcoGame;

public partial class Item
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

    public virtual string GetNome() => _nome;
    public virtual int GetQuantidade() => _quantidade;
    public virtual MaterialBase GetMaterial() => _material;
    public virtual int GetPontos() => 10;

    protected void Reconfigurar(string nome, int quantidade, MaterialBase material)
    {
        _nome = nome;
        _quantidade = quantidade;
        _material = material;
    }
}
