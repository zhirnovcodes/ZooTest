using UnityEngine;

public class ReleaseComposition : IComposition
{
    private IResourceManager Resource;
    private IParkModel ParkModel;
    private IAnimalsFactory AnimalsFactory;
    private IAnimalsSpawnStrategy AnimalsSpawnStrategy;
    private DeathCounterModel DeathCounter;
    private Camera Camera;

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
        AnimalsFactory = null;
        AnimalsSpawnStrategy = null;
        DeathCounter = null;
        Camera = null;

    }

    public IAnimalsFactory GetAnimalsFactory()
    {
        if (AnimalsFactory == null)
        {
            var resource = GetResourceManager();
            var park = GetParkModel();
            var counter = GetDeathCounter();
            var gameConfig = GetGameConfig();
            var repo = new AnimalsFactory(resource, park, counter, gameConfig);

            AnimalsFactory = repo;
        }

        return AnimalsFactory;
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

    public Camera GetCamera()
    {
        if (Camera == null)
        {
            Camera = GameObject.FindAnyObjectByType<Camera>();
            var park = GetParkModel();
            Camera.transform.position = park.GetCameraPosition();
            Camera.transform.rotation = park.GetCameraRotation();
        }

        return Camera;
    }

    public GameConfigReader GetConfigReader()
    {
        if (ConfigReader == null)
        {
            ConfigReader = new GameConfigReader();
        }

        return ConfigReader;
    }

    public DeathCounterModel GetDeathCounter()
    {
        if (DeathCounter == null)
        {
            DeathCounter = new DeathCounterModel();
        }
        return DeathCounter;
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
