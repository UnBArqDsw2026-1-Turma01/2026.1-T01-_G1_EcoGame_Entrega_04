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

	
	//novo método
	public void AlimentarMaquina(Item itemRecebido) 
    {
        if (itemRecebido is BaldeComposite balde) 
        {
            // Se for um balde, extraímos tudo o que está dentro
            List<Item> itensInternos = balde.ExtrairConteudo(); 

            foreach (var subItem in itensInternos) 
            {
                DistribuirParaBuilders(subItem); 
            }
            GD.Print("Balde descarregado na máquina!");
        } 
        else 
        {
            // Se for um item avulso, vai direto
            DistribuirParaBuilders(itemRecebido);
        }
    }
    private void DistribuirParaBuilders(Item item)
    {
        foreach (var receita in _receitasDisponiveis)
        {
            if (receita.EhCompativel(item))
            {
                receita.AdicionarIngrediente(item);
                GD.Print($"{item.GetNome()} adicionado à receita!");
            }
        }
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