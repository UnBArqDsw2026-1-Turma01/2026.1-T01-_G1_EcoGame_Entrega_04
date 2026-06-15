using Godot;

namespace EcoGame;

// InventarioView é a View MVC do inventário.
// Lê os slots do InventarioController e renderiza o HUD.
public partial class InventarioView : Node
{
    [Export] private GridContainer _grid;
    [Export] private InventarioController _controller;

    public override void _Ready()
    {
        AtualizarGrid();
    }

    public void AtualizarGrid()
    {
        if (_grid == null || _controller == null) return;

        foreach (Node filho in _grid.GetChildren())
            filho.QueueFree();

        var slots = _controller.GetSlots();
        foreach (var slot in slots)
        {
            var label = new Label();
            if (slot != null && !slot.EstaVazio())
                label.Text = $"{slot.GetNome()} x{slot.GetQuantidade()}";
            else
                label.Text = "[ ]";
            _grid.AddChild(label);
        }
    }
}
