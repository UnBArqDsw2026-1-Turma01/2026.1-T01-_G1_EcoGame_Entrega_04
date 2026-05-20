using Godot;
namespace EcoGame;

public partial class CalculadoraPontuacao : Node
{
    private PontuacaoStrategy _estrategia;
    private int _pontosAcumulados = 0;

    public CalculadoraPontuacao()
    {
        _estrategia = new PontuacaoBaseStrategy();
    }

    public void SetEstrategia(PontuacaoStrategy novaEstrategia)
    {
        _estrategia = novaEstrategia;
        GD.Print($"[Strategy] Estratégia trocada para: {_estrategia.GetNomeEstrategia()}");
    }

    public int CalcularEAcumular(Item item)
    {
        int pontos = _estrategia.CalcularPontos(item);
        _pontosAcumulados += pontos;
        GD.Print($"[Strategy:{_estrategia.GetNomeEstrategia()}] {item.GetNome()} → {pontos} pts | Total: {_pontosAcumulados}");
        VerificarDesbloqueio();
        return pontos;
    }

    public int GetPontosAcumulados() => _pontosAcumulados;
    public string GetEstrategiaAtiva() => _estrategia.GetNomeEstrategia();

    private void VerificarDesbloqueio()
    {
        if (_pontosAcumulados >= 300 && _estrategia is not PontuacaoComboStrategy)
        {
            SetEstrategia(new PontuacaoComboStrategy());
            GD.Print("[Desbloqueio] Combo desbloqueado! Recicle em quantidade para ganhar mais.");
        }
        else if (_pontosAcumulados >= 100 && _estrategia is PontuacaoBaseStrategy)
        {
            SetEstrategia(new PontuacaoRaridadeStrategy());
            GD.Print("[Desbloqueio] Raridade desbloqueada! Materiais raros valem mais agora.");
        }
    }
}