namespace EcoGame;

public partial class Item
{
    private string _nome;
	private int _quantidade;
    private MaterialBase _material;
    private int _pontos;

    public Item(string nome, int quantidade, MaterialBase material, int pontos)
    {
        _nome = nome;
		_quantidade = quantidade;
        _material = material;
        _pontos = pontos;
    }

    public string GetNome() => _nome;
	public int GetQuantidade() => _quantidade;
    public MaterialBase GetMaterial() => _material;
    public int GetPontos() => _pontos;


}