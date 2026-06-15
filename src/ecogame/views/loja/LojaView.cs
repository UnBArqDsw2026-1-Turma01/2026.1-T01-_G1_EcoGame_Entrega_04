using Godot;

namespace EcoGame;

// LojaView é a View MVC da loja.
// Exibe o nome da loja atual e o saldo do jogador.
public partial class LojaView : Node
{
    [Export] private Label _labelLoja;
    [Export] private Label _labelMoedas;
    [Export] private LojaController _controller;

    public override void _Ready() => Atualizar();

    public void Atualizar()
    {
        var player = FachadaInventarioJogador.Instancia?.GetPlayer();
        if (_labelLoja != null && _controller != null)
            _labelLoja.Text = $"Loja: {_controller.GetLojaAtual()}";
        if (_labelMoedas != null && player != null)
            _labelMoedas.Text = $"Moedas: {player.GetMoedas()}";
    }
}
