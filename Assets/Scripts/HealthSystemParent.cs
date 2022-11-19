using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtSystemParent : MonoBehaviour
{
    private int healthAmount;
    private int maxHealtAmount;
    private bool isAlive;
    public HealthBarPlayer hpBar;
    // Start is called before the first frame update
    void Start()
    {
        healthAmount = maxHealtAmount;
        isAlive = true;
        hpBar.SetMaxHealth(maxHealtAmount);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        healthAmount -= dmg;
        if (healthAmount <= 0)
        {
            isAlive = false;
        }
    }
    public void Heal(int heal)
    {
        healthAmount += heal;
        if (healthAmount > maxHealtAmount)
        {
            healthAmount = maxHealtAmount;
        }

    }
}
