using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningScreenBehaviour : MonoBehaviour
{
    public void OnClickPlayGame() {
        SceneManager.LoadScene("Game");
    }
}
