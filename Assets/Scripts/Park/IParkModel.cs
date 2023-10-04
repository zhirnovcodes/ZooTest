using UnityEngine;

public interface IParkModel
{
    Vector3 GetRandomFreePoint();

    bool IsOutOfBounds(Vector3 position);
}
