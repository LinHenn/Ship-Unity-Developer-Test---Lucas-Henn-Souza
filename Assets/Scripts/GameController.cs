using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController GM;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] public TextMeshProUGUI pauseText;
    [SerializeField] public TextMeshProUGUI pauseScore;

    [SerializeField] private Slider timeSlider;

    [SerializeField] private TextMeshProUGUI Score;

    public spawnCtrl[] Enemies;
    private bool youWin;

    private int Points;

    public int points
    {
        get { return Points; }
        set
        {
            if (!onPlay) return;
            Points = value;
            Score.text = value.ToString();
        }
    }

    public static float sessionTime;
    public static float spawnTime;

    [SerializeField] private float sessionCount = 0;
    [SerializeField] private float spawnCount = 0;
    public static bool onPlay;



    private void Awake()
    {
        GM = this;
        onPlay = true;
    }

    private void Start()
    {
        
        points = 0;

        sessionTime = mainMenu.SessionTime;
        spawnTime = mainMenu.SpawnTime;
        

        if (sessionTime == 0) sessionTime = 60;
        if (spawnTime == 0) spawnTime = 1;

        timeSlider.maxValue = sessionTime;

        spawnCount = spawnTime;

        Boat.instance.GetComponent<LifeManager>().onDie += isFinished;
    }

    private void Update()
    {
        if (!onPlay) return;

        spawnCount -= Time.deltaTime;        
        if(spawnCount <= 0)
        {
            spawnCount = spawnTime;
            newEnemy();
        }
        
        sessionCount += Time.deltaTime;
        setTimer();
        if(sessionCount >= sessionTime)
        {
            youWin = true;
            isFinished();
        }
    }


    public void newEnemy()
    {
        if (!onPlay) return;

        var enemy = Enemies[Random.Range(0, Enemies.Length)];
        enemy.newBoat();
    }

    private void setTimer()
    {
        timeSlider.value = sessionCount;
    }

    public void plusPoints()
    {
        points++;
    }

    public void isFinished()
    {
        if(youWin) pauseText.text = "You Win!";
        else pauseText.text = "You Lose!";

        pauseScore.text = "Score: " +points.ToString();

        pauseMenu.SetActive(true);
        onPlay = false;
    }

    public void Playgame()
    {
        onPlay = true;
        SceneManager.LoadScene("GameScene");
    }

    public void Mainmenu()
    {
        onPlay = false;
        SceneManager.LoadScene("Menu");
    }
}
