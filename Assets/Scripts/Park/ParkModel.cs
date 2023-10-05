using UnityEngine;

public class ParkModel : MonoBehaviour, IParkModel
{
    public Vector2 Size = new Vector2(20, 20);

    public Vector3 GetCenter()
    {
        return transform.position;
    }

    public Vector3 GetRandomFreePoint()
    {
        var size = Size;
        var position = GetCenter();

        var x = Random.Range(position.x - size.x / 2, position.x + size.x / 2);
        var z = Random.Range(position.z - size.y / 2, position.z + size.y / 2);

        return new Vector3(x, 0, z);
    }

    public bool IsOutOfBounds(Vector3 position)
    {
        var size = Size;
        var center = GetCenter();

        var minX = center.x - size.x / 2;
        var maxX = center.x + size.x / 2;
        var minZ = center.z - size.y / 2;
        var maxZ = center.z + size.y / 2;

        return position.x < minX || position.x > maxX ||
                position.z < minZ || position.z > maxZ;
    }
}
