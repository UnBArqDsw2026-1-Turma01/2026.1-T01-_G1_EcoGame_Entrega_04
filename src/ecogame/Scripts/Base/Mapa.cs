using System.Collections.Generic;
using Godot;

namespace EcoGame;

public partial class Mapa : Node
{
    private readonly List<ISunObserver> _observadores = new List<ISunObserver>();
    private int _diaAtual = 0;
    private bool _ehDia = false;

    // Injeção do Pool conforme modelagem de Object Pool
    [Export] private LixoPool _pool;

    // --- Métodos do Padrão Observer ---

    public void Attach(ISunObserver observador)
    {
        if (!_observadores.Contains(observador))
            _observadores.Add(observador);
    }

    public void Detach(ISunObserver observador)
    {
        _observadores.Remove(observador);
    }

    private void NotifyObservers(bool amanhecer)
    {
        // Snapshot para evitar InvalidOperationException
        var snapshot = _observadores.ToArray();

        foreach (var o in snapshot)
        {
            // Mitigação de Lapsed Listener: verifica se o objeto ainda é válido (Godot)
            if (o is Node node && !IsInstanceValid(node))
            {
                _observadores.Remove(o);
                continue;
            }

            if (amanhecer) o.OnSunrise();
            else o.OnSunset();
        }
    }

    // --- Métodos do Padrão Object Pool ---

    public void SpawnLixo(string nome, int qtd, MaterialBase mat, Vector2 posicao)
    {
        // Mapa atua como cliente do pool
        Lixo novoLixo = _pool.Acquire(nome, qtd, mat);
        AddChild(novoLixo);
        novoLixo.Position = posicao;

        // Registro automático no Observer
        Attach(novoLixo);
    }

    public void ColetarLixo(Lixo lixo)
    {
        Detach(lixo);
        RemoveChild(lixo);
        _pool.Release(lixo); // Devolve para o reuso
    }

    // --- Controle de Ciclo ---

    public void AvancarDia()
    {
        _diaAtual++;
        _ehDia = true;
        NotifyObservers(true);
    }

    public void Anoitecer()
    {
        _ehDia = false;
        NotifyObservers(false);
    }

    public int GetDiaAtual() => _diaAtual;
    public bool GetEhDia() => _ehDia;
}
