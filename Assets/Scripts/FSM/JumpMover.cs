using UnityEngine;

public class JumpMover : IMover
{
    private Rigidbody Rigidbody;

    private float Speed;
    private float Height;
    private float Interval;

    private float ElapsedTime;

    private IParkModel Park;

    public JumpMover(Rigidbody rigidbody, float intervalTime, float speed, float height, IParkModel park)
    {
        Rigidbody = rigidbody;
        Speed = speed;
        Interval = intervalTime;
        Height = height;
        Park = park;
    }

    public void StopMoving()
    {
    }

    public void StartMoving()
    {
        ElapsedTime = Interval;
    }

    public void UpdateMoving()
    {
        if (ElapsedTime > 0)
        {
            ElapsedTime -= Time.deltaTime;

            if (ElapsedTime <= 0)
            {
                ElapsedTime = Interval;
                Jump();
            }
        }
    }

    private void Jump()
    {
        var direction = ChooseDirection();

        var newPosition = Rigidbody.position + direction;
        if (Park.IsOutOfBounds(newPosition))
        {
            direction = (Park.GetCenter() - Rigidbody.position).normalized * Speed;
        }

        Rigidbody.AddForce(direction, ForceMode.Impulse);
    }

    private Vector3 ChooseDirection()
    {
        var alpha = UnityEngine.Random.Range(0f, 2 * Mathf.PI);

        var x = Mathf.Cos(alpha);
        var z = Mathf.Sin(alpha);

        var direction = new Vector3(x, Height, z).normalized;

        return direction * Speed;
    }
}
