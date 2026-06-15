using Godot;

namespace EcoGame;

// DemoFrenteD: cena de demonstração dos três padrões GoF da Frente D
// (Object Pool, Observer, Facade). Não faz parte do gameplay — existe
// apenas para evidenciar o código rodando em runtime, conforme exigido
// pela Entrega 03 (Foco_01). Cada bloco imprime no Output panel do
// Godot e o nó se auto-encerra ao final.
public partial class DemoFrenteD : Node
{
	public override void _Ready()
	{
		GD.Print("====================================================");
		GD.Print(" EcoGame — Demo Frente D (Gabriel Bevilaqua)");
		GD.Print(" Object Pool (criacional) + Observer + Facade");
		GD.Print("====================================================");

		DemoObjectPool();
		DemoObserver();
		DemoFacade();

		GD.Print("====================================================");
		GD.Print(" Fim da demonstração. Encerrando.");
		GD.Print("====================================================");

		CallDeferred(nameof(EncerrarCena));
	}

	private void EncerrarCena() => GetTree().Quit();

	private void DemoObjectPool()
	{
		GD.Print("\n--- [1/3] OBJECT POOL (criacional) ---");
		var pool = new LixoPool(tamanhoInicial: 3, tamanhoMaximo: 5);
		AddChild(pool);
		GD.Print($"Pool inicial: disponiveis={pool.TotalDisponivel()} emUso={pool.TotalEmUso()} alocado={pool.TotalAlocado()}");

		var a = pool.Acquire("garrafa_pet", 1, MaterialBase.PLASTICO);
		var b = pool.Acquire("lata_aluminio", 1, MaterialBase.METAL);
		var c = pool.Acquire("residuo_organico", 1, MaterialBase.ORGANICO);
		GD.Print($"Após 3x Acquire: disponiveis={pool.TotalDisponivel()} emUso={pool.TotalEmUso()} alocado={pool.TotalAlocado()}");
		GD.Print($"Instâncias: a='{a.GetNome()}' b='{b.GetNome()}' c='{c.GetNome()}'");

		pool.Release(b);
		GD.Print($"Release(b): disponiveis={pool.TotalDisponivel()} emUso={pool.TotalEmUso()} alocado={pool.TotalAlocado()}");

		var d = pool.Acquire("vidro_quebrado", 1, MaterialBase.VIDRO);
		GD.Print($"Acquire(d) reaproveitou instância do pool? {(ReferenceEquals(d, b) ? "SIM (mesma ref de b)" : "NÃO (nova)")}");
		GD.Print($"Estado final: disponiveis={pool.TotalDisponivel()} emUso={pool.TotalEmUso()} alocado={pool.TotalAlocado()}");
	}

	private void DemoObserver()
	{
		GD.Print("\n--- [2/3] OBSERVER (comportamental) ---");
		var mapa = new Mapa();
		AddChild(mapa);

		var lixo1 = new Lixo("garrafa_pet", 1, MaterialBase.PLASTICO);
		var lixo2 = new Lixo("lata_aluminio", 1, MaterialBase.METAL);
		// Lixo herda de Item (POCO), não de Node — sem AddChild.

		mapa.Attach(lixo1);
		mapa.Attach(lixo2);
		GD.Print("Dois Lixo registrados no Mapa (Subject).");

		GD.Print(">> mapa.AvancarDia() — esperando OnSunrise nos observers:");
		mapa.AvancarDia();
		GD.Print($"   lixo1.brilhando={lixo1.GetEstaBrilhando()} lixo2.brilhando={lixo2.GetEstaBrilhando()} diaAtual={mapa.GetDiaAtual()}");

		GD.Print(">> mapa.Anoitecer() — esperando OnSunset nos observers:");
		mapa.Anoitecer();
		GD.Print($"   lixo1.brilhando={lixo1.GetEstaBrilhando()} lixo2.brilhando={lixo2.GetEstaBrilhando()} ehDia={mapa.GetEhDia()}");

		mapa.Detach(lixo2);
		GD.Print("Detach(lixo2). Próximo AvancarDia notifica só lixo1:");
		mapa.AvancarDia();
		GD.Print($"   lixo1.brilhando={lixo1.GetEstaBrilhando()} lixo2.brilhando={lixo2.GetEstaBrilhando()}");
	}

	private void DemoFacade()
	{
		GD.Print("\n--- [3/3] FACADE (estrutural) ---");
		var ciclo = new CicloDiaNoiteSubsystem();
		var clima = new ClimaSubsystem();
		var iluminacao = new IluminacaoSubsystem();
		AddChild(ciclo);
		AddChild(clima);
		AddChild(iluminacao);

		var facade = new AmbienteFacade(iluminacao, ciclo, clima);
		AddChild(facade);
		GD.Print("AmbienteFacade orquestrando Ciclo + Clima + Iluminacao.");

		GD.Print(">> facade.AmanhecerEnsolarado():");
		facade.AmanhecerEnsolarado();
		GD.Print($"   EhSeguroParaColetar? {facade.EhSeguroParaColetar()}");

		GD.Print(">> facade.IniciarChuva():");
		facade.IniciarChuva();
		GD.Print($"   EhSeguroParaColetar? {facade.EhSeguroParaColetar()} (chuva — deve ser falso)");

		GD.Print(">> facade.Anoitecer():");
		facade.Anoitecer();
		GD.Print($"   EhSeguroParaColetar? {facade.EhSeguroParaColetar()} (noite — deve ser falso)");
	}
}
