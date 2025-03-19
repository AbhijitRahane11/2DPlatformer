using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealtBar;
    [SerializeField] private Image currenthealtBar;


    private void Start()
    {
        totalhealtBar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        currenthealtBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
