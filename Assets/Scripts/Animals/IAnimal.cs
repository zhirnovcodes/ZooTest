using UnityEngine;

public interface IAnimal
{
    EAnimals AnimalType { get; }
    EFoodChainTypes FoodChainType { get; }
}
