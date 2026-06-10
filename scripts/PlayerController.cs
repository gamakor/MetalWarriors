using Godot;
using System;
using MetalWarriors.scripts;

public partial class PlayerController : Node2D
{
	[Export]
	PlayerCharacter _character;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float direction = Input.GetAxis("left", "right");
		_character.Move(direction);
		if (Input.IsActionJustPressed("jump"))
		{
			_character.Jump();
			GD.Print("Jumping");
		}
	}
	public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseButton eventMouseButton)
		{
			//GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
			_character.FirePrimary();
			
		}
		else if (@event is InputEventMouseMotion eventMouseMotion)
		{
			GD.Print("Mouse Motion at: ", eventMouseMotion.Position);
		}

		// Print the size of the viewport.
		GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);
	}
	
	//get mouse angle
}
