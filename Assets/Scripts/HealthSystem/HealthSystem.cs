using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

[Obsolete("HealthSystem will be removed. Use LivingMixin instead")]
public class HealthSystem : MonoBehaviour
{
    public int healthAmount;
    public int maxHealthAmount = 100;
    [SerializeField] private bool isAlive;
    [SerializeField] public bool isImmortal;
    [SerializeField] double immortalTime;
    [SerializeField] double immortalCooldown;
    public BarParent hpBar;
    //public EnemyHealthBar OldenemyHealthbar;
    public EnemyDrop drop;

    [SerializeField] GameObject EnemyHealthBarObject;
    [SerializeField] Slider EnemyHPSlider;

    [SerializeField] UpgradeController upgradeController;
    private EnemyAnimationsAndModel enemyAnimationsAndModel;
    [SerializeField] private Canvas hpBarCanvas;

    [SerializeField] private GameObject enemyWeapon;
    [SerializeField] private VisualEffect BloodEffect;
    
    // Start is called before the first frame update
    void Start()
    {

        enemyAnimationsAndModel = this.gameObject.GetComponent<EnemyAnimationsAndModel>();
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
            if (this.tag == "player")
            {
                StartCoroutine(GetComponent<ScreenBloodController>().StartBloodScreen(0f, 1f, 1f));     
            }
            healthAmount -= dmg;
            if (this.tag != "player") {
                    //OldenemyHealthbar.SetHealth(getHealthAmount(), getMaxHealthAmount());
                    EnemyHealthBarObject.SetActive(true);
                    EnemyHPSlider.value = (float) healthAmount / (float) maxHealthAmount;
                    BloodEffect.playRate = 2;
                    BloodEffect.Play();

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
            Ragdoll rd = GetComponentInChildren<Ragdoll>();
            if (rd != null)
            {
                rd.Activate();
                rd.Push(Player.main.bodyForward * 1000f);
            }
            else
            {
                enemyAnimationsAndModel.PlayDeathAnim();
            }
            afterDeath();
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

    private void afterDeath()
    {
        this.gameObject.GetComponent<AIDestinationSetter>().enabled = false;
        this.gameObject.GetComponent<AIPath>().enabled = false;
        this.gameObject.GetComponent<EnemyDetectPlayer>().enabled = false;
        this.gameObject.GetComponent<EnemyDrop>().enabled = false;
        this.gameObject.GetComponent<EnemyAnimationsAndModel>().enabled = false;
        this.gameObject.GetComponent<WalkingAnimationBehaviour>().enabled = false;
        GetComponentInChildren<EnemyWeapon>().enabled = false;

        hpBarCanvas.enabled = false;
        enemyWeapon.SetActive(false);
        
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        
        this.gameObject.GetComponent<HealthSystem>().enabled = false;
    }
}
