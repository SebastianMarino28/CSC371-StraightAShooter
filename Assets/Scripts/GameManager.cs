using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI hitText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI accuracyText;
    public int roomsTotal = 0;
    public int roomsCleared = 0;
    public int enemiesDestroyed = 0;
    public int timesHit = 0;
    public float projectilesFired = 0;
    public float projectilesOnTarget = 0;
    public float timePercentage = 100;
    public float healthPercentage = 100;
    public float accuracyPercentage = 100;
    public float extraCredit = 0;
    public bool beatBoss;
    

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
        StartCoroutine(RoomCheck());
    }

    void Update()
    {
        float elapsedTime = Time.timeSinceLevelLoad;
        string formattedTime = FormatTime(elapsedTime);
        timerText.text = formattedTime;
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

    public void CalculateGrade()
    {
        if (roomsCleared >= roomsTotal)
        {
            extraCredit += 10;
        }
        float elapsedTime = Time.timeSinceLevelLoad;
        if (elapsedTime > 300f) // If time is over 5 minutes (300 seconds)
        {
            // Linearly decrease the score from 100% to 0% over the next 10 minutes (from 5 to 15 minutes)
            float percentage = Mathf.Clamp01((elapsedTime - 300f) / 600f);
            timePercentage = Mathf.Lerp(100f, 0f, percentage);
        }
        accuracyPercentage = 100 * (projectilesOnTarget / projectilesFired);
        float grade = ((healthPercentage + timePercentage + accuracyPercentage) / 3.0f) + extraCredit;
        string letter;
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else 
        {
            letter = "D";
        }
        gradeText.text = letter;
        hitText.text = timesHit.ToString();
        accuracyText.text = ((int)accuracyPercentage).ToString() + "%";
        timeText.text = FormatTime(Time.timeSinceLevelLoad);
        Debug.Log("Grade: " + grade + "%");

    }

    IEnumerator RoomCheck()
    {
        yield return new WaitForSeconds(1);
        if (roomsTotal < 25)
        {
            RestartGame();
        }
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
