using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour, IGameView
{
    public GameObject Content;

    public TextMeshProUGUI PreyText; 
    public TextMeshProUGUI PredatorText; 

    public void Disable()
    {
        Content.SetActive(false);
    }

    public void Enable()
    {
        Content.SetActive(true);
    }

    public void SetDeadPredatorsCount(int count)
    {
        PredatorText.text = count.ToString();
    }

    public void SetDeadPreyCount(int count)
    {
        PreyText.text = count.ToString();
    }
}
