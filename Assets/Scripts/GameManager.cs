using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Update()
    {
        float elapsedTime = Time.timeSinceLevelLoad;
        string formattedTime = FormatTime(elapsedTime);
        timerText.text = formattedTime;
    }

    //used chatGPT for this function
    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000f) % 1000f);

        // Ensure milliseconds are limited to two digits
        milliseconds %= 100;

        string formattedTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        return formattedTime;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
