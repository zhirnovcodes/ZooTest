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

    public static GameConfig GetGameConfig()
    {
        return Instance.GetGameConfig();
    }

    public static IAnimalsRepository GetAnimalsRepository()
    {
        return Instance.GetAnimalsRepository();
    }

    public static IAnimalsSpawnStrategy GetAnimalsSpawnStrategy()
    {
        return Instance.GetAnimalsSpawnStrategy();
    }
}