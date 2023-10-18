using UnityEngine;
using TMPro;

//Singleton references
public class KillCounter : MonoBehaviour
{
    public static KillCounter instance;

    public int enemyKillCount = 0;
    public TextMeshProUGUI killText;

    void Start()
    {
        instance = this;
        killText.text = enemyKillCount.ToString();
    }

    public void IncreaseEnemyKillCount()
    {
        enemyKillCount++;
        killText.text = enemyKillCount.ToString();
    }
}
