using System;
using UnityEngine;

public class MorphAnimal : MonoBehaviour, IPreyModel, IPredatorModel
{
    public event Action<IAnimal> AnimalCollided = animal => { };

    [SerializeField] private Material DeadMaterial;

    private Material AliveMaterial;
    private MeshRenderer Renderer;

    public EAnimals AnimalType { get; private set; }

    public EFoodChainTypes FoodChainType { get; private set; }

    public bool IsAlive { get; private set; }

    private IState StateMachine;

    public void Initialize(EAnimals animalType, EFoodChainTypes foodChainType, IState stateMachine)
    {
        StateMachine = stateMachine;
        AnimalType = animalType;
        FoodChainType = foodChainType;

        Renderer = GetComponent<MeshRenderer>();
        AliveMaterial = Renderer.sharedMaterial;
    }

    public void PlayDeadAnimation()
    {
        Renderer.sharedMaterial = DeadMaterial;
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
        this.enabled = true;
        StateMachine.Enable();
        IsAlive = true;
        Renderer.sharedMaterial = AliveMaterial;
    }

    public void Disable()
    {
        this.enabled = false;
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
