using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public int roomsTotal = 0;
    public int roomsCleared = 0;
    public int enemiesDestroyed = 0;
    private bool won;
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

        if (!won && roomsTotal > 0 && roomsTotal == roomsCleared)
        {
            // trigger win
            won = true;
            GameObject.FindGameObjectWithTag("WinScreen").GetComponent<GameOverBehaviour>().isFadingIn = true;
            Time.timeScale = 0.0000001f;
        }
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
