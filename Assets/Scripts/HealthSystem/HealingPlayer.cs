using UnityEngine;

public class HealingPlayer : MonoBehaviour
{
    public int healingValue = 30;
    HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = FindObjectOfType<PlayerManagment>().health;
    }

    private void OnTriggerEnter(Collider other)
    {
        healthSystem.Heal(healingValue);
        gameObject.SetActive(false);
    }

}


   
