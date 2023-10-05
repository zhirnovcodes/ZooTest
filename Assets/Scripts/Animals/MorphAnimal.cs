using System;
using UnityEngine;

public class MorphAnimal : MonoBehaviour, IPreyModel, IPredatorModel
{
    public event Action<IAnimal> AnimalCollided = animal => { };

    public EAnimals AnimalType { get; private set; }

    public EFoodChainTypes FoodChainType { get; private set; }

    public bool IsAlive { get; private set; }

    private IState StateMachine;

    public void Initialize(EAnimals animalType, EFoodChainTypes foodChainType, IState stateMachine)
    {
        StateMachine = stateMachine;
        AnimalType = animalType;
        FoodChainType = foodChainType;
    }

    public void PlayDeadAnimation()
    {
        //play dead animation
    }

    public void Speak()
    {
        Debug.Log("tasty");
    }

    public void Kill()
    {
        IsAlive = false;
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void Enable()
    {
        StateMachine.Enable();
        IsAlive = true;
    }

    public void Disable()
    {
        StateMachine.Disable();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null)
        {
            var animal = collision.gameObject.GetComponent<IAnimal>();
            if (animal != null)
            {
                AnimalCollided(animal);
            }
        }
    }
}
