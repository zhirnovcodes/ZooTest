using UnityEngine;

public class ReleaseComposition : IComposition
{
    private IResourceManager Resource;
    private IParkModel ParkModel;
    private IAnimalsRepository AnimalsRepository;
    private IAnimalsSpawnStrategy AnimalsSpawnStrategy;

    private GameConfigReader ConfigReader;
    private GameConfig GameConfig;

    public void Dispose()
    {
        if (ParkModel != null)
        {
            GameObject.Destroy((ParkModel as MonoBehaviour).gameObject);
        }
        if (AnimalsSpawnStrategy != null)
        {
            GameObject.Destroy((AnimalsSpawnStrategy as MonoBehaviour).gameObject);
        }

        Resource = null;
        ParkModel = null;
        ConfigReader = null;
        GameConfig = null;
        AnimalsRepository = null;
        AnimalsSpawnStrategy = null;

    }

    public IAnimalsRepository GetAnimalsRepository()
    {
        if (AnimalsRepository == null)
        {
            var resource = GetResourceManager();
            var park = GetParkModel();
            var repo = new AnimalsRepository(resource, park);

            AnimalsRepository = repo;
        }

        return AnimalsRepository;
    }

    public IAnimalsSpawnStrategy GetAnimalsSpawnStrategy()
    {
        if (AnimalsSpawnStrategy == null)
        {
            var gameObject = new GameObject("AnimalsSpawnStrategy");
            AnimalsSpawnStrategy = gameObject.AddComponent<AnimalsSpawnStrategy>();
        }

        return AnimalsSpawnStrategy;
    }

    public GameConfigReader GetConfigReader()
    {
        if (ConfigReader == null)
        {
            ConfigReader = new GameConfigReader();
        }

        return ConfigReader;
    }

    public GameConfig GetGameConfig()
    {
        if (GameConfig == null)
        {
            var reader = GetConfigReader();
            GameConfig = reader.GetGameConfig();
        }

        return GameConfig;
    }

    public IParkModel GetParkModel()
    {
        if (ParkModel == null)
        {
            var resource = GetResourceManager();
            var park = resource.GetFromPool<EParks, IParkModel>(EParks.Main);
            ParkModel = park;
        }

        return ParkModel;
    }

    public IResourceManager GetResourceManager()
    {
        if (Resource == null)
        {
            Resource = new ResourceManager();
        }

        return Resource;
    }
}
