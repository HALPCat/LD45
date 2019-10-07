using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool GameStarted = false;
    public bool Paused;

    public Canvas canvas;
    public GameObject startButton;
    public GameObject exitButton;
    public GameObject dropDown;
    public GameObject difficultyText;
    public Canvas scoreCanvas;
    public TMP_Text scoreText;

    public GameObject[] Bots;
    public GameObject PlayerGO;
    RacerScript playerRacer;

    public static GameManager instance = null;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreCanvas.enabled = false;
        Time.timeScale = 0;
        Paused = false;
        playerRacer = PlayerGO.GetComponent<RacerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) & GameStarted)
        {
            Paused = !Paused;
            Pause(Paused);
        }
        if(scoreCanvas.enabled)
        {
            scoreText.text = playerRacer.points + " / 3";
        }
    }

    void Pause(bool paused)
    {
        if(paused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            canvas.enabled = true;
        }else{
            Time.timeScale = 1;
            Cursor.visible = false;
            canvas.enabled = false;
        }
    }

    public void StartGame()
    {
        scoreCanvas.enabled = true;
        startButton.SetActive(false);
        dropDown.SetActive(false);
        difficultyText.SetActive(false);
        canvas.enabled = false;
        Time.timeScale = 1f;
        GameStarted = true;

        float diffMultiplier = 1f;
        TMP_Dropdown dd = dropDown.GetComponent<TMP_Dropdown>();

        if(dd.value == 1)
        {
            diffMultiplier = 1.5f;
        }else if (dd.value == 2)
        {
            diffMultiplier = 2f;
        }

        foreach(GameObject go in Bots)
        {
            RacerScript goRacer = go.GetComponent<RacerScript>();
            goRacer.maxSpeed *= diffMultiplier;
            goRacer.accelerationSpeed *= diffMultiplier;
            goRacer.deAccelerationSpeed *= diffMultiplier;
            goRacer.rotationSpeed *= diffMultiplier;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game called");
    }
}
