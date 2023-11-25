using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class mainMenu : MonoBehaviour
{
    public static float SessionTime, SpawnTime;

    [SerializeField] private TextMeshProUGUI sessionTimetext;
    [SerializeField] private TextMeshProUGUI spawnTimetext;
    [SerializeField] GameObject panelConfig;

    private void Start()
    {
        panelConfig.SetActive(false);

        SessionTime = 60;
        SpawnTime = 1;

    }

    public void Playgame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void setSession(float time)
    {
        time = Mathf.RoundToInt(time);
        sessionTimetext.text = time.ToString() + " seconds";
        SessionTime = time;
    }

    public void setSpawn(float time)
    {
        time = Mathf.RoundToInt(time);
        spawnTimetext.text = time.ToString() + " seconds";
        SpawnTime = time;
    }
}
