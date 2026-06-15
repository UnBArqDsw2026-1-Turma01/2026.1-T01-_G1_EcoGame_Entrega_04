using System.Collections.Generic;
using Godot;

namespace EcoGame;

public partial class Mapa : Node
{
    private readonly List<ISunObserver> _observadores = new List<ISunObserver>();
    private int _diaAtual = 0;
    private bool _ehDia = false;

    [Export] private LixoPool _pool;

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
        var snapshot = _observadores.ToArray();
        foreach (var o in snapshot)
        {
            if (amanhecer) o.OnSunrise();
            else o.OnSunset();
        }
    }

    public void SpawnLixo(string nome, int qtd, MaterialBase mat)
    {
        Lixo novoLixo = _pool != null
            ? _pool.Acquire(nome, qtd, mat)
            : new Lixo(nome, qtd, mat);
        Attach(novoLixo);
    }

    public void ColetarLixo(Lixo lixo)
    {
        Detach(lixo);
        _pool?.Release(lixo);
    }

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