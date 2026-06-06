using Godot;
using System;

namespace EcoGame;

public partial class SalaTesteMovimento : Node2D
{
	private FachadaInventarioJogador _fachada;

	public override void _Ready()
	{
		// Inicializa a Fachada que gerencia inventário + jogador
		_fachada = FachadaInventarioJogador.Instancia;

		Player player = GetNode<Player>("player");

		if (player != null)
		{
			player.ItemColetado += OnPlayerItemColetado;
			GD.Print("[TESTE] Sistema de Sinais conectado com sucesso (agora usando a Fachada)!");
		}
	}

	private void OnPlayerItemColetado(string nomeItem)
	{
		SlotInventario[] slots = _fachada.GetSlots();

		GD.Print($"[TESTE] Fachada recebeu coleta de '{nomeItem}'. Imprimindo inventário atual:");
		ImprimirStatusDoInventario(slots);
	}

	private void ImprimirStatusDoInventario(SlotInventario[] slots)
	{
		GD.Print("====== STATUS ATUAL DO INVENTÁRIO ======");
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i] != null)
			{
				GD.Print($"Slot [{i}]: {slots[i].NomeItem} | Quantidade: {slots[i].Quantidade}/12");
			}
		}
		GD.Print("========================================");
	}
}
