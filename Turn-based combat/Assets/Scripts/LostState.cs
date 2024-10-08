using UnityEngine;
public class LostState : BattleState
{
    public LostState(BattleController battleController, MonoBehaviour monoBehaviour) : base(battleController, monoBehaviour) { }

    public override void Enter()
    {
        controller.UpdateDialogue("You were defeated.");
        // Handle loss condition, maybe reload scene or show game over screen
    }

    public override void Exit()
    {
        // Clean up if necessary
    }
}