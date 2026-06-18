using Godot;

namespace EcoGame;

// Semente é um ConcreteObserver (GoF Observer) que reage ao sol para crescer.
public partial class Semente : Item, ISunObserver // Adicionada a interface
{
    private int _tempoCrescimento = 0;
    private string _estadoAtual = "Semente";
    private int _ciclosParaCrescer = 4;
    private bool _estaRegada = false;

    public Semente(string nome, int quantidade, MaterialBase material)
        : base(nome, quantidade, material) { }

    // --- Implementação de ISunObserver ---

    public void OnSunrise()
    {
        if (_estaRegada)
        {
            _tempoCrescimento++;
            GD.Print($"[Semente:{GetNome()}] Sol nasceu. Crescimento: {_tempoCrescimento}/{_ciclosParaCrescer}");
            // Lógica de evolução de estado pode entrar aqui
        }
    }

    public void OnSunset()
    {
        // Preparação para o próximo dia
        GD.Print($"[Semente:{GetNome()}] Noite caiu. Aguardando próximo ciclo.");
    }

    // --- Getters ---

    public int GetTempoCrescimento() => _tempoCrescimento;
    public string GetEstadoAtual() => _estadoAtual;
    public int GetCiclosParaCrescer() => _ciclosParaCrescer;
    public bool GetEstaRegada() => _estaRegada;

    // Método para ser usado pelo sistema de plantio
    public void Regar() => _estaRegada = true;
}
