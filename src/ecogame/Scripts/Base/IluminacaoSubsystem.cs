using Godot;

namespace EcoGame;

// Subsystem do GoF Facade: encapsula a manipulação da intensidade luminosa
// global da cena. AmbienteFacade orquestra este subsystem; o cliente nunca
// interage diretamente.
public partial class IluminacaoSubsystem : Node
{
    private float _intensidade = 1.0f;

    public void DefinirIntensidade(float valor)
    {
        _intensidade = Mathf.Clamp(valor, 0f, 1f);
        GD.Print($"[Iluminacao] intensidade -> {_intensidade:F2}");
        // TODO (Frente D): aplicar em CanvasModulate / WorldEnvironment do Godot.
    }

    public float GetIntensidade() => _intensidade;
}
