using Godot;

namespace EcoGame;

public partial class TesteStrategy : Node
{
    public override void _Ready()
    {
        GD.Print("╔══════════════════════════════════════════════════════════════╗");
        GD.Print("║     DEMONSTRAÇÃO — PADRÃO STRATEGY (EcoGame)                 ║");
        GD.Print("╚══════════════════════════════════════════════════════════════╝\n");

        GD.Print("O padrão Strategy permite trocar algoritmos de pontuação em");
        GD.Print("runtime. A MaquinaReciclagem não sabe qual estratégia está");
        GD.Print("ativa — ela apenas delega para a CalculadoraPontuacao.\n");

        var maquina = new MaquinaReciclagem();
        var plasticoBuilder = new PlasticoBuilder();
        var metalBuilder    = new MetalBuilder();
        var baldeBuilder    = new BaldeBuilder();
        maquina.AdicionarReceita(plasticoBuilder);
        maquina.AdicionarReceita(metalBuilder);
        maquina.AdicionarReceita(baldeBuilder);

        // ---------------------------------------------------------
        // FASE 1: Estratégia Base (0–99 pts)
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("FASE 1 — PontuacaoBaseStrategy (ativa: 0–99 pts acumulados)");
        GD.Print("         Retorna pontos brutos sem modificador.");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print($"► Estratégia atual: {maquina.GetNomeEstrategiaAtual()}");

        maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        Item bloco1 = maquina.ReciclarItem(plasticoBuilder);
        if (bloco1 != null)
            GD.Print($"  ✔ {bloco1.GetNome()} | XP ganho: {bloco1.GetPontos()} | Total: {maquina.GetTotalPontos()} | Estratégia: {maquina.GetNomeEstrategiaAtual()}\n");

        maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        Item bloco2 = maquina.ReciclarItem(plasticoBuilder);
        if (bloco2 != null)
            GD.Print($"  ✔ {bloco2.GetNome()} | XP ganho: {bloco2.GetPontos()} | Total: {maquina.GetTotalPontos()} | Estratégia: {maquina.GetNomeEstrategiaAtual()}\n");

        // ---------------------------------------------------------
        // FASE 2: Desbloqueio da Raridade (100 pts)
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("FASE 2 — PontuacaoRaridadeStrategy (desbloqueada em 100 pts)");
        GD.Print("         Vidro ×2.0 · Metal ×1.75 · PET ×1.25 · Orgânico ×0.75");
        GD.Print("         BaldeComposite recebe +0.5 extra · Ferramenta ×0.1");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        // Acumula até passar de 100
        for (int i = 0; i < 7 && maquina.GetTotalPontos() < 100; i++)
        {
            maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
            maquina.ReciclarItem(plasticoBuilder);
        }

        GD.Print($"► Estratégia atual: {maquina.GetNomeEstrategiaAtual()} | Total XP: {maquina.GetTotalPontos()}\n");

        GD.Print("► Reciclando Bloco de Metal com Raridade ativa (×1.75):");
        metalBuilder.AdicionarIngrediente(new Lixo("Sucata de Metal", 3, MaterialBase.METAL));
        Item blocoMetal = maquina.ReciclarItem(metalBuilder);
        if (blocoMetal != null)
            GD.Print($"  ✔ {blocoMetal.GetNome()} | XP ganho (com bônus): {maquina.GetTotalPontos()} total | Estratégia: {maquina.GetNomeEstrategiaAtual()}\n");

        // ---------------------------------------------------------
        // FASE 3: BaldeComposite recebe bônus extra na Raridade
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("FASE 3 — BaldeComposite pontuado pela Raridade (+0.5 extra)");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        baldeBuilder.AdicionarIngrediente(new Reciclado("Bloco de Metal", 2, MaterialBase.METAL, 15));
        baldeBuilder.AdicionarIngrediente(new Reciclado("Bloco de Plástico", 1, MaterialBase.GARRAFA_PET, 10));
        Item balde = maquina.ReciclarItem(baldeBuilder);
        if (balde != null)
            GD.Print($"  ✔ {balde.GetNome()} | Total XP: {maquina.GetTotalPontos()} | Estratégia: {maquina.GetNomeEstrategiaAtual()}\n");

        // ---------------------------------------------------------
        // FASE 4: Desbloqueio do Combo (300 pts)
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("FASE 4 — PontuacaoComboStrategy (desbloqueada em 300 pts)");
        GD.Print("         qtd≥10 ×1.5 · qtd≥6 ×1.25 · qtd≥3 ×1.10");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        for (int i = 0; i < 20 && maquina.GetTotalPontos() < 300; i++)
        {
            maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
            maquina.ReciclarItem(plasticoBuilder);
        }

        GD.Print($"► Estratégia atual: {maquina.GetNomeEstrategiaAtual()} | Total XP: {maquina.GetTotalPontos()}\n");

        GD.Print("► Reciclando com quantidade 6 (bônus ×1.25 no Combo):");
        maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        Item blocoCombo = maquina.ReciclarItem(plasticoBuilder);
        if (blocoCombo != null)
            GD.Print($"  ✔ {blocoCombo.GetNome()} | Total XP: {maquina.GetTotalPontos()} | Estratégia: {maquina.GetNomeEstrategiaAtual()}\n");

        GD.Print("╔══════════════════════════════════════════════════════════════╗");
        GD.Print("║  Conclusão: a estratégia trocou 2x em runtime de forma       ║");
        GD.Print("║  transparente. A MaquinaReciclagem nunca precisou mudar.     ║");
        GD.Print("╚══════════════════════════════════════════════════════════════╝");
    }
}