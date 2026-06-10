using System.Collections.Generic;
using System.Threading.Tasks;
using Ecogame;
using Godot;

namespace EcoGame;

/*
	Demo frente C -> apresenta rodagem dos padrões GoF
	template method, factory e proxy. 

	Graciosamente alterado a partir da DemoFrenteD com 
	permissão de seus autores. 

	Código horrivelmente adaptado pelo
	
	gafanhoto
		do 
			código ruim horrível 
	BCL0c.
*/
public partial class DemoFrenteC : Node
{
	public override void _Ready()
	{
		GD.Print("====================================================");
		GD.Print(" EcoGame — Demo Frente C; Base Graciosamente construida por Bevi e adaptada por");
		GD.Print(" Ryan Salles (l0c) ");
		GD.Print(" &");
		GD.Print(" Dan Nunes");
		GD.Print(" Demonstração: Factory | Template Method | Proxy ");
		GD.Print("====================================================");

		DemoComplete();

		GD.Print("====================================================");
		GD.Print(" Fim da demonstração. Encerrando :>>.");
		GD.Print("====================================================");

		CallDeferred(nameof(EncerrarCena));
	}

	private void EncerrarCena() => GetTree().Quit();

	private void DemoComplete()
	{
		GD.Print("starting!");
		GD.Print("creating factories...");

		FLojaFerramentas criadorFerramentas = new FLojaFerramentas();
		FLojaSementes criadorSementes = new FLojaSementes();

		GD.Print("creating store object through factory!");

		LojaSementes l1 = criadorSementes.criarLoja("novaLojaSementes", true);
		LojaFerramentas l2 = criadorFerramentas.criarLoja();
		LojaFerramentas l3 = criadorFerramentas.criarLoja("ohoho!", false);

		//an observer in the sunstate could be created to autoupdate store status!
		//		-> or the store would look for a signal emmited by the sun. Something like that....
		GD.Print("look, store l3 with name " + l3.getNome() + " is closed. We may infer this since their state is" + l3.getEstado());

		GD.Print("Creating some items and populating stores...");
		GD.Print("(This makes part of the mock process!)");

		Item i1 = new Item("oho!", 1, MaterialBase.PLASTICO);
		Item i2 = new Item("ohoho!", 1, MaterialBase.PLASTICO);
		Item i3 = new Item("Sir Hamilton the Zombie Killer", 34, MaterialBase.ORGANICO);
		Item i4 = new Item("Hammer", 255, MaterialBase.VIDRO); //martelo de vidro, obviamente...
		Item i5 = new Item("DeathBringer Axe v2", 2, MaterialBase.PLASTICO);
		List<string> storenames = [l1.getNome(), l2.getNome(), l3.getNome()];
		List<Item> ouritems = [i1, i2, i3, i4, i5];
		GD.Print("We've created the following items: ");

		foreach (Item i in ouritems)
		{
			GD.Print(i.GetNome());
		}

		GD.Print("Populating stores directly...");
		l1.ItensDisponiveis.Add(i1);
		l1.ItensDisponiveis.Add(i1);
		l1.ItensDisponiveis.Add(i1);
		l2.ItensDisponiveis.Add(i2);
		l2.ItensDisponiveis.Add(i2);
		l2.ItensDisponiveis.Add(i2);
		l2.ItensDisponiveis.Add(i2);
		l3.ItensDisponiveis.Add(i3);
		l3.ItensDisponiveis.Add(i4);
		l3.ItensDisponiveis.Add(i5);



		GD.Print("creating proxy v with store " + storenames[0] + " !");
		VerificadorCompra v = new VerificadorCompra(l1);

		void callProxy()
		{
			v.templateComprarItem(i1);
			v.templateComprarItem(i2);
			v.templateComprarItem(i3);
			v.templateComprarItem(i4);
			v.templateComprarItem(i5);
			v.templateVenderItem(i1);
			v.templateVenderItem(i2);
			v.templateVenderItem(i3);
			v.templateVenderItem(i4);
			v.templateVenderItem(i5);
		}

		GD.Print("running proxy..");
		callProxy();

		GD.Print("Attempting proxy hotswap store1 to store2!");
		v.setTarget(l2);

		if (v.getTargetName() == l2.getNome())
		{
			GD.Print("Hot swap functioning Alrighttt!!!");
		}
		else
		{
			GD.Print("Hot swap not working properly...");
			GD.Print("Worry not, we'll fix it. We get paid to fix things like this :)");
		}
		GD.Print("Running tests for store 2...");
		callProxy();
		v.setTarget(l3);
		GD.Print("Running tests for store 3...");
		callProxy();
		GD.Print("Done!");
	}
}
