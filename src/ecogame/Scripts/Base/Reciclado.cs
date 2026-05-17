namespace EcoGame;

public partial class Reciclado: Item
{
	private int _pontos;
	public Reciclado(string nome, int quantidade, MaterialBase material, int pontos)
		: base (nome, quantidade, material)
	{
		_pontos = pontos;
		
	}

	public int GetPontos() => _pontos;
	
}
