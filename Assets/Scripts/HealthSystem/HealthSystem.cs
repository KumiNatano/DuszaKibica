using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Obsolete("HealthSystem will be removed. Use LivingMixin instead")]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] int healthAmount;
    [SerializeField] private int maxHealthAmount = 100;
    [SerializeField] private bool isAlive;
    [SerializeField] bool isImmortal;
    [SerializeField] double immortalTime;
    [SerializeField] double immortalCooldown;
    public BarParent hpBar;
    //public EnemyHealthBar OldenemyHealthbar;
    public EnemyDrop drop;

    [SerializeField] GameObject EnemyHealthBarObject;
    [SerializeField] Slider EnemyHPSlider;

    [SerializeField] UpgradeController upgradeController;

    // Start is called before the first frame update
    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>(); //z upgrade controllera bierze bonus do zycia
        if(upgradeController.LiveUpgradeLevel != 0)
        {
            maxHealthAmount = maxHealthAmount + (upgradeController.LiveUpgradeLevel * upgradeController.SingleLiveBonus);
        }

        healthAmount = maxHealthAmount;
        isAlive = true;
        hpBar.SetMax(maxHealthAmount);

        if(this.gameObject.tag == "enemy")
        {
            EnemyHPSlider.value = maxHealthAmount;
            EnemyHealthBarObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "enemy")
        {
            //print(EnemyHPSlider.enabled);
        }
        manageImmortality();
    }

    public void TakeDamage(int dmg)
    {
        if (!isImmortal)
        {
        if(healthAmount-dmg >= 0)
        {
            healthAmount -= dmg;
            if (this.tag != "player") {
                    //OldenemyHealthbar.SetHealth(getHealthAmount(), getMaxHealthAmount());
                    EnemyHealthBarObject.SetActive(true);
                    EnemyHPSlider.value = (float) healthAmount / (float) maxHealthAmount;

            }
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


    /*private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "knife" && this.tag == "enemy")
        {
            TakeDamage(other.GetComponent<KnifeBehaviour>().knifeDamage);
        }
    }*/
    
    public void setImmortal(double iTime, double cTime)
    {
        if(immortalCooldown > 0 || immortalTime > 0)
        {
            return;
        }
        immortalTime = iTime;
        immortalCooldown = cTime;
        isImmortal = true;

    }
    private void manageImmortality()
    {
        if (immortalTime > 0)
        {
            immortalTime -= Time.deltaTime;
        }
        if (isImmortal && immortalTime <= 0)
        {
            isImmortal = false;
            immortalTime = 0.0;
        }
        if (immortalCooldown > 0 && immortalTime <= 0)
        {
            immortalCooldown -= Time.deltaTime;

        }
    }
}
