using Godot;

namespace MetalWarriors.scripts;
// Need to rename this so it can be used by the pilot 
public interface IPlayerCharacter
{
    void Move(float direction);
    void Jump();
    void SetMovementState(MovementState state);
    void FirePrimary();
    void EnterPawn();
    void ExitPawn();
    void ToggleMount();
    void SetGlobalPosition(Vector2 position);
    void SetPlayerController(PlayerController playerController);
}