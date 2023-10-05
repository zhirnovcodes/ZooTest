using System;

public interface IPredatorModel : IAnimal, IMortal
{
    event Action<IAnimal> AnimalCollided;

    void Speak();
    void PlayDeadAnimation();

    void Enable();
    void Disable();
}
