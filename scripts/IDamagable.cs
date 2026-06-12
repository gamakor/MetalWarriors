using Godot;
using System;

public interface IDamagable 
{
	int TakeDamage(int damage);
	int GetDamage(int damage);
	int GetHealthPercentage();
	
}
