using UnityEngine;

public class PreyStateMachine : IState
{
    private IPreyModel Model;
    private IMover Mover;

    private DeathCounterModel DeathCounter;

    private IState CurrentState;
    private IState MovingState;
    private IState DyingState;

    public PreyStateMachine(IPreyModel model, IMover mover, DeathCounterModel deathCounter)
    {
        Model = model;
        Mover = mover;
        DeathCounter = deathCounter;
        MovingState = new MoveState(this);
        DyingState = new DeathState(this);
    }

    public void Disable()
    {
        CurrentState.Disable();
    }

    public void Enable()
    {
        CurrentState = MovingState;
        CurrentState.Enable();
    }

    public void Update()
    {
        CurrentState.Update();
    }

    private class MoveState : IState
    {
        public PreyStateMachine Machine { get; }

        public MoveState(PreyStateMachine machine)
        {
            Machine = machine;
        }

        public void Disable()
        {
            Machine.Mover.StopMoving();
        }

        public void Enable()
        {
            Machine.Mover.StartMoving();
        }

        public void Update()
        {
            if (!Machine.Model.IsAlive)
            {
                Machine.CurrentState.Disable();
                Machine.CurrentState = Machine.DyingState;
                Machine.CurrentState.Enable();
                return;
            }

            Machine.Mover.UpdateMoving();
        }
    }

    private class DeathState : IState
    {
        const float Interval = 1f;

        private PreyStateMachine Machine;
        private float ElapsedTime;

        public DeathState(PreyStateMachine machine)
        {
            Machine = machine;
        }

        public void Disable()
        {
        }

        public void Enable()
        {
            Machine.Model.PlayDeadAnimation();
            Machine.DeathCounter.AddCount(Machine.Model.FoodChainType);
            ElapsedTime = Interval;
        }

        public void Update()
        {
            if (ElapsedTime > 0)
            {
                ElapsedTime -= Time.deltaTime;
                if (ElapsedTime <= 0)
                {
                    Machine.Model.Disable();
                }
            }
        }
    }
}
