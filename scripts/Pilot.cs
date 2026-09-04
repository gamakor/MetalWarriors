using Godot;
using System;
using MetalWarriors.scripts;


public partial class Pilot : PlayerCharacter
{
	public override void EnterPawn()
	{
		GD.Print("Entering Pawn");
	}

	public override void ExitPawn()
	{
		GD.Print("Exiting Pawn");
		QueueFree();
	}
	
	public override void ToggleMount()
	{
		//Spawns pilot mount if no nextPawn is null
		if (NextPawn == null)
		{
			GD.Print("You are already a pilot");
		}
		else
		{
			PlayerController.SetPawn(NextPawn);
		}
		
	}
}
