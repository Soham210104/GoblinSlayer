using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    float elapsedTime;
    public TextMeshProUGUI maxTimer;
    float maxTime;
    //public EnemySpawner spawnEnemy;

    // Update is called once per frame
    void Start()
    {
        //spawnEnemy = FindObjectOfType<EnemySpawner>();
        maxTime = PlayerPrefs.GetFloat("maxSurvivalTime", 0f);
        UpdateMaxTimer(maxTime);
    }
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > maxTime) 
        {
            maxTime = elapsedTime;
            PlayerPrefs.SetFloat("maxSurvivalTime", maxTime); //used to store the high score of the game
            PlayerPrefs.Save();
        }
        UpdateTimer(elapsedTime);
    }

    void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateMaxTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        maxTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
   
}
