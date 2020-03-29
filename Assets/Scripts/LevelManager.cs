using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using ChartboostSDK;

public class LevelManager : MonoBehaviour {

    private const float TIME_BEFORE_START = 3.0f;

    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public GameObject pauseMenu;
    public GameObject endMenu;
    public Transform respawnPoint;
    public Text endTimerText;
    public Text timerText;
    private GameObject player;

    private float startTime;
    private float levelDuration;
    public float silverTime;
    public float goldTime;

    private void Start()
    {
        instance = this;
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
    }

    private void Update()
    {
        if (player.transform.position.y < -10.0f)
            Death();

        if (Time.time - startTime < TIME_BEFORE_START)
            return;

        levelDuration = Time.time - (startTime + TIME_BEFORE_START);
        string minutes = ((int)levelDuration / 60).ToString("00");
        string seconds = (levelDuration % 60).ToString("00.00");

        timerText.text = minutes + ":" + seconds;
    }


    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 0 : 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        int indexNextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(indexNextScene);
    }

    public void Victory()
    {
        foreach(Transform t in endMenu.transform.parent)
        {
            t.gameObject.SetActive(false);
        }

        endMenu.SetActive(true);

        Rigidbody rigid = player.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.FreezePosition;

        levelDuration = Time.time - (startTime + TIME_BEFORE_START);
        string minutes = ((int)levelDuration / 60).ToString("00");
        string seconds = (levelDuration % 60).ToString("00.00");
        endTimerText.text = minutes + ":" + seconds;

        if (levelDuration < goldTime)
        {
            GameManager.Instance.currency += 50;
            endTimerText.color = Color.yellow;
        }
        else if (levelDuration < silverTime)
        {
            GameManager.Instance.currency += 25;
            endTimerText.color = Color.gray;
        }
        else
        {
            GameManager.Instance.currency += 10;
            endTimerText.color = new Color(0.8f, 0.5f, 0.2f, 1.0f);
        }
        GameManager.Instance.Save();

        string saveString = "";
        //"30&60&45"
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        saveString += (level.BestTime > levelDuration || level.BestTime == 0.0f) ? levelDuration.ToString() : level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

        Chartboost.cacheInterstitial(CBLocation.Default);

        if (Chartboost.hasInterstitial(CBLocation.Default))
        {
            Chartboost.showInterstitial(CBLocation.Default);
        }
    }

    public void Death()
    {
        player.transform.position = respawnPoint.position;
        Rigidbody rigid = player.GetComponent<Rigidbody>();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        RestartLevel();
    }

}
