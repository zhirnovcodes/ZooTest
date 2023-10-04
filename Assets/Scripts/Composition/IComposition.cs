using System;

public interface IComposition : IDisposable
{
    IResourceManager GetResourceManager();
    IParkModel GetParkModel();
    IAnimalsRepository GetAnimalsRepository();
    IAnimalsSpawnStrategy GetAnimalsSpawnStrategy();

    GameConfigReader GetConfigReader();
    GameConfig GetGameConfig();
}
