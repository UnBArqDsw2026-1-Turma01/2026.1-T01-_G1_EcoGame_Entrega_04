namespace EcoGame;

public abstract partial class Item //apos a implementacao do composite -> classe abstract
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

//com virtual temos agora que os filhos com override sobreescrevem o valor do pai -> situação no baldeComposite**
    public virtual string GetNome() => _nome;
	public virtual int GetQuantidade() => _quantidade; 
    public virtual MaterialBase GetMaterial() => _material;
    public virtual int GetPontos() => _pontos; 


}