using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public enemyAttack playersHealth;
    public Image fillImage;
    public float fillValue = 100f;
    public Slider slider;
    
    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 100f;
    }
    
    public void UpdatePlayerHealth(float health)
    {
        slider.value = health;
    }
}
