using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    [SerializeField] private int maxHealtAmount=100;
    [SerializeField] private bool isAlive;
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

        if (this.tag == "player" && healthAmount <= 0)
        {
            this.GetComponent<PlayerDeathManager>().PlayerDeath();
        }

        else if (healthAmount <= 0)
        {
            isAlive = false;
            Destroy(this.gameObject); //dodalem zniszczenie obiektu po smierci - Jacek
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
