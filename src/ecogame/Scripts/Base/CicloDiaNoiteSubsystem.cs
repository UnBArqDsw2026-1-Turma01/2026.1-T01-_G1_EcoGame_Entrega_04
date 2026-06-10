using Godot;

namespace EcoGame;

// Subsystem do GoF Facade: encapsula a fase do ciclo dia/noite.
// AmbienteFacade combina este estado com Iluminacao e Clima para apresentar
// uma única operação coerente ao cliente.
public partial class CicloDiaNoiteSubsystem : Node
{
    public enum Fase { Aurora, Dia, Crepusculo, Noite }

    private Fase _faseAtual = Fase.Dia;

    public void AvancarFase()
    {
        _faseAtual = (Fase)(((int)_faseAtual + 1) % 4);
        GD.Print($"[CicloDiaNoite] fase -> {_faseAtual}");
    }

    public void DefinirFase(Fase fase)
    {
        _faseAtual = fase;
        GD.Print($"[CicloDiaNoite] fase -> {_faseAtual}");
    }

    public Fase GetFaseAtual() => _faseAtual;
    public bool EhDia() => _faseAtual == Fase.Dia || _faseAtual == Fase.Aurora;
}
