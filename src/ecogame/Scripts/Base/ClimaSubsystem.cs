using Godot;

namespace EcoGame;

// Subsystem do GoF Facade: encapsula o estado climático da cena.
// AmbienteFacade decide como dia/noite influencia probabilidade de chuva,
// mantendo o cliente alheio à correlação.
public partial class ClimaSubsystem : Node
{
    public enum Condicao { Ensolarado, Nublado, Chuva, Tempestade }

    private Condicao _condicaoAtual = Condicao.Ensolarado;

    public void DefinirCondicao(Condicao condicao)
    {
        _condicaoAtual = condicao;
        GD.Print($"[Clima] condicao -> {_condicaoAtual}");
        // TODO (Frente D): acionar shader de chuva e particulas.
    }

    public Condicao GetCondicaoAtual() => _condicaoAtual;
    public bool EhChuvoso() => _condicaoAtual == Condicao.Chuva || _condicaoAtual == Condicao.Tempestade;
}
