using System;
using UnityEngine;

public class TestAnimal : MonoBehaviour, IAnimal, IMortal
{
    public EAnimals MAnimalType;
    public EFoodChainTypes MChainType;

    public EAnimals AnimalType => MAnimalType;

    public EFoodChainTypes FoodChainType => MChainType;

    public bool IsAlive => true;

    public event Action Died;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
