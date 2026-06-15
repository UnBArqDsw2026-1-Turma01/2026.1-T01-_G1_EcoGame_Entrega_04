using System.Collections.Generic;
using Godot;

namespace EcoGame;

// InventarioController é o Controller MVC do inventário.
// Expõe operações de alto nível para a View renderizar
// e para o JogadorController delegar ações de coleta.
public partial class InventarioController : Node
{
    private FachadaInventarioJogador _fachada;

    public override void _Ready()
    {
        _fachada = FachadaInventarioJogador.Instancia;
    }

    public bool AdicionarItem(string nomeItem)
    {
        return _fachada.AdicionarItemNoInventario(nomeItem);
    }

    public List<SlotInventario> GetSlots()
    {
        return _fachada.GetSlots();
    }

    public bool InventarioEstaVazio()
    {
        foreach (var slot in GetSlots())
            if (slot != null && !slot.EstaVazio()) return false;
        return true;
    }
}
