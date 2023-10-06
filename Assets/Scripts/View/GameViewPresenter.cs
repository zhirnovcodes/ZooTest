public class GameViewPresenter
{
    private IGameView View;
    private DeathCounterModel Counter;

    public GameViewPresenter(DeathCounterModel counter, IGameView gameView)
    {
        Counter = counter;
        View = gameView;
        Counter.CountChanged += HandleDeathCountChanged;
    }

    public void Enable()
    {
        View.Enable();
    }

    public void Disable()
    {
        View.Disable();
    }

    private void HandleDeathCountChanged()
    {
        var prey = Counter.PreyCount;
        var predator = Counter.PredatorCount;

        View.SetDeadPredatorsCount(predator);
        View.SetDeadPreyCount(prey);
    }
}