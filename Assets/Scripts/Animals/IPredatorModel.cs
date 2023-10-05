using System;

public interface IPredatorModel : IAnimal, IMortal
{
    event Action<IAnimal> AnimalCollided;

    void PlayDeadAnimation();

    void Enable();
    void Disable();
}
