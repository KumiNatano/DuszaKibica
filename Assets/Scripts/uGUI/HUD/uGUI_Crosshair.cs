using System;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_Crosshair : MonoBehaviour
{
    public uGUI_CrosshairArch leftArch;
    public uGUI_CrosshairArch rightArch;


    [Header("Slider")]
    public bool updateRestSlider = true;
    public bool updateAttackSlider = true;
    [Header("Idle")]
    public Color idleSliderColor = Color.white;
    public Color idleBackColor = Color.black;
    [Header("Rest")]
    public Color restSliderColor = Color.yellow;
    public Color restBackColor = Color.black;
    [Header("Attack")]
    public Color attackSliderColor = Color.red;
    public Color attackBackColor = Color.black;


    void UpdateColorArmL(PunchMachineState newState) => UpdateColorArm(leftArch, newState);
    void UpdateColorArmR(PunchMachineState newState) => UpdateColorArm(rightArch, newState);
    void UpdateColorArm(uGUI_CrosshairArch arch, PunchMachineState state)
    {
        Color slider;
        Color back;
        switch(state)
        {
            case PunchMachineState.Idle:
                slider = idleSliderColor;
                back = idleBackColor;
                break;
            case PunchMachineState.Attack:
                slider = attackSliderColor;
                back = attackBackColor;
                break;
            case PunchMachineState.Rest:
                slider = restSliderColor;
                back = restBackColor;
                break;
            default:
                throw new NotImplementedException();
        }
        arch.SetSliderColor(slider);
        arch.SetBackColor(back);
    }
    void UpdateSliderArmL() => UpdateSliderArm(leftArch, atkModule.leftArm);
    void UpdateSliderArmR() => UpdateSliderArm(rightArch, atkModule.rightArm);
    void UpdateSliderArm(uGUI_CrosshairArch arch, PlayerPunchMachine arm)
    {
        float value = 1;
        if (arm.isAttacking && updateAttackSlider)
        {
            value = arm.attackFraction;
        }
        else if (arm.isResting && updateRestSlider)
        {
            value = 1 - arm.restFraction;
        }
        arch.SetSliderValue(value);
    }

    void Start()
    {
        atkModule = Player.main.GetModule<PlayerAttack>();

        atkModule.leftArm.onStateUpdate += UpdateColorArmL;
        atkModule.rightArm.onStateUpdate += UpdateColorArmR;

        atkModule.leftArm.onAttackStay += UpdateSliderArmL;
        atkModule.leftArm.onAttackEnd += UpdateSliderArmL;
        atkModule.leftArm.onRestStay += UpdateSliderArmL;
        atkModule.leftArm.onRestEnd += UpdateSliderArmL;

        atkModule.rightArm.onAttackStay += UpdateSliderArmR;
        atkModule.rightArm.onAttackEnd += UpdateSliderArmR;
        atkModule.rightArm.onRestStay += UpdateSliderArmR;
        atkModule.rightArm.onRestEnd += UpdateSliderArmR;
    }


    PlayerAttack atkModule;
}
[Serializable]
public class uGUI_CrosshairArch
{
    public Image background;
    public Image slider;

    public void SetSliderColor(Color color)
    {
        slider.color = color;
    }
    public void SetBackColor(Color color)
    {
        background.color = color;
    }
    public void SetSliderValue(float value)
    {
        Vector3 scale = Vector3.one;
        scale.y = value;
        slider.rectTransform.localScale = scale;
    }
}
