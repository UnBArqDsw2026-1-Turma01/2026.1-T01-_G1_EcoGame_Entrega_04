using System.Collections.Generic;
using Godot;
namespace EcoGame;

public partial class MaquinaReciclagem: Node
{
	private List<ReceitaBuilder> _receitasDisponiveis = new List<ReceitaBuilder>();
	public void AdicionarReceita(ReceitaBuilder novaReceita)
	{
		_receitasDisponiveis.Add(novaReceita);
	}
	public Item ReciclarItem(ReceitaBuilder r)
	{
		if (r.ValidarIngredientes())
		{
			return r.Construir();
		}
		GD.Print("Puts! Você não tem ingredientes suficientes para a receita! :(");
		return null;

	}
}