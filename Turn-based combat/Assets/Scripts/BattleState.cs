using UnityEngine;

public abstract class BattleState
{
    protected BattleController controller;
    protected MonoBehaviour monoBehaviour;

    public BattleState(BattleController battleController, MonoBehaviour mono)
    {
        controller = battleController;
        monoBehaviour = mono;
    }

    public abstract void Enter();
    public abstract void Exit();
    public virtual void Update() { }
}