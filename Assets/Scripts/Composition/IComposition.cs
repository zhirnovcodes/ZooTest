using System;

public interface IComposition : IDisposable
{
    IResourceManager GetResourceManager();
    IParkModel GetParkModel();
    IAnimalsFactory GetAnimalsFactory();
    IAnimalsSpawnStrategy GetAnimalsSpawnStrategy();

    DeathCounterModel GetDeathCounter();
    GameConfigReader GetConfigReader();
    GameConfig GetGameConfig();
}
