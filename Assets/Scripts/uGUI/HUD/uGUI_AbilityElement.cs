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

    private void Start()
    {
        abilityRef.onStateUpdate += UpdateIcon;
    }
}
