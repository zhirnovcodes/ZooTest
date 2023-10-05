using UnityEngine;

public class LineMover : IMover
{
    private Rigidbody Rigidbody;
    private float Speed;

    private Vector3 Direction;

    private float ElapsedTime;
    private float Interval;

    private IParkModel Park;

    public LineMover(Rigidbody rigidbody, float intervalTime, float speed, IParkModel park)
    {
        Rigidbody = rigidbody;
        Speed = speed;
        Park = park;
        Interval = intervalTime;
    }

    public void StopMoving()
    {
    }

    public void StartMoving()
    {
        ChooseDirection();
        ElapsedTime = Interval;
    }

    public void UpdateMoving()
    {
        if (ElapsedTime > 0)
        {
            ElapsedTime -= Time.deltaTime;
            if (ElapsedTime <= 0)
            {
                ChooseDirection();
                ElapsedTime = Interval;
            }
        }

        var position = Rigidbody.position + Direction * Time.deltaTime;

        Rigidbody.MovePosition(position);
    }

    private void ChooseDirection()
    {
        var alpha = UnityEngine.Random.Range(0f, 2 * Mathf.PI);

        var x = Mathf.Cos(alpha);
        var z = Mathf.Sin(alpha);

        var direction = new Vector3(x, 0, z).normalized;

        Direction = direction * Speed;

        var position = Rigidbody.position + Direction * Interval;

        if (Park.IsOutOfBounds(position))
        {
            Direction = (Park.GetCenter() - Rigidbody.position).normalized;
            Direction *= Speed;
        }
    }
}


