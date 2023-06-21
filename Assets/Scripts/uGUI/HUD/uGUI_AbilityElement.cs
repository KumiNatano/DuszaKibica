using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_AbilityElement : MonoBehaviour
{
    [Header("Assets")]
    public Sprite idleSprite;
    public Sprite useSprite;
    public Sprite workSprite;
    public Sprite restSprite;
    [Header("Elements")]
    public Image icon;
    public TMP_Text timer;
    public BaseAbility abilityRef;


    void UpdateIcon(AbilityState state)
    {
        if (state == AbilityState.Idle && idleSprite != null)
        {
            icon.sprite = idleSprite;
        }
        else if (state == AbilityState.Use && useSprite != null)
        {
            icon.sprite = useSprite;
        }
        else if (state == AbilityState.Work && workSprite != null)
        {
            icon.sprite = workSprite;
        }
        else if (state == AbilityState.Rest && restSprite != null)
        {
            icon.sprite = restSprite;
        }
    }
    void UpdateTimer()
    {
        float num1 = abilityRef.restDuration;
        float num2 = abilityRef.restFraction;
        bool flag = true;

        sb.Append(num1 - num1 * num2);
        for (int i = 0; i < sb.Length; i++)
        {
            if (sb[i] == '.')
            {
                i += 2;
                sb.Remove(i, sb.Length - i);
                flag = false;
                break;
            }
        }
        if (flag)
        {
            sb.Append(".0");
        }
        sb.Append("s");
        timer.text = sb.ToString();
        sb.Clear();
    }
    void HideTimer()
    {
        timer.text = "";
    }

    private void Start()
    {
        abilityRef.onStateUpdate += UpdateIcon;
        abilityRef.onRestBegin += UpdateTimer;
        abilityRef.onRestStay += UpdateTimer;
        abilityRef.onRestEnd += HideTimer;
    }

    StringBuilder sb = new StringBuilder();
}
