using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] int healthAmount;
    [SerializeField] private int maxHealthAmount = 100;
    [SerializeField] private bool isAlive;
    public BarParent hpBar;
    public EnemyDrop drop;
    // Start is called before the first frame update
    void Start()
    {
        healthAmount = maxHealthAmount;
        isAlive = true;
        hpBar.SetMax(maxHealthAmount);
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
            this.drop.DropLottery();
            Destroy(this.gameObject); //dodalem zniszczenie obiektu po smierci - Jacek
        }
    }
    public void Heal(int heal)
    {
        healthAmount += heal;
        if (healthAmount > maxHealthAmount)
        {
            healthAmount = maxHealthAmount;
        }
        hpBar.Change(healthAmount);
    }
    public bool CheckIfAlive()
    {
        return isAlive;
    }
    public int getHealthAmount(){
        return healthAmount;
    }
    public void setMaxHealthAmount(int value) {
        maxHealthAmount = value;
    }
    public int getMaxHealthAmount() {
        return maxHealthAmount;
    }
}
