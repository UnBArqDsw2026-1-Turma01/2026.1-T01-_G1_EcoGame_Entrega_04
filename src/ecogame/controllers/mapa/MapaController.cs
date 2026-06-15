using Godot;

namespace EcoGame;

// MapaController é o Controller MVC do mapa.
// Coordena o Mapa (Model) e o AmbienteFacade (core),
// disparando o ciclo dia/noite e controlando o spawn de lixo.
public partial class MapaController : Node
{
    [Export] private Mapa _mapa;

    private AmbienteFacade _ambiente;

    public override void _Ready()
    {
        _ambiente = new AmbienteFacade(
            new IluminacaoSubsystem(),
            new CicloDiaNoiteSubsystem(),
            new ClimaSubsystem()
        );
    }

    // Chamado ao pressionar a tecla de avançar dia / entrar na casa
    public void AvancarDia()
    {
        _ambiente.AmanhecerEnsolarado();
        _mapa?.AvancarDia();
        GD.Print($"[MapaController] Dia {_mapa?.GetDiaAtual()} iniciado.");
    }

    public void IniciarNoite()
    {
        _ambiente.Anoitecer();
        _mapa?.Anoitecer();
    }

    public void IniciarChuva()
    {
        _ambiente.IniciarChuva();
    }

    public bool EhSeguroParaColetar()
    {
        return _ambiente.EhSeguroParaColetar();
    }
}
