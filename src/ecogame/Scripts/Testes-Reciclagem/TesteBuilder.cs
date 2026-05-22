using Godot;

namespace EcoGame;

public partial class TesteBuilder : Node
{
    public override void _Ready()
    {
        GD.Print("╔══════════════════════════════════════════════════════════════╗");
        GD.Print("║     DEMONSTRAÇÃO — PADRÃO BUILDER + STRATEGY (EcoGame)       ║");
        GD.Print("╚══════════════════════════════════════════════════════════════╝\n");

        GD.Print("O padrão Builder separa a CONSTRUÇÃO de um objeto da sua");
        GD.Print("REPRESENTAÇÃO final. Nesta versão, a MaquinaReciclagem também");
        GD.Print("integra o padrão Strategy: cada item construído é pontuado");
        GD.Print("automaticamente pela CalculadoraPontuacao sem que a Máquina");
        GD.Print("saiba qual algoritmo está ativo.\n");

        var maquina = new MaquinaReciclagem();

        // Registra as receitas disponíveis
        var plasticoBuilder = new PlasticoBuilder();
        var metalBuilder    = new MetalBuilder();
        var regadorBuilder  = new RegadorBuilder();
        maquina.AdicionarReceita(plasticoBuilder);
        maquina.AdicionarReceita(metalBuilder);
        maquina.AdicionarReceita(regadorBuilder);

        // ---------------------------------------------------------
        // TIER 1: Lixo → Reciclado
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("TIER 1 — Processamento: Lixo → Material Reciclado");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print("► Passo 1: AlimentarMaquina() distribui o lixo para os builders compatíveis.");
        maquina.AlimentarMaquina(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        GD.Print("");

        GD.Print("► Passo 2: ReciclarItem() valida, constrói e pontua via Strategy.");
        Item blocoPlastico = maquina.ReciclarItem(plasticoBuilder);
        if (blocoPlastico != null)
            GD.Print($"  ✔ Criado: {blocoPlastico.GetNome()} | Estratégia: {maquina.GetNomeEstrategiaAtual()} | Total XP: {maquina.GetTotalPontos()}\n");

        // ---------------------------------------------------------
        // FALHA INTENCIONAL
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("PROTEÇÃO DO BUILDER — Ingredientes insuficientes");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print("► Tentando reciclar metal sem ingredientes suficientes:");
        Item falha = maquina.ReciclarItem(metalBuilder);
        if (falha == null)
            GD.Print("  ✔ Comportamento correto: construção bloqueada.\n");

        // ---------------------------------------------------------
        // TIER 2: Reciclado → Ferramenta
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("TIER 2 — Crafting: Material Reciclado → Ferramenta");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print("► Adicionando ingredientes diretamente ao RegadorBuilder:");
        regadorBuilder.AdicionarIngrediente(new Reciclado("Bloco de Metal", 1, MaterialBase.METAL, 15));
        regadorBuilder.AdicionarIngrediente(new Reciclado("Bloco de Plástico", 2, MaterialBase.GARRAFA_PET, 10));

        Item regador = maquina.ReciclarItem(regadorBuilder);
        if (regador != null)
            GD.Print($"  ✔ Criado: {regador.GetNome()} | Estratégia: {maquina.GetNomeEstrategiaAtual()} | Total XP: {maquina.GetTotalPontos()}\n");

        // ---------------------------------------------------------
        // COMPOSITE INTEGRADO: BaldeComposite → AlimentarMaquina
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("INTEGRAÇÃO COMPOSITE+BUILDER — Balde descarregado na Máquina");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print("► Criando um BaldeComposite com lixo dentro:");
        var balde = new BaldeComposite("Balde", 1, MaterialBase.GARRAFA_PET, 40, 5);
        balde.AdicionarItem(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));
        balde.AdicionarItem(new Lixo("Garrafa PET", 3, MaterialBase.GARRAFA_PET));

        GD.Print("► AlimentarMaquina() detecta o Balde e extrai o conteúdo automaticamente:");
        maquina.AlimentarMaquina(balde);

        Item blocoPlastico2 = maquina.ReciclarItem(plasticoBuilder);
        if (blocoPlastico2 != null)
            GD.Print($"  ✔ Criado: {blocoPlastico2.GetNome()} | Estratégia: {maquina.GetNomeEstrategiaAtual()} | Total XP: {maquina.GetTotalPontos()}\n");

        GD.Print("╔══════════════════════════════════════════════════════════════╗");
        GD.Print("║  Conclusão: Builder, Composite e Strategy integrados.        ║");
        GD.Print("║  A Máquina orquestra tudo sem conhecer os detalhes de        ║");
        GD.Print("║  nenhum dos três padrões individualmente.                    ║");
        GD.Print("╚══════════════════════════════════════════════════════════════╝");
    }
}