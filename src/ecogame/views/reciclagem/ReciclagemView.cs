using Godot;

namespace EcoGame;

// ReciclagemView é a View MVC da reciclagem.
// Exibe o estado da MaquinaReciclagem e os pontos acumulados.
public partial class ReciclagemView : Node
{
    [Export] private Label _labelPontos;
    [Export] private Label _labelEstrategia;

    private MaquinaReciclagem _maquina;

    public override void _Ready()
    {
        _maquina = new MaquinaReciclagem();
    }

    public void AtualizarPlacar()
    {
        if (_labelPontos != null)
            _labelPontos.Text = $"Pontos: {_maquina.GetTotalPontos()}";
        if (_labelEstrategia != null)
            _labelEstrategia.Text = $"Estratégia: {_maquina.GetNomeEstrategiaAtual()}";
    }
}
