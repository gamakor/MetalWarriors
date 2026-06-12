using Godot;
using System;

public partial class Projectile : Node2D
{
	[Export(PropertyHint.Range, "0,1000")]
	private float _speed = 100.0f;
	[Export]
	private Vector2 _direction;
	public Vector2 Direction { get => _direction; set => _direction = value; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += _direction * _speed * (float)delta  ;
		GD.Print("Bullet is at: ", GlobalPosition);
	}
}
