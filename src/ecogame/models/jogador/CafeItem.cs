using Godot;
using System;

namespace EcoGame;

public partial class CafeItem : Area2D
{
	public override void _Ready()
	{
		// Conecta o sinal de colisão com corpos físicos
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Player player)
		{
			GD.Print("[ITEM] Colidiu com o jogador. Enviando para o inventário...");
			
			// Diz para o jogador disparar o sinal de coleta
			player.ColetarItem("Café");

			// Remove o item do mapa
			QueueFree();
		}
	}
}
