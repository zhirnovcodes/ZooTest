using System.Collections.Generic;

public interface IPredatorModel : IAnimal, IMortal
{
    void GetCollidedObjects(List<IAnimal> resultList);

    void Speak();
    void Die();

    void StartMoving();
    void StopMoving();
}
