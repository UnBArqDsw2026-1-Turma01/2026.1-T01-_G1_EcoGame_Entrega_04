using Godot;
using System.Collections.Generic;

namespace EcoGame;

public partial class SalaTesteMovimento : Node2D
{
    private FachadaInventarioJogador _fachada;

    public override void _Ready()
    {
        _fachada = FachadaInventarioJogador.Instancia;
        Player player = GetNode<Player>("player");
        if (player != null)
        {
            player.ItemColetado += OnPlayerItemColetado;
            GD.Print("[TESTE] Sistema de Sinais conectado com sucesso!");
        }
    }

    private void OnPlayerItemColetado(string nomeItem)
    {
        List<SlotInventario> slots = _fachada.GetSlots();
        GD.Print($"[TESTE] Coleta de '{nomeItem}'. Inventário:");
        ImprimirStatusDoInventario(slots);
    }

    private void ImprimirStatusDoInventario(List<SlotInventario> slots)
    {
        GD.Print("====== STATUS ATUAL DO INVENTÁRIO ======");
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && !slots[i].EstaVazio())
                GD.Print($"Slot [{i}]: {slots[i].GetNome()} | Quantidade: {slots[i].GetQuantidade()}/12");
        }
        GD.Print("========================================");
    }
}
