using System.Collections;
using UnityEngine;

public class AnimalsSpawnStrategy : MonoBehaviour, IAnimalsSpawnStrategy
{
    private Coroutine Routine;
    private IAnimalsFactory Factory;

    private float MinInterval;
    private float MaxInterval;

    private void Awake()
    {
        Factory = Composition.GetAnimalsFactory(); 

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

            Factory.SpawnRandomAnimal();
        }
    }
}
