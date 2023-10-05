using UnityEngine;

public interface IParkModel
{
    Vector3 GetRandomFreePoint();
    Vector3 GetCenter();

    bool IsOutOfBounds(Vector3 position);
}
