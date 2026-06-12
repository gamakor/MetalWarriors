using Godot;
using System;

public partial class WeaponData : Resource
{
    [Export]
    public int Damage = 10;
    [Export]
    public int MaxAmmo = 10;
    [Export]
    public float FireRate = 1.0f;
}
