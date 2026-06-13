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

	[Export] 
	private WeaponData _weaponData;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//make a way to know if htere is a weapondeatea ready. 
		LoadData();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePos  = GetGlobalMousePosition();
		float angleToMouse = GlobalPosition.AngleToPoint(mousePos);
		RotationDegrees = Mathf.RadToDeg(angleToMouse);
	}
	
	public void Fire()
	{
		//Normalizing Direction that gets passed through to the projectile
		Vector2 direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
		
		//spawn bullet
		Projectile spawnedProjectile = _projectile.Instantiate<Projectile>();
		
		spawnedProjectile.GlobalPosition = GlobalPosition;
		spawnedProjectile.Direction = direction;
		
		GetTree().Root.AddChild(spawnedProjectile);
		
	}
	
	public void Reload()
	{
		//add a timer to reload later
		_ammo = _maxAmmo;	
	}

	public void LoadData()
	{
		//add a null check here for weapon data
		_damage = _weaponData.Damage;
		_maxAmmo = _weaponData.MaxAmmo;
		_ammo = _maxAmmo;
		_fireRate = _weaponData.FireRate;
	}
}
