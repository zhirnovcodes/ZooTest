using System;

public class DeathCounterModel
{
    public event Action CountChanged = () => { };

    public int PredatorCount { get; private set; }
    public int PreyCount { get; private set; }

    public void Clear()
    {
        PredatorCount = 0;
        PreyCount = 0;
    }

    public void AddCount(EFoodChainTypes type)
    {
        switch (type)
        {
            case EFoodChainTypes.Predator:
                PredatorCount++;
                break;
            case EFoodChainTypes.Prey:
                PreyCount++;
                break;
        }

        CountChanged();
    }
}
