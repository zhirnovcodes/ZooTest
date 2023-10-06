public class GameConfigReadonly
{
    private GameConfig Config;

    public GameConfigReadonly(GameConfig config)
    {
        Config = config;
    }

    public GameInfo GetGameInfo()
    {
        return new GameInfo
        {
            AnimalsSpawnSecondsMax = Config.AnimalsSpawnSecondsMax,
            AnimalsSpawnSecondsMin = Config.AnimalsSpawnSecondsMin
        };
    }

    public AnimalInfo GetAnimalInfo(EAnimals animalType)
    {
        AnimalConfig animalConfig = null;

        foreach (var config in Config.AnimalConfigs)
        {
            if (config.AnimalType == animalType)
            {
                animalConfig = config;
                break;
            }
        }

        return new AnimalInfo
        {
            Id = animalConfig.Id,
            AnimalType = animalConfig.AnimalType,
            FoodChainType = animalConfig.FoodChainType,
            MoverType = animalConfig.MoverType,
            Speed = animalConfig.Speed,
            MoveInterval = animalConfig.MoveInterval,
            JumpHeight = animalConfig.JumpHeight
        };
    }
}

public struct GameInfo
{
    public float AnimalsSpawnSecondsMin;
    public float AnimalsSpawnSecondsMax;
}

public struct AnimalInfo
{
    public int Id;

    public EAnimals AnimalType;

    public EFoodChainTypes FoodChainType;
    public EMoverTypes MoverType;

    public float Speed;
    public float MoveInterval;
    public float JumpHeight;
}