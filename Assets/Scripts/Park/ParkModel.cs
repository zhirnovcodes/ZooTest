using UnityEngine;

public class ParkModel : MonoBehaviour, IParkModel
{
    public Transform Plane;
    public Transform CameraPlaceholder;

    public Camera Camera;

    private Vector3 MinBounds;
    private Vector3 MaxBounds;

    public void SetCamera(Camera camera)
    {
        Camera = camera;
        Camera.transform.SetParent(CameraPlaceholder, false);
        CalculateBounds();
    }

    public Vector3 GetCenter()
    {
        return Plane.position;
    }

    public Vector3 GetRandomFreePoint()
    {
        var x = Random.Range(MinBounds.x, MaxBounds.x);
        var z = Random.Range(MinBounds.z, MaxBounds.z);

        return new Vector3(x, 0, z);
    }

    public bool IsOutOfBounds(Vector3 position)
    {
        return position.x < MinBounds.x || position.x > MaxBounds.x ||
                position.z < MinBounds.z || position.z > MaxBounds.z;
    }

    private void CalculateBounds()
    {
        var rayMin = Camera.ViewportPointToRay(new Vector3(0, 0, 0));
        var rayMax = Camera.ViewportPointToRay(new Vector3(1, 1, 0));

        var plane = new Plane(Plane.up, Plane.position);

        plane.Raycast(rayMin, out var distance);
        MinBounds = rayMin.GetPoint(distance);

        plane.Raycast(rayMax, out var distMax);
        MaxBounds = rayMax.GetPoint(distance);
    }
}
