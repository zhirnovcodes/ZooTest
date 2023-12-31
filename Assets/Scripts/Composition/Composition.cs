using UnityEngine;

public static class Composition
{
    private static IComposition Instance;

    public static void Initialize(IComposition composition)
    {
        Instance = composition;
    }

    public static void Dispose()
    {
        Instance.Dispose();
        Instance = null;
    }

    public static IResourceManager GetResourceManager()
    {
        return Instance.GetResourceManager();
    }

    public static IParkModel GetParkModel()
    {
        return Instance.GetParkModel();
    }

    public static GameConfigReadonly GetGameConfigReadonly()
    {
        return Instance.GetGameConfigReadonly();
    }

    public static IAnimalsFactory GetAnimalsFactory()
    {
        return Instance.GetAnimalsFactory();
    }

    public static IAnimalsSpawnStrategy GetAnimalsSpawnStrategy()
    {
        return Instance.GetAnimalsSpawnStrategy();
    }

    public static DeathCounterModel GetDeathCounter()
    {
        return Instance.GetDeathCounter();
    }

    public static Camera GetCamera()
    {
        return Instance.GetCamera();
    }

    public static GameViewPresenter GetGameViewPresenter()
    {
        return Instance.GetGameViewPresenter();
    }
}
