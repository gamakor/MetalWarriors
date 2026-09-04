using Godot;
using System;
using MetalWarriors.scripts;

public partial class PlayerController : Node2D
{
	[Export]
	PlayerCharacter _character;

	public IPlayerCharacter pawn
	{
		get => _character as IPlayerCharacter;
		set => _character = value as PlayerCharacter;
	}
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_character.SetPlayerController(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float direction = Input.GetAxis("left", "right");
		_character.Move(direction);
		if (Input.IsActionJustPressed("jump"))
		{
			_character.Jump();
			
		}
		
		if (Input.IsActionJustPressed("mount"))
		{
			GD.Print("Mount");
			_character.ToggleMount();
		}
	}
	public override void _Input(InputEvent @event)
	{
		// Mouse in viewport coordinates.
		if (@event is InputEventMouseButton { Pressed: true, ButtonIndex: (MouseButton)1 })
		{
			_character.FirePrimary();
		}

		// Print the size of the viewport.
		//GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);
	}

	public void SetPawn(IPlayerCharacter character)
	{
		pawn.ExitPawn();
		pawn = character;
		pawn.EnterPawn();
	}

	//get mouse angle
}
