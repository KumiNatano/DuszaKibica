using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt(kebabTip.savename, 0);
    }
    void FixedUpdate()
    {
        if (itpDirty)
        {
            return;
        }
        if (CheckKebab())
        {
            StartCoroutine(TipProcedure(kebabTip));
        }
    }

    bool CheckKebab()
    {
        if (kebabTip.wasShown)
        {
            return false;
        }
        var hs = Player.main.GetComponent<HealthSystem>();
        return ((hs.healthAmount * 1.0f) / hs.maxHealthAmount) < 0.5f && hs.healthAmount > 0;
    }
    IEnumerator TipProcedure(GameTip tip)
    {
        if (tip.wasShown)
        {
            yield break;
        }
        itpDirty = true;
        PauseManager.dirty = true;
        PauseManager.ResumeGame();
        PauseManager.isPaused = true;
        Time.timeScale = 0;
        tip.Show();
        
        yield return new WaitUntil(() => Input.GetButton(tip.keyname));

        tip.Hide();
        PauseManager.isPaused = false;
        PauseManager.dirty = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(intertipCd);
        itpDirty = false;
    }


    bool itpDirty = false;

    [SerializeField] float intertipCd = 2f;
    [SerializeField] GameTip kebabTip;
}
[Serializable]
public class GameTip
{
    public string keyname = "Invalid";
    public string savename = "invalidtip";
    public bool wasShown => PlayerPrefs.GetInt(savename) == 1;
    public GameObject graphic;

    public void Show()
    {
        if (wasShown)
        {
            return;
        }
        graphic.SetActive(true);
        PlayerPrefs.SetInt(savename, 1);
    }
    public void Hide()
    {
        graphic.SetActive(false);
    }
}

