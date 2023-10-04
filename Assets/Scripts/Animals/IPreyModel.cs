using System.Collections.Generic;

public interface IPreyModel : IAnimal, IMortal
{
    void GetCollidedObjects(List<IAnimal> resultList);

    void Die();

    void StartMoving();
    void StopMoving();
}
