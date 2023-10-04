using System;
using System.Collections;
using UnityEngine;

public class UpdateTimer
{
    private MonoBehaviour MonoBehaviour;
    private float Duration;
    private Action TimerEndAction;
    private bool IsLoop;

    private Coroutine Routine;

    private Action NOOPAction = () => { };

    public UpdateTimer(MonoBehaviour monoBehaviour, float duration, bool isLoop = false)
    {
        MonoBehaviour = monoBehaviour;
        Duration = duration;
        IsLoop = isLoop;
        TimerEndAction = NOOPAction;
    }

    public void StartTimer()
    {
        Routine = MonoBehaviour.StartCoroutine(Countdown());
    }

    public void StopTimer()
    {
        if (Routine != null)
        {
            MonoBehaviour.StopCoroutine(Routine);
            Routine = null;
        }
        TimerEndAction = NOOPAction;
    }

    public void SetNOOP()
    {
        TimerEndAction = NOOPAction;
    }

    public void AddTimerEndListener(Action action)
    {
        TimerEndAction = action;
    }

    private IEnumerator Countdown()
    {
        while (true)
        {
            float remainingTime = Duration;

            while (remainingTime > 0)
            {
                // Update the remaining time
                remainingTime -= Time.deltaTime;

                // Yield to the next frame
                yield return null;
            }

            // Timer has ended, trigger the event
            TimerEndAction();

            if (!IsLoop)
            {
                // Exit the coroutine if not looping
                yield break;
            }
        }
    }
}