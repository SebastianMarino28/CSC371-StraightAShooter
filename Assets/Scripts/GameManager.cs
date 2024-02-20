using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Update()
    {
        //timerText.text = "Time: " + Mathf.Floor(Time.timeSinceLevelLoad);
    }
}
