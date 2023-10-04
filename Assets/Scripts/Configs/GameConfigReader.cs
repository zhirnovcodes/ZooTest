public class GameConfigReader 
{
    public GameConfig GetGameConfig()
    {
        return new GameConfig
        {
            AnimalsSpawnSecondsMin = 2,
            AnimalsSpawnSecondsMax = 3,

            AnimalConfigs = new AnimalConfig[] {

                new AnimalConfig
                {
                    Id = 0,
                    AnimalType = EAnimals.Frog,
                    FoodChainType = EFoodChainTypes.Prey,
                    MoverType = EMoverTypes.Jump,
                    MoveInterval = 2f,
                    JumpHeight = 1f,
                    Speed = 5f
                },
                new AnimalConfig
                {
                    Id = 1,
                    AnimalType = EAnimals.Snake,
                    FoodChainType = EFoodChainTypes.Predator,
                    MoverType = EMoverTypes.Linear,
                    MoveInterval = 1f,
                    Speed = 1f
                }
            }
        };
    }
}
