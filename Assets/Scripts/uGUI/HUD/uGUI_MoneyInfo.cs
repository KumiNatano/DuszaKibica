using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_MoneyInfo : MonoBehaviour
{
    public float dingLength = 0.25f;
    public float dingSize = 0.4f;
    public AnimationCurve dingCurve;

    public void UpdateInfo(int newAmount)
    {
        if (israni)
        {
            dirty = true;
        }
        display.text = newAmount.ToString();
        StartCoroutine(DingSeq());
    }

    IEnumerator DingSeq()
    {
        israni = true;
        float timer = 0f;
        while (timer < dingLength)
        {
            if (dirty)
            {
                dirty = false;
                yield break;
            }
            timer += Time.deltaTime;
            icon.rectTransform.localScale = Vector3.one + Vector3.one * dingCurve.Evaluate(timer / dingLength) * dingSize;
            yield return new WaitForEndOfFrame();
        }
        icon.rectTransform.localScale = Vector3.one;
        israni = false;
    }

    bool israni = false;
    bool dirty = false;

    [SerializeField] TMP_Text display;
    [SerializeField] Image icon;
}
