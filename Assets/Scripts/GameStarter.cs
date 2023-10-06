using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private IParkModel Park;

    void Awake()
    {
        SetTargetFPS();

        InitializeComposition();

        ReadConfigs();

        SpawnPark();

        EnableGameView();

        SpawnCamera();

        EnableStrategy();
    }

    private void SetTargetFPS()
    {
        Application.targetFrameRate = 60;
    }

    private void InitializeComposition()
    {
        var composition = new ReleaseComposition();
        Composition.Initialize(composition);
    }

    private void ReadConfigs()
    {
        var config = Composition.GetGameConfigReadonly();
    }

    private void SpawnPark()
    {
        Park = Composition.GetParkModel();
    }

    private void EnableGameView()
    {
        var presenter = Composition.GetGameViewPresenter();
        presenter.Enable();
    }

    private void SpawnCamera()
    {
        var camera = Composition.GetCamera();

        Park.SetCamera(camera);
    }

    private void EnableStrategy()
    {
        var strategy = Composition.GetAnimalsSpawnStrategy();
        strategy.Enable();
    }
}
