using Godot;
using System;

public partial class Healh : Node, IDamagable
{
	// Called when the node enters the scene tree for the first time.
	//make a load data section for enemy data/player data
	
	int _health = 100;

	public int Health => _health;

	int _maxHealth = 100;
	
	public override void _Ready()
	{
		TakeDamage(50);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public int TakeDamage(int damage)
	{
		_health = GetDamage(damage);
		GD.Print("Health is now: ", _health);
		return Mathf.Max(_health, 0);
		
	}

	public int GetDamage(int damage)
	{
		int damagePreview = _health - damage;
		return damagePreview;
	}

	public int GetHealthPercentage()
	{
		return _health / _maxHealth;
	}
}
