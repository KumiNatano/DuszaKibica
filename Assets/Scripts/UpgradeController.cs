using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] bool isThisStoreLevel = false;
    [SerializeField] int Money = 0;

    [Header("GameObjects")]
    [SerializeField] Button LiveUpgradeButton = null;
    [SerializeField] Button StaminaUpgradeButton = null;
    [SerializeField] Button BuyPotionButton = null;
    [SerializeField] Button FuryButton = null;
    [SerializeField] Button ImmortalButton = null;
    [SerializeField] Button KnifeButton = null;
    [SerializeField] TMP_Text MoneyText = null;
    [SerializeField] GameObject UI = null;

    [Header("Live")]
    public int LiveUpgradeLevel = 0;
    [SerializeField] int LiveUpgradeLevelMax = 0;
    [SerializeField] int LiveUpgradePrice = 0;
    public int SingleLiveBonus = 20;

    [Header("Stamina")]
    public int StaminaUpgradeLevel = 0;
    [SerializeField] int StaminaUpgradeLevelMax = 0;
    [SerializeField] int StaminaUpgradePrice = 0;
    public int SingleStaminaBonus = 20;

    [Header("Potions")]
    public int PotionAmount = 0;
    [SerializeField] int PotionAmountMax = 0;
    [SerializeField] int PotionPrice = 0;

    [Header("Upgrades")]
    public bool Fury = false;
    public bool Immortal = false;
    public bool Knife = false;

    [SerializeField] int FuryPrice = 0;
    [SerializeField] int ImmortalPrice = 0;
    [SerializeField] int KnifePrice = 0;


    private static UpgradeController instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }



    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += preparationsForNewScene;

        if (isThisStoreLevel == true)
        {
            LiveUpgradeButton = GameObject.Find("LiveUpgradeButton").GetComponent<Button>();
            StaminaUpgradeButton = GameObject.Find("StaminaUpgradeButton").GetComponent<Button>();
            BuyPotionButton = GameObject.Find("BuyPotionButton").GetComponent<Button>();
            FuryButton = GameObject.Find("FuryButton").GetComponent<Button>();
            ImmortalButton = GameObject.Find("ImmortalButton").GetComponent<Button>();
            KnifeButton = GameObject.Find("KnifeButton").GetComponent<Button>();
        }
        else
        {
            UI = GameObject.Find("UI");
            refreshUI();
        }

        MoneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();

        updateMoneyText();

        

    }

    void refreshUI()
    {
        GameObject abilities = UI.transform.Find("Abilities").gameObject;

        GameObject knifeImage = null;
        GameObject FuryImage = null;
        GameObject immortalImage = null;

        if (Knife)
        {
            knifeImage = abilities.transform.Find("Knife").gameObject;
            knifeImage.SetActive(true);
        }

        if (Fury)
        {
            FuryImage = abilities.transform.Find("Fury").gameObject;
            FuryImage.SetActive(true);
        }

        if (Immortal)
        {
            immortalImage = abilities.transform.Find("Immortal").gameObject;
            immortalImage.SetActive(true);
        }

    }

    void preparationsForNewScene(Scene scene, LoadSceneMode mode)
    {
        if (isThisStoreLevel == true)
        {
            LiveUpgradeButton = GameObject.Find("LiveUpgradeButton").GetComponent<Button>();
            LiveUpgradeButton.onClick.AddListener(buyLiveUpgrade);

            StaminaUpgradeButton = GameObject.Find("StaminaUpgradeButton").GetComponent<Button>();
            StaminaUpgradeButton.onClick.AddListener(buyStaminaUpgrade);

            BuyPotionButton = GameObject.Find("BuyPotionButton").GetComponent<Button>();
            BuyPotionButton.onClick.AddListener(buyPotion);

            FuryButton = GameObject.Find("FuryButton").GetComponent<Button>();
            FuryButton.onClick.AddListener(buyFury);

            ImmortalButton = GameObject.Find("ImmortalButton").GetComponent<Button>();
            ImmortalButton.onClick.AddListener(buyImmortal);

            KnifeButton = GameObject.Find("KnifeButton").GetComponent<Button>();
            KnifeButton.onClick.AddListener(buyKnife);
        }

        MoneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();

        updateMoneyText();
    }

    public void goingToNextLevel()
    {
        isThisStoreLevel = !isThisStoreLevel;
    }

    void updateMoneyText()
    {
        MoneyText.text = "Ilosc szalikow: " + Money;
    }

    public void buyLiveUpgrade()
    {
        if(Money >= LiveUpgradePrice && LiveUpgradeLevel < LiveUpgradeLevelMax)
        {
            LiveUpgradeLevel += 1;
            Money -= LiveUpgradePrice;
            updateMoneyText();
        }
    }

    public void buyStaminaUpgrade()
    {
        if(Money >= StaminaUpgradePrice && StaminaUpgradeLevel < StaminaUpgradeLevelMax)
        {
            StaminaUpgradeLevel += 1;
            Money -= StaminaUpgradePrice;
            updateMoneyText();
        }
    }

    public void buyPotion() 
    {
        if (Money >= PotionPrice && PotionAmount < PotionAmountMax)
        {
            PotionAmount += 1;
            Money -= PotionPrice;
            updateMoneyText();
        }

    }

    public void buyFury() {
        if (Money >= FuryPrice && Fury == false)
        {
            Fury = true; 
            Money -= FuryPrice;
            updateMoneyText();
        }
    }

    public void buyImmortal() { 
        if(Money > ImmortalPrice && Immortal == false)
        {
            Immortal = true;
            Money -= ImmortalPrice;
            updateMoneyText();
        }
    
    }
        

    public void buyKnife() {

        if (Money > KnifePrice && Knife == false)
        {
            Knife = true;
            Money -= KnifePrice;
            updateMoneyText();
        }
    }


}
