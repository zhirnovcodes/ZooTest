using UnityEngine;

public class LineMover : IMover
{
    private Rigidbody Rigidbody;
    private float Speed;
    private UpdateTimer Timer;

    private Vector3 Direction;

    public LineMover(Rigidbody rigidbody, MonoBehaviour timerInvoker, float intervalTime, float speed)
    {
        Rigidbody = rigidbody;
        Speed = speed;
        Timer = new UpdateTimer(timerInvoker, intervalTime, true);
    }

    public void Disable()
    {
        Timer.StopTimer();
    }

    public void Enable()
    {
        Timer.AddTimerEndListener(ChooseDirection);
        Timer.StartTimer();
    }

    public void Update()
    {
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


