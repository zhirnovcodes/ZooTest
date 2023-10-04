using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Awake()
    {
        InitializeComposition();

        SpawnPark();

        EnableStrategy();
    }

    private void InitializeComposition()
    {
        var composition = new ReleaseComposition();
        Composition.Initialize(composition);
    }

    private void SpawnPark()
    {
        var park = Composition.GetParkModel();
    }

    private void EnableStrategy()
    {
        var strategy = Composition.GetAnimalsSpawnStrategy();
        strategy.Enable();
    }
}