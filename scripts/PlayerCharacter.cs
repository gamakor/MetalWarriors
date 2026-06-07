using Godot;
using System;
using MetalWarriors.scripts;

public partial class PlayerCharacter :CharacterBody2D , IPlayerCharacter
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	
	private AnimatedSprite2D _animatedSprite;
	
	//used to check if player is on floor last frame will be used to trigger landing animation
	private bool _isOnFloorLastFrame = false;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{// Add the gravity.
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		Velocity = velocity;
	}

	public void Move(float direction)
	{
		Vector2 velocity = Velocity;
		
		if (direction != 0)
		{
			//Play animation 
			_animatedSprite.Play("run");
			
			//move player character
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		
		//handles sprite direction 
		if (direction > 0)
		{
			_animatedSprite.FlipH = false;
		}
		else if (direction < 0)
		{
			_animatedSprite.FlipH = true;
		}
		
		//Starts Idle animation if not moving
		if (Mathf.IsZeroApprox(velocity.X) && IsOnFloor()) //
		{
			_animatedSprite.Play("idle");
		} else if (IsOnFloor() && !_isOnFloorLastFrame) //check if player is on floor and last frame was not
		{
			//play landing animation
		}

		_isOnFloorLastFrame = IsOnFloor();
		MoveAndSlide();
	}

	public void Jump()
	{
		Vector2 velocity = Velocity;
		
		if (IsOnFloor())
		{
			velocity = Velocity;
			velocity.Y = JumpVelocity;
			Velocity = velocity;
		}

	}
}
