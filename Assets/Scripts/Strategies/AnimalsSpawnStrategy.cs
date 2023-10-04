using System.Collections;
using UnityEngine;

public class AnimalsSpawnStrategy : MonoBehaviour, IAnimalsSpawnStrategy
{
    private Coroutine Routine;
    private IAnimalsRepository Repository;

    private float MinInterval;
    private float MaxInterval;

    private void Awake()
    {
        Repository = Composition.GetAnimalsRepository(); 

        var config = Composition.GetGameConfig();

        MinInterval = config.AnimalsSpawnSecondsMin;
        MaxInterval = config.AnimalsSpawnSecondsMax;
    }

    public void Enable()
    {
        Routine = StartCoroutine(TickCoroutine());
    }

    public void Disable()
    {
        StopCoroutine(Routine);
    }

    private IEnumerator TickCoroutine()
    {
        while (true)
        {
            var interval = UnityEngine.Random.Range(MinInterval, MaxInterval);

            yield return new WaitForSeconds(interval);

            Repository.SpawnRandomUnit();
        }
    }
}
