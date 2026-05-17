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

    public string GetNome() => _nome;
	public int GetQuantidade() => _quantidade;
    public MaterialBase GetMaterial() => _material;
}