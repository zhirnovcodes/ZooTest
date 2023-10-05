using UnityEngine;

public class PredatorStateMachine : IState
{
    private IMover Mover;
    private ISpeaker Speaker;
    private IPredatorModel Model;

    private DeathCounterModel DeathCounter;

    private IState CurrentState;
    private IState MovingState;
    private IState DyingState;

    public PredatorStateMachine(IPredatorModel model, IMover mover, ISpeaker speaker, DeathCounterModel deathCounter)
    {
        Model = model;
        Mover = mover;
        Speaker = speaker;
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
        private PredatorStateMachine StateMachine;

        public MoveState(PredatorStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public void Disable()
        {
            StateMachine.Mover.StopMoving();
            StateMachine.Speaker.Interrupt();
            StateMachine.Model.AnimalCollided -= HandleAnimalCollided;
        }

        public void Enable()
        {
            StateMachine.Mover.StartMoving();
            StateMachine.Model.AnimalCollided += HandleAnimalCollided;
        }

        private void HandleAnimalCollided(IAnimal other)
        {
            if (StateMachine.Model.IsAlive)
            {
                if (other is IMortal mortalOther)
                {
                    if (mortalOther.IsAlive)
                    {
                        mortalOther.Kill();
                        StateMachine.Speaker.Speak();
                    }
                }
            }
        }

        public void Update()
        {
            if (!StateMachine.Model.IsAlive)
            {
                StateMachine.CurrentState.Disable();
                StateMachine.CurrentState = StateMachine.DyingState;
                StateMachine.CurrentState.Enable();
                return;
            }

            StateMachine.Mover.UpdateMoving();
        }
    }

    private class DeathState : IState
    {
        const float Interval = 1f;

        private PredatorStateMachine Machine;
        private float ElapsedTime;

        public DeathState(PredatorStateMachine machine)
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
