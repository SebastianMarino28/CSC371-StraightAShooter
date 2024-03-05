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
    private MapComponent[,] map = new MapComponent[100, 100];

    public int mapX = 0;
    public int mapY = 0;

    public GameObject roomUIA;
    public GameObject roomUIB;
    public GameObject roomUIC;
    public GameObject roomUID;
    public GameObject roomUIE;
    public GameObject roomUIF;
    public GameObject roomUIG;
    public GameObject roomUIH;
    public GameObject roomUII;

    public GameObject doorUIAB;
    public GameObject doorUIAD;
    public GameObject doorUIBC;
    public GameObject doorUIBE;
    public GameObject doorUICF;
    public GameObject doorUIDE;
    public GameObject doorUIDG;
    public GameObject doorUIEF;
    public GameObject doorUIEH;
    public GameObject doorUIFI;
    public GameObject doorUIGH;
    public GameObject doorUIHI;



    private void Awake()
    {
        if (instance == null) {
            instance = this;
            AddRoom(0, 0, new bool[]{true, true, true, true});
            SetRoomSeen(0, 0);
            roomsTotal -= 1;
        }
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
        UpdateMap(mapX, mapY);
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

    public void AddRoom(int x, int y, bool[] configuration)
    {
        map[x + 50, y + 50] = new MapComponent(configuration[0], configuration[1], configuration[2], configuration[3]);
        roomsTotal += 1;
    }

    public MapComponent GetRoom(int x, int y)
    {
        return map[x + 50, y + 50];
    }

    public void SetRoomSeen(int x, int y)
    {
        MapComponent room = GetRoom(x, y);
        room.seen = true;
    }

    public void UpdateMap(int x, int y)
    {
        MapComponent roomA = GetRoom(x-1, y+1);
        MapComponent roomB = GetRoom(x  , y+1);
        MapComponent roomC = GetRoom(x+1, y+1);
        MapComponent roomD = GetRoom(x-1, y  );
        MapComponent roomE = GetRoom(x  , y  );
        MapComponent roomF = GetRoom(x+1, y  );
        MapComponent roomG = GetRoom(x-1, y-1);
        MapComponent roomH = GetRoom(x  , y-1);
        MapComponent roomI = GetRoom(x+1, y-1);


        roomUIA.SetActive(roomA != null && roomA.seen);
        roomUIB.SetActive(roomB != null && roomB.seen);
        roomUIC.SetActive(roomC != null && roomC.seen);
        roomUID.SetActive(roomD != null && roomD.seen);
        roomUIF.SetActive(roomF != null && roomF.seen);
        roomUIG.SetActive(roomG != null && roomG.seen);
        roomUIH.SetActive(roomH != null && roomH.seen);
        roomUII.SetActive(roomI != null && roomI.seen);

        doorUIAB.SetActive(roomA != null && roomB != null && roomA.doorRight && roomB.doorLeft && (roomA.seen || roomB.seen));
        doorUIAD.SetActive(roomA != null && roomD != null && roomA.doorBottom && roomD.doorTop && (roomA.seen || roomD.seen));
        doorUIBC.SetActive(roomB != null && roomC != null && roomB.doorRight && roomC.doorLeft && (roomB.seen || roomC.seen));
        doorUIBE.SetActive(roomB != null && roomE != null && roomB.doorBottom && roomE.doorTop && (roomB.seen || roomE.seen));
        doorUICF.SetActive(roomC != null && roomF != null && roomC.doorBottom && roomF.doorTop && (roomC.seen || roomF.seen));
        doorUIDE.SetActive(roomD != null && roomE != null && roomD.doorRight && roomE.doorLeft && (roomD.seen || roomE.seen));
        doorUIDG.SetActive(roomD != null && roomG != null && roomD.doorBottom && roomG.doorTop && (roomD.seen || roomG.seen));
        doorUIEF.SetActive(roomE != null && roomF != null && roomE.doorRight && roomF.doorLeft && (roomE.seen || roomF.seen));
        doorUIEH.SetActive(roomE != null && roomH != null && roomE.doorBottom && roomH.doorTop && (roomE.seen || roomH.seen));
        doorUIFI.SetActive(roomF != null && roomI != null && roomF.doorBottom && roomI.doorTop && (roomF.seen || roomI.seen));
        doorUIGH.SetActive(roomG != null && roomH != null && roomG.doorRight && roomH.doorLeft && (roomG.seen || roomH.seen));
        doorUIHI.SetActive(roomH != null && roomI != null && roomH.doorRight && roomI.doorLeft && (roomH.seen || roomI.seen));
    }
}
