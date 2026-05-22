using Godot;

namespace EcoGame;

public partial class TesteComposite : Node
{
    public override void _Ready()
    {
        GD.Print("╔══════════════════════════════════════════════════════╗");
        GD.Print("║     DEMONSTRAÇÃO — PADRÃO COMPOSITE (EcoGame)        ║");
        GD.Print("╚══════════════════════════════════════════════════════╝\n");

        GD.Print("O padrão Composite permite tratar itens individuais e");
        GD.Print("grupos de itens de forma uniforme. No EcoGame, o Inventario");
        GD.Print("não sabe se está somando 1 item ou um slot com 12 —");
        GD.Print("ambos respondem à mesma interface.\n");

        var inventario = new Inventario();

        // ---------------------------------------------------------
        // PASSO 1: Adicionando itens de tipos diferentes
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("PASSO 1 — Adicionando itens de tipos diferentes");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print("► Adicionando 2x Lixo, 2x Reciclado e 1x Ferramenta...");
        inventario.AdicionarItem(new Lixo("Garrafa PET", 1, MaterialBase.GARRAFA_PET));
        inventario.AdicionarItem(new Lixo("Garrafa PET", 1, MaterialBase.GARRAFA_PET));
        inventario.AdicionarItem(new Reciclado("Bloco de Metal", 1, MaterialBase.METAL, 15));
        inventario.AdicionarItem(new Reciclado("Bloco de Metal", 1, MaterialBase.METAL, 15));
        inventario.AdicionarItem(new Ferramenta("Regador", 1, MaterialBase.METAL, 60));
        GD.Print("  ✔ Itens adicionados com sucesso.\n");

        // ---------------------------------------------------------
        // PASSO 2: Agrupamento automático por slot
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("PASSO 2 — Agrupamento automático: itens iguais vão pro mesmo slot");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        int slotsOcupados = 0;
        foreach (var slot in inventario.GetSlots())
        {
            if (!slot.EstaVazio())
            {
                slotsOcupados++;
                GD.Print($"  Slot {slot.GetIndice()}: [{slot.GetNome()}] | Qtd no slot: {slot.GetQuantidade()} | Pontos do slot: {slot.GetPontos()} pts");
            }
        }
        GD.Print($"\n  ✔ {slotsOcupados} slots ocupados de 24 disponíveis.");
        GD.Print("  As 2 Garrafas PET foram agrupadas num único slot automaticamente.\n");

        // ---------------------------------------------------------
        // PASSO 3: Essência do Composite — tudo via mesma interface
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("PASSO 3 — Essência do Composite: Inventario soma tudo sem distinguir");
        GD.Print("          se é item individual ou grupo dentro do slot");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        GD.Print($"  Pontos totais do inventário: {inventario.GetPontosTotal()} pts");
        GD.Print("  ✔ GetPontosTotal() percorre todos os slots e delega para cada um.");
        GD.Print("  Cada slot soma seus filhos. O Inventario não acessa os itens diretamente.\n");

        // ---------------------------------------------------------
        // PASSO 4: Limite de stack
        // ---------------------------------------------------------
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
        GD.Print("PASSO 4 — Limite de stack: cada slot aceita no máximo 12 itens");
        GD.Print("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");

        var slotTeste = new SlotInventario(99);
        for (int i = 0; i < 13; i++)
        {
            bool adicionado = slotTeste.AdicionarItem(new Lixo("Garrafa PET", 1, MaterialBase.GARRAFA_PET));
            if (!adicionado)
            {
                GD.Print($"  ✔ Slot recusou o item {i + 1} — limite de 12 atingido!");
                break;
            }
        }
        GD.Print($"  Quantidade final no slot: {slotTeste.GetQuantidade()} / 12\n");

        GD.Print("╔══════════════════════════════════════════════════════╗");
        GD.Print("║  Conclusão: Inventario, SlotInventario e os itens   ║");
        GD.Print("║  são tratados uniformemente. Nenhuma camada precisa  ║");
        GD.Print("║  saber o que tem dentro da outra.                   ║");
        GD.Print("╚══════════════════════════════════════════════════════╝");
    }
}