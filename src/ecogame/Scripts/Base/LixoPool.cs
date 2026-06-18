using System.Collections.Generic;
using Godot;

namespace EcoGame;

// LixoPool é o padrão GoF Criacional Object Pool aplicado ao EcoGame.
// O Mapa pode espalhar centenas de instâncias de Lixo durante o ciclo dia/noite.
// Criar/destruir essas instâncias a cada respawn pressiona o GC do .NET e gera
// stutter visível em runtime no Godot. O pool mantém um conjunto pré-alocado de
// instâncias reutilizáveis: Acquire devolve uma livre (criando sob demanda até
// o limite máximo) e Release devolve a instância para o pool após reset.
public partial class LixoPool : Node
{
    private readonly Queue<Lixo> _disponiveis = new Queue<Lixo>();
    private readonly HashSet<Lixo> _emUso = new HashSet<Lixo>();
    private readonly int _tamanhoInicial;
    private readonly int _tamanhoMaximo;

    public LixoPool(int tamanhoInicial = 16, int tamanhoMaximo = 128)
    {
        _tamanhoInicial = tamanhoInicial;
        _tamanhoMaximo = tamanhoMaximo;
        Prealocate();
    }

    private void Prealocate()
    {
        for (int i = 0; i < _tamanhoInicial; i++)
            _disponiveis.Enqueue(CriarNovo());
    }

    private Lixo CriarNovo()
    {
        return new Lixo(nome: "lixo_pool", quantidade: 0, material: MaterialBase.OUTROS);
    }

    public Lixo Acquire(string nome, int quantidade, MaterialBase material)
    {
        Lixo instancia;
        if (_disponiveis.Count > 0)
        {
            instancia = _disponiveis.Dequeue();
        }
        else if (TotalAlocado() < _tamanhoMaximo)
        {
            instancia = CriarNovo();
        }
        else
        {
            GD.PrintErr($"[LixoPool] pool esgotado (max={_tamanhoMaximo}) — alocando fora do pool.");
            return new Lixo(nome, quantidade, material);
        }

        instancia.Reconfigurar(nome, quantidade, material);
        _emUso.Add(instancia);
        return instancia;
    }

    public void Release(Lixo instancia)
    {
        if (!_emUso.Remove(instancia))
            return;

        instancia.Reset();
        _disponiveis.Enqueue(instancia);
    }

    public int TotalDisponivel() => _disponiveis.Count;
    public int TotalEmUso() => _emUso.Count;
    public int TotalAlocado() => _disponiveis.Count + _emUso.Count;
}
