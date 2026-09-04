using Godot;
using System;
using MetalWarriors.scripts;

public enum MovementState { Idle, Running, Jumping, Landing }

public partial class PlayerCharacter :CharacterBody2D , IPlayerCharacter
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public float Direction = 0;
	
	protected IPlayerCharacter NextPawn;
	[Export] private PackedScene _packedPilotMount;
	protected PlayerController PlayerController;
	private Area2D _area;

	
	[Export]
	public Weapon PrimaryWeapon = null;
	
	private MovementState _movementState = MovementState.Idle;
	
	private AnimatedSprite2D _animatedSprite;
	
	//used to check if player is on floor last frame will be used to trigger landing animation
	private bool _isOnFloorLastFrame = false;
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		_animatedSprite.AnimationFinished += OnAnimationFinished;
		
		_area = GetNode<Area2D>("Area2D");
		if (_area != null)
		{
			_area.BodyEntered += OnBodyEntered;
			_area.BodyExited += OnBodyExited;
		}
	}

	public override void _PhysicsProcess(double delta)
	{// Add the gravity.
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		
		
		switch (_movementState)
		{
			case MovementState.Idle:
				if (IsOnFloor() && !_isOnFloorLastFrame) //check if player is on floor and last frame was not
				{
					
				}
				break;
			case MovementState.Running:
				//move player character
				velocity.X = Direction * Speed;
				//moves to idle state if not moving
				if (Mathf.IsZeroApprox(velocity.X) && IsOnFloor()) //
				{
					SetMovementState(MovementState.Idle);
				} 
				break;
			case MovementState.Jumping:
				//moves to landing state if not moving
				if (!_isOnFloorLastFrame && IsOnFloor()) //
				{
					//play landing animation
					SetMovementState(MovementState.Landing);
					break;
				}

				velocity.X = Direction * Speed;
				if (IsOnFloor())
				{
					//set velocity to jump
					velocity = Velocity;
					velocity.Y = JumpVelocity;
					Velocity = velocity;
				}
				
				break;
			case MovementState.Landing:
				break;
		}
		
		
		
		//handles sprite direction 
		if (Direction > 0)
		{
			_animatedSprite.FlipH = false;
		}
		else if (Direction < 0)
		{
			_animatedSprite.FlipH = true;
		}
		
		
		_isOnFloorLastFrame = IsOnFloor();
		MoveAndSlide();
		
		Velocity = velocity;
		
		
	}

	#region Movement Functions
	public void Move(float direction)
	{
		Direction = direction;
		
		if (direction != 0 && _movementState != MovementState.Running && _movementState != MovementState.Jumping)
		{
			SetMovementState(MovementState.Running);
		}
		/*else this was used for deceleration in the orginal move script
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}*/
	}

	public void Jump()
	{
		SetMovementState(MovementState.Jumping);
	}

	public void SetMovementState(MovementState state)
	{
		_movementState = state;

		switch (_movementState)
		{
			case MovementState.Idle:
				_animatedSprite.Play("idle");
				break;
			case MovementState.Running:
				_animatedSprite.Play("run");
				break;
			case MovementState.Jumping:
				_animatedSprite.Play("jump");
				break;
			case MovementState.Landing:
				_animatedSprite.Play("land");
				GD.Print("landing");
				break;
		}
	}
	#endregion

	public void FirePrimary()
	{
		PrimaryWeapon.Fire();
	}
	
	#region Pawn System
	public void SetPlayerController(PlayerController playerController)
	{
		PlayerController = playerController;
	}
	
	public virtual void EnterPawn()
	{
		GD.Print("Entering Pawn");
	}

	public virtual void ExitPawn()
	{
		GD.Print("Exiting Pawn");
	}

	public virtual void ToggleMount()
	{
		//Spawns pilot mount if no nextPawn is null
		if (NextPawn == null)
		{
			GD.Print("spawning pilot mount");
			PlayerCharacter spawnedPawn = _packedPilotMount.Instantiate<PlayerCharacter>();
			spawnedPawn.SetGlobalPosition(GlobalPosition);
			GetTree().Root.AddChild(spawnedPawn);
			PlayerController.SetPawn(spawnedPawn);
			spawnedPawn.SetPlayerController(PlayerController);
			
		}
		else
		{
			PlayerController.SetPawn(NextPawn);
		}
		
	}
	#endregion 

	private void OnAnimationFinished()
	{
		if (_animatedSprite.Animation == "land")
		{
			SetMovementState(MovementState.Idle);
		}
	}

	public void OnEject()
	{
		GD.Print("Ejecting");
		
		//play eject aniamtion 
	}

	public void SetGlobalPosition(Vector2 position)
	{
		GlobalPosition = position;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is IPlayerCharacter && body != this)
		{
			NextPawn = body as IPlayerCharacter;
			//GD.Print("Player Character Entered");
		}
	}
	
	private void OnBodyExited(Node2D body)
	{
		if (body is IPlayerCharacter && body != this)
		{
			NextPawn = null;
		}
	}
}
