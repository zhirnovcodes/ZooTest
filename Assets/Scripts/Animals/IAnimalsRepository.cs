using System;

public interface IAnimalsRepository
{
    event Action AnimalDied;
    event Action AnimalSpawned;

    void SpawnRandomUnit();

    int GetPreyCount();
    int GetPredatorsCount();
}
