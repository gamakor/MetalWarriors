using Godot;
using System;

public partial class Health : Node, IDamagable
{
	// Called when the node enters the scene tree for the first time.
	//make a load data section for enemy data/player data
	
	int _currentHealth = 100;

	public int CurrentHealth => _currentHealth;

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
		_currentHealth = GetDamage(damage);
		GD.Print("Health is now: ", _currentHealth);
		return Mathf.Max(_currentHealth, 0);
		
	}

	public int GetDamage(int damage)
	{
		int damagePreview = _currentHealth - damage;
		return damagePreview;
	}

	public int GetHealthPercentage()
	{
		return _currentHealth / _maxHealth;
	}
}
