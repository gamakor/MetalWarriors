using Godot;
using System;

public partial class Projectile : Node2D
{
	[Export(PropertyHint.Range, "0,1000")]
	private float _speed = 100.0f;
	[Export]
	private Vector2 _direction;
	public Vector2 Direction { get => _direction; set => _direction = value; }
	//Add a var for owner so that we can know who fired the bullet and not hurt itself
	
	private Area2D _area;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_area = GetNode<Area2D>("Area2D");

		_area.BodyEntered += OnBodyEntered;

	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += _direction * _speed * (float)delta;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		body.GetNode<Health>("Health").TakeDamage(10);
	}
}
