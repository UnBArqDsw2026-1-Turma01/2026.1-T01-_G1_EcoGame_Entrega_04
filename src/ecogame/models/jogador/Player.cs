using System;
using Godot;

namespace EcoGame;

public partial class Player : CharacterBody2D
{
	// Sinal nativo para alertar o Inventário
	[Signal]
	public delegate void ItemColetadoEventHandler(string nomeItem);
	
	// Singleton para acessar o Jogador
	private static Player instanciaJogador = null;

	// Atributos do player
	private string _nome;
	private int _velocidade = 50;
	private Vector2 _direcao;
	private Boolean _estaComItem;
	private Boolean _EstaDormido;

	private AnimatedSprite2D _animacao;
	private string _ultimaDirecaoVertical = "frente";

	private int _energia;
	private int _moedas;

// Metodos 

	public override void _Ready()
	{
		// Registra a instância atual ao carregar na cena
		instanciaJogador = this;
		// Pega o nó filho chamado "idle_frente"
		_animacao = GetNode<AnimatedSprite2D>("animações");
	}
	
	public void ColetarItem(string nomeItem)
	{
		EmitSignal(SignalName.ItemColetado, nomeItem);
	}

	// Metodos getters e setters
	public static Player GetInstancia() {
		return instanciaJogador;
	}

	public string GetNome() {
		return _nome;
	}
	public void SetNome(string nome) {
		this._nome = nome;
	}

	public int GetEnergia() {
		return _energia;
	}
	public void SetEnergia(int energia) {
		if (energia < 0) {
			energia = 0;
		} else if (energia > 100) {
			energia = 100;
		}
		this._energia = energia;
	}

	public int GetMoedas() {
		return _moedas;
	}
	public void SetMoedas(int moedas) {
		if (moedas < 0) {
			moedas = 0;
		}
		this._moedas = moedas;
	}

	// Método para processar a física do jogador
	public override void _PhysicsProcess(double delta)
	{
		_direcao = Vector2.Zero;

		if (Input.IsKeyPressed(Key.W))
		{
			_direcao.Y -= 1;
		}

		if (Input.IsKeyPressed(Key.S))
		{
			_direcao.Y += 1;
		}

		if (Input.IsKeyPressed(Key.A))
		{
			_direcao.X -= 1;
		}

		if (Input.IsKeyPressed(Key.D))
		{
			_direcao.X += 1;
		}

		Velocity = _direcao * _velocidade;

		MoveAndSlide();

		// Se o jogador estiver se movendo
		if (_direcao != Vector2.Zero)
		{
			_direcao = _direcao.Normalized();
			AjustarAnimacaoMovimento(_direcao);
		}
		else
		{
			// Se parou, decide qual idle tocar baseado em para onde ele estava olhando
			AjustarAnimacaoParado();
		}

		Velocity = _direcao * _velocidade;
		MoveAndSlide();
	}

	// Controla as animações de ANDAR
	private void AjustarAnimacaoMovimento(Vector2 direcao)
	{
		// Movimento para os Lados (Esquerda / Direita)
		if (direcao.X != 0)
		{
			_animacao.Play("andando_lado");
			if (direcao.X > 0)
				_animacao.FlipH = true; // Olhando para a direita
			else
				_animacao.FlipH = false; // Olhando para a esquerda

			_ultimaDirecaoVertical = "lado";
		}
		// Movimento para Baixo
		else if (direcao.Y > 0)
		{
			_animacao.Play("Andando_frente"); // Note a maiúscula idêntica ao seu print
			_animacao.FlipH = false;
			_ultimaDirecaoVertical = "frente";
		}
		// Movimento para Cima
		else if (direcao.Y < 0)
		{
			_animacao.Play("andando_costas");
			_animacao.FlipH = false;
			_ultimaDirecaoVertical = "costas";
		}
	}

	// Controla as animações de PARADO (Idle)
	private void AjustarAnimacaoParado()
	{
		if (_ultimaDirecaoVertical == "frente")
		{
			_animacao.Play("idle_frente");
		}
		else if (_ultimaDirecaoVertical == "costas")
		{
			_animacao.Play("idle_costas");
		}
		else if (_ultimaDirecaoVertical == "lado")
		{
			_animacao.Play("idle_lado");
			// Mantém o flip atual para ele continuar olhando para o lado correto parado
		}
	}

	public void AplicarBuffVelocidade(float duracao, int novaVelocidade) {
		_velocidade = novaVelocidade;
		// Inicia o timer 
		Timer timer = new Timer();
		timer.WaitTime =  duracao;
		timer.OneShot = true;

		timer.Timeout += () =>
		{
			_velocidade = 50; // Velocidade padrão de volta
			timer.QueueFree();
		} ;
		AddChild(timer);
		timer.Start();
	}
}
