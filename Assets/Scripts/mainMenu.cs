using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class mainMenu : MonoBehaviour
{
    public static float SessionTime, SpawnTime;

    public TextMeshProUGUI sessionTimetext;
    public TextMeshProUGUI spawnTimetext;


    public void Playgame()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void setSession(float time)
    {
        sessionTimetext.text = time.ToString();
        SessionTime = time;

    }

    public void setSpawn(float time)
    {
        spawnTimetext.text = time.ToString();
        SpawnTime = time;
    }
}
