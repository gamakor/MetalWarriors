using Godot;

namespace MetalWarriors.scripts;
// Need to rename this so it can be used by the pilot 
public interface IPlayerCharacter
{
    void Move(float direction);
    void Jump();
}