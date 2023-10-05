using UnityEngine;

public class LineMover : IMover
{
    private Rigidbody Rigidbody;
    private float Speed;

    private Vector3 Direction;

    private float ElapsedTime;
    private float Interval;

    public LineMover(Rigidbody rigidbody, float intervalTime, float speed)
    {
        Rigidbody = rigidbody;
        Speed = speed;
        Interval = intervalTime;
    }

    public void Disable()
    {
    }

    public void Enable()
    {
        ChooseDirection();
        ElapsedTime = Interval;
    }

    public void Update()
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

        var position = Rigidbody.position;
        position += Direction * Time.deltaTime;
        Rigidbody.MovePosition(position);
    }

    private void ChooseDirection()
    {
        var alpha = UnityEngine.Random.Range(0f, 2 * Mathf.PI);

        var x = Mathf.Cos(alpha);
        var z = Mathf.Sin(alpha);

        var direction = new Vector3(x, 0, z);

        Direction = direction * Speed;
    }
}


