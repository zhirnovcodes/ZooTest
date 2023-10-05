using System.Collections;
using UnityEngine;

public class BannerSpeaker : ISpeaker
{
    private IResourceManager ResourceManager;
    private GameObject Banner;

    private MonoBehaviour Invoker;
    private Coroutine Routine;
    private float Interval;
    private float ElapsedTime;

    private Transform Parent;

    public BannerSpeaker(IResourceManager resourceManager, MonoBehaviour invoker, float interval, Transform parent)
    {
        ResourceManager = resourceManager;
        Invoker = invoker;
        Interval = interval;
        Parent = parent;
    }

    public void Speak()
    {
        if (Banner != null)
        {
            ElapsedTime = Interval;
            return;
        }

        Banner = ResourceManager.GetFromPool<EWidgets, GameObject>(EWidgets.PredatorBaner);
        ElapsedTime = Interval;
        Routine = Invoker.StartCoroutine(Coroutine());
    }

    private void DisposeBanner()
    {
        Banner.SetActive(false);
        Banner = null;
    }

    private IEnumerator Coroutine()
    {
        while (ElapsedTime > 0)
        {
            ElapsedTime -= Time.deltaTime;
            Banner.transform.position = Parent.position;
            yield return null;
        }
        DisposeBanner();
    }

    public void Interrupt()
    {
        if (Banner != null)
        {
            DisposeBanner();
            Invoker.StopCoroutine(Routine);
        }
    }
}
