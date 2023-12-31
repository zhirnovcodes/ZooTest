using System;
using UnityEngine;

public interface IComposition : IDisposable
{
    IResourceManager GetResourceManager();
    IParkModel GetParkModel();
    IAnimalsFactory GetAnimalsFactory();
    IAnimalsSpawnStrategy GetAnimalsSpawnStrategy();

    Camera GetCamera();
    DeathCounterModel GetDeathCounter();
    GameConfigReader GetConfigReader();
    GameConfigReadonly GetGameConfigReadonly();
    GameViewPresenter GetGameViewPresenter();
}
