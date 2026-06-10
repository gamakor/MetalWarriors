using Godot;
using System;
//add state machine if there is going to be reloading
public partial class Weapon : Node2D
{
	//public weapon data
	
	int _damage = 10;
	int _ammo = 10;
	int _maxAmmo = 10;
	float _fireRate = 1.0f;
	[Export]
	PackedScene _projectile;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Fire()
	{
		// 1. Get the mouse position in world space
		Vector2 mousePos  = GetGlobalMousePosition();

		// 2. Calculate the angle pointing toward the mouse
		float angleToMouse = GlobalPosition.AngleToPoint(mousePos);
		//spawn bullet
		Node2D spawnedProjectile = _projectile.Instantiate<Node2D>();
		spawnedProjectile.Position = GlobalPosition;
		spawnedProjectile.RotationDegrees = angleToMouse;
		AddChild(spawnedProjectile);
		//passe direction to bullet
		GD.Print("Fired");
		
	}
	
	public void Reload()
	{
		//add a timer to reload later
		_ammo = _maxAmmo;	
	}

	public void LoadData()
	{
		
	}
}
