using System;
using System.Linq;
using System.Collections.Generic;

public class AnimalsRepository : IAnimalsRepository
{
    public event Action AnimalDied = () => { };
    public event Action AnimalSpawned = () => { };

    private List<IAnimal> Animals = new List<IAnimal>();

    private EAnimals[] AvailableAnimals;

    private IResourceManager ResourceManager;
    private IParkModel Park;

    public AnimalsRepository(IResourceManager resourceManager, IParkModel park)
    {
        ResourceManager = resourceManager;
        Park = park;
    }

    public void SpawnRandomUnit()
    {
        var type = GetRandomAnimalType();
        var animal = ResourceManager.GetFromPool<EAnimals, IAnimal>(type);

        Animals.Add(animal);

        if (animal is IMortal mortalAnimal)
        {
            mortalAnimal.Died += HandleAnimalDied;

            void HandleAnimalDied()
            {
                Animals.Remove(animal);
                AnimalDied();
            }
        }

        AnimalSpawned();
    }

    public int GetPredatorsCount()
    {
        var result = 0;

        for (int i = 0; i < Animals.Count; i++)
        {
            if (Animals[i].FoodChainType == EFoodChainTypes.Predator)
            {
                result++;
            }
        }

        return result;
    }

    public int GetPreyCount()
    {
        var result = 0;

        for (int i = 0; i < Animals.Count; i++)
        {
            if (Animals[i].FoodChainType == EFoodChainTypes.Prey)
            {
                result++;
            }
        }

        return result;
    }

    private EAnimals GetRandomAnimalType()
    {
        if (AvailableAnimals == null)
        {
            AvailableAnimals = Enum.GetValues(typeof(EAnimals)).Cast<EAnimals>().ToArray();
        }

        var randomIndex = UnityEngine.Random.Range(0, AvailableAnimals.Length);
        return AvailableAnimals[randomIndex];
    }
}
