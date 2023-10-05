using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsFactory : IAnimalsFactory
{
    private EAnimals[] AvailableAnimals;

    private IResourceManager ResourceManager;
    private IParkModel Park;

    private DeathCounterModel DeathCounter;
    private GameConfig GameConfig;

    private Dictionary<EAnimals, List<GameObject>> AnimalsPool = new Dictionary<EAnimals, List<GameObject>>();

    public AnimalsFactory(IResourceManager resourceManager, IParkModel park, DeathCounterModel deathCounter, GameConfig gameConfig)
    {
        ResourceManager = resourceManager;
        Park = park;
        DeathCounter = deathCounter;
        GameConfig = gameConfig;
    }

    public void SpawnRandomAnimal()
    {
        var type = GetRandomAnimalType();
        var position = Vector3.zero;
        BuildAnimal(type, position);
    }

    private void BuildAnimal(EAnimals type, Vector3 position)
    {
        // TODO dont read config as classes
        AnimalConfig animalConfig = null;
        foreach (var config in GameConfig.AnimalConfigs)
        {
            if (config.AnimalType == type)
            {
                animalConfig = config;
                break;
            }
        }

        var animal = SpawnAnimal(type, out var wasInstantiated);
        animal.transform.position = position;

        var morph = animal.GetComponent<MorphAnimal>();
        if (wasInstantiated)
        {

            var animalType = animalConfig.AnimalType;
            var foodChain = animalConfig.FoodChainType;
            var stateMachine = BuildStateMachine(morph, animalConfig);

            morph.Initialize(animalType, foodChain, stateMachine);
        }

        morph.Enable();
    }

    private GameObject SpawnAnimal(EAnimals type, out bool wasInstantiated)
    {
        if (!AnimalsPool.ContainsKey(type))
        {
            AnimalsPool.Add(type, new List<GameObject>());
        }

        GameObject animalPooled = null;

        foreach (var animal in AnimalsPool[type])
        {
            if (!animal.activeInHierarchy)
            {
                animalPooled = animal;
                break;
            }
        }

        if (animalPooled == null)
        {
            var instantiated = ResourceManager.InstantiatePrefab<EAnimals, GameObject>(type);
            AnimalsPool[type].Add(instantiated);
            animalPooled = instantiated;
            wasInstantiated = true;
        }
        else
        {
            animalPooled.SetActive(true);
            wasInstantiated = false;
        }

        return animalPooled;
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

    private IState BuildStateMachine(IAnimal model, AnimalConfig config)
    {
        switch (config.FoodChainType)
        {
            case EFoodChainTypes.Prey:
                var preyModel = model as IPreyModel;
                var modelMono = preyModel as MonoBehaviour;
                var mover = BuildMover(modelMono, config);
                return new PreyStateMachine(preyModel, mover, DeathCounter);

            case EFoodChainTypes.Predator:
                var predatorModel = model as IPredatorModel;
                var modelPredatorMono = predatorModel as MonoBehaviour;
                var moverPredator = BuildMover(modelPredatorMono, config);
                return new PredatorStateMachine(predatorModel, moverPredator, DeathCounter);

        }
        throw new NotImplementedException();
    }

    private IMover BuildMover(MonoBehaviour model, AnimalConfig config)
    {
        switch (config.MoverType)
        {
            case EMoverTypes.Linear:
                var speed = config.Speed;
                var interval = config.MoveInterval;
                var rigidbody = model.GetComponent<Rigidbody>();
                return new LineMover(rigidbody, interval, speed);
            case EMoverTypes.Jump:
                return new JumpMover();
        }

        throw new NotImplementedException();
    }
}
