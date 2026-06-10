using Godot;

namespace EcoGame;

// AmbienteFacade implementa o padrão GoF Estrutural Facade.
// Expõe uma interface unificada sobre tres subsistemas que, juntos, definem
// o ambiente da cena: iluminacao, ciclo dia/noite e clima. O cliente (Mapa,
// UI, MaquinaReciclagem) chama operacoes de alto nivel como AmanhecerEnsolarado
// sem precisar conhecer cada subsistema, sua ordem de chamada ou as regras
// de consistencia entre eles.
public partial class AmbienteFacade : Node
{
    private readonly IluminacaoSubsystem _iluminacao;
    private readonly CicloDiaNoiteSubsystem _ciclo;
    private readonly ClimaSubsystem _clima;

    public AmbienteFacade(
        IluminacaoSubsystem iluminacao,
        CicloDiaNoiteSubsystem ciclo,
        ClimaSubsystem clima)
    {
        _iluminacao = iluminacao;
        _ciclo = ciclo;
        _clima = clima;
    }

    public void AmanhecerEnsolarado()
    {
        _ciclo.DefinirFase(CicloDiaNoiteSubsystem.Fase.Dia);
        _clima.DefinirCondicao(ClimaSubsystem.Condicao.Ensolarado);
        _iluminacao.DefinirIntensidade(1.0f);
    }

    public void Anoitecer()
    {
        _ciclo.DefinirFase(CicloDiaNoiteSubsystem.Fase.Noite);
        _clima.DefinirCondicao(ClimaSubsystem.Condicao.Nublado);
        _iluminacao.DefinirIntensidade(0.15f);
    }

    public void IniciarChuva()
    {
        _clima.DefinirCondicao(ClimaSubsystem.Condicao.Chuva);
        if (_ciclo.EhDia())
            _iluminacao.DefinirIntensidade(0.5f);
        else
            _iluminacao.DefinirIntensidade(0.1f);
    }

    public bool EhSeguroParaColetar() => _ciclo.EhDia() && !_clima.EhChuvoso();
}
