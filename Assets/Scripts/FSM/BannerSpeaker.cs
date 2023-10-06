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

    private Camera Camera;

    public BannerSpeaker(IResourceManager resourceManager, MonoBehaviour invoker, float interval, Transform parent, Camera camera)
    {
        ResourceManager = resourceManager;
        Invoker = invoker;
        Interval = interval;
        Parent = parent;
        Camera = camera;
    }

    public void Speak()
    {
        if (Banner != null)
        {
            ElapsedTime = Interval;
            return;
        }

        SpawnBanner();

        ElapsedTime = Interval;
        Routine = Invoker.StartCoroutine(Coroutine());
    }

    private void SpawnBanner()
    {
        Banner = ResourceManager.GetFromPool<EWidgets, GameObject>(EWidgets.PredatorBaner);

        ChangeBannerRotation();
    }

    private void ChangeBannerRotation()
    {
        Banner.transform.forward = Camera.transform.forward;
    }

    private void DisposeBanner()
    {
        Banner.SetActive(false);
        Banner = null;
    }

    private IEnumerator Coroutine()
    {
        const float heightOffset = 1.1f;

        while (ElapsedTime > 0)
        {
            ElapsedTime -= Time.deltaTime;
            Banner.transform.position = Parent.position + new Vector3(0, heightOffset, 0);
            ChangeBannerRotation();
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
