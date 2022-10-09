using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController GM;
    public GameObject pauseMenu;
    public TextMeshProUGUI pauseScore;

    [SerializeField] private TextMeshProUGUI Score;
    private int Points;

    public GameObject[] Enemies;

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





    private void Start()
    {

        GM = this;
        points = 0;

        onPlay = true;

        sessionTime = mainMenu.SessionTime;
        spawnTime = mainMenu.SpawnTime;

        spawnCount = spawnTime;

        Debug.Log(sessionTime + " : " + spawnTime);
        
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
        if(sessionCount >= sessionTime)
        {
            isFinished();
        }
    }


    public void newEnemy()
    {
        if (!onPlay) return;

        var enemy = Enemies[Random.Range(0, 2)];

        Vector2 posicaoAleatoria = new Vector2(Random.Range(-18f, 18), Random.Range(-13, 15));
        Instantiate(enemy, posicaoAleatoria, Quaternion.identity);

    }

    public void plusPoints()
    {
        points++;
    }

    public void isFinished()
    {
        pauseScore.text = Points.ToString();
        pauseMenu.SetActive(true);
        onPlay = false;
        //Time.timeScale = 0;
    }

    public void Playgame()
    {
        onPlay = true;
        //Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void Mainmenu()
    {
        onPlay = false;
        SceneManager.LoadScene("Menu");
    }
}
