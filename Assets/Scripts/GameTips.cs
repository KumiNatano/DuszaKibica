using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour
{
    public static bool blockCursor = false;
    public static bool isShowing = false;

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(inputTip.savename, 0);
        PlayerPrefs.SetInt(fightTip.savename, 0);
        PlayerPrefs.SetInt(handTip.savename, 0);
        PlayerPrefs.SetInt(kebabTip.savename, 0);
    }
    void FixedUpdate()
    {
        if (itpDirty)
        {
            return;
        }
        if (CheckInput())
        {
            StartCoroutine(TipProcedure(inputTip));
        }
        if (CheckFight())
        {
            StartCoroutine(TipProcedure(fightTip));
        }
        if (CheckHand())
        {
            StartCoroutine(TipProcedure(handTip));
        }
        if (CheckKebab())
        {
            StartCoroutine(TipProcedure(kebabTip));
        }
    }

    bool CheckInput()
    {
        if (inputTip.wasShown)
        {
            return false;
        }
        return true;
    }
    bool CheckFight()
    {
        if (fightTip.wasShown)
        {
            return false;
        }
        return ArenaManager.main.areas[ArenaManager.main.actualArena].activateArena;
    }
    bool CheckHand()
    {
        if (handTip.wasShown || !fightTip.wasShown)
        {
            return false;
        }
        var atk = Player.main.attack;
        var lc = atk.leftArm.punchCount;
        var rc = atk.rightArm.punchCount;
        return Math.Abs(lc - rc) >= 5;
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
    bool CheckInputs(string[] keynames)
    {
        foreach(string kn in keynames)
        {
            if (Input.GetButton(kn))
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator TipProcedure(GameTip tip)
    {
        if (tip.wasShown)
        {
            yield break;
        }
        blockCursor = tip.blockCursor;
        isShowing = true;
        itpDirty = true;
        PauseManager.ResumeGame();
        PauseManager.dirty = true;
        PauseManager.isPaused = true;
        if (tip.stopTime)
        {
            Time.timeScale = 0;
        }
        tip.Show();

        yield return new WaitForSecondsRealtime(tip.forcedDelay);
        yield return new WaitUntil(() => CheckInputs(tip.keynames));

        tip.Hide();
        PauseManager.isPaused = false;
        PauseManager.dirty = false;
        if (tip.stopTime)
        {
            Time.timeScale = 1;
        }
        isShowing = false;
        blockCursor = false;
        yield return new WaitForSeconds(intertipCd);
        itpDirty = false;
    }


    bool itpDirty = false;

    [SerializeField] float intertipCd = 2f;
    [SerializeField] GameTip inputTip;
    [SerializeField] GameTip fightTip;
    [SerializeField] GameTip handTip;
    [SerializeField] GameTip kebabTip;
}
[Serializable]
public class GameTip
{
    public string[] keynames = new string[1] { "Invalid" };
    public string savename = "invalidtip";
    public bool stopTime = false;
    public bool blockCursor = false;
    public float forcedDelay = 0f;
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

