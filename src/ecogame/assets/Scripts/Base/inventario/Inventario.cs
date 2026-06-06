using Godot;
using System;

public class Inventario
{
	private const int TAMANHO_DO_INVENTARIO = 24;
	private const int TAMANHO_DA_PILHA = 12;

	private SlotInventario[] _slots = new SlotInventario[TAMANHO_DO_INVENTARIO]; 

	public bool AdicionarItem(string nomeItem)
	{
		// PASSO 1: Procurar uma pilha existente do mesmo item que não esteja cheia (< 12)
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] != null && _slots[i].NomeItem == nomeItem && _slots[i].Quantidade < TAMANHO_DA_PILHA)
			{
				_slots[i].Quantidade++;
				return true; // Item acumulado com sucesso!
			}
		}

		// PASSO 2: Se não encontrou uma pilha com espaço, procura o primeiro slot vazio (null)
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == null)
			{
				_slots[i] = new SlotInventario(nomeItem, 1); // Começa uma nova pilha com 1 item
				return true; // Novo slot ocupado com sucesso!
			}
		}

		// Se passou pelos dois loops e não conseguiu fazer nada, o inventário está lotado
		return false;
	}

	// Retorna os slots para a Interface Gráfica poder desenhar
	public SlotInventario[] GetSlots()
	{
		return _slots;
	}
}

public class SlotInventario
{
	public string NomeItem { get; set; }
	public int Quantidade { get; set; }

	public SlotInventario(string nomeItem, int quantidade)
	{
		NomeItem = nomeItem;
		Quantidade = quantidade;
	}
}
