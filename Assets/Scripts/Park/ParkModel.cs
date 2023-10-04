using UnityEngine;

public class ParkModel : MonoBehaviour, IParkModel
{
    public Vector3 GetRandomFreePoint()
    {
        throw new System.NotImplementedException();
    }

    public bool IsOutOfBounds(Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }

    private Vector2 GetSize()
    {
        var size = transform.localScale;
        var size2D = new Vector2(size.x, size.z);
        return size2D;
    }
}
