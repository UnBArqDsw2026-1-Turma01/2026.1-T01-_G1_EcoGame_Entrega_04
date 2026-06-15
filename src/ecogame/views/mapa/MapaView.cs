using Godot;

namespace EcoGame;

// MapaView é a View MVC do mapa.
// Atualiza elementos visuais da cena (iluminação, HUD de dia)
// conforme o estado gerenciado pelo MapaController.
public partial class MapaView : Node
{
    [Export] private Label _labelDia;
    [Export] private MapaController _controller;

    public override void _Ready()
    {
        AtualizarHud();
    }

    public void AtualizarHud()
    {
        if (_labelDia != null && _controller != null)
            _labelDia.Text = _controller.EhSeguroParaColetar()
                ? "Coletar: permitido"
                : "Coletar: aguarde o dia";
    }

    public void OnDiaAvancado()
    {
        AtualizarHud();
    }
}
