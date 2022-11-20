using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int healthAmount;
    private int maxHealtAmount=100;
    private bool isAlive;
    public BarParent hpBar;
    // Start is called before the first frame update
    void Start()
    {
        healthAmount = maxHealtAmount;
        isAlive = true;
        hpBar.SetMax(maxHealtAmount);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int dmg)
    {
        if(healthAmount-dmg >= 0)
        {
            healthAmount -= dmg;
        }
        else
        {
            healthAmount = 0;
        }
        hpBar.Change(healthAmount);
        if (healthAmount == 0)
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
        hpBar.Change(healthAmount);
    }
    public bool CheckIfAlive()
    {
        return isAlive;
    }
}
