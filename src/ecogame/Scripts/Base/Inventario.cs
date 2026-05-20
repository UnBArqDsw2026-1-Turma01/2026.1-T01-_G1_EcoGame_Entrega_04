using System.Collections.Generic;
using System.Linq;

namespace EcoGame;

public class Inventario
{
    private List<SlotInventario> _slots = new List<SlotInventario>();
    private int _espacosBarra;
    private int _espacosMochila;

    public Inventario(int espacosBarra = 8, int espacosMochila = 16)
    {
        _espacosBarra = espacosBarra;
        _espacosMochila = espacosMochila;
        int totalSlots = espacosBarra + espacosMochila;
        for (int i = 0; i < totalSlots; i++)
        {
            _slots.Add(new SlotInventario(i));
        }
    }

    public void AdicionarItem(ItemAgrupavel item)
    {
        foreach (var slot in _slots)
        {
            if (!slot.EstaVazio() && slot.GetNome() == item.GetNome() && !slot.EstaLotado())
            {
                slot.AdicionarItem(item);
                return;
            }
        }
        foreach (var slot in _slots)
        {
            if (slot.EstaVazio())
            {
                slot.AdicionarItem(item);
                return;
            }
        }
    }

    public void RemoverItem(ItemAgrupavel item)
    {
        foreach (var slot in _slots)
        {
            if (!slot.EstaVazio() && slot.GetNome() == item.GetNome())
            {
                slot.RemoverItem(1);
                return;
            }
        }
    }

    public int GetPontosTotal()
    {
        return _slots.Sum(slot => slot.GetPontos());
    }

    public List<SlotInventario> GetSlots() => new List<SlotInventario>(_slots);
}