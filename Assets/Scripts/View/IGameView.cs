public interface IGameView
{
    void Enable();
    void Disable();

    void SetDeadPredatorsCount(int count);
    void SetDeadPreyCount(int count);
}
