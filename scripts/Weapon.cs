using Godot;
using System;
//add state machine if there is going to be reloading
public partial class Weapon : AnimatedSprite2D
{
	//public weapon data

	private int _damage = 10;
	private int _ammo = 10;
	private int _maxAmmo = 10;
	private float _fireRate = 1.0f;
	[Export]
	PackedScene _projectile;

	[Export] 
	private WeaponData _weaponData;
	[Export]
	private AnimatedSprite2D _animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//make a way to know if htere is a weapondeatea ready. 
		LoadData(_weaponData);
		_animatedSprite.AnimationFinished += OnAnimationFinished;
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
		//TODO: Add owner to projectile
		//Animation for arm firing
		_animatedSprite.Play("Firing");
		
		
		GetTree().Root.AddChild(spawnedProjectile);
		
	}
	
	public void Reload()
	{
		//add a timer to reload later
		_ammo = _maxAmmo;	
	}

	private void LoadData(WeaponData data)
	{
		if (data == null)
		{
			GD.Print("Weapon Data is missing");
			return;
		}

		//add a null check here for weapon data
		_damage = _weaponData.Damage;
		_maxAmmo = _weaponData.MaxAmmo;
		_ammo = _maxAmmo;
		_fireRate = _weaponData.FireRate;
	}

	void OnAnimationFinished()
	{
		if (_animatedSprite.Animation == "Firing")
		{
			_animatedSprite.Play("Default");
		}
	}
}
