using UnityEngine;

public class GameStarter : MonoBehaviour
{
    void Awake()
    {
        InitializeComposition();

        SpawnPark();

        EnableGameView();

        SpawnCamera();

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

    private void SpawnCamera()
    {
        var camera = Composition.GetCamera();
    }

    private void EnableGameView()
    {
        var presenter = Composition.GetGameViewPresenter();
        presenter.Enable();
    }

    private void EnableStrategy()
    {
        var strategy = Composition.GetAnimalsSpawnStrategy();
        strategy.Enable();
    }
}
