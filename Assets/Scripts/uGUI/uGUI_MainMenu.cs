using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class uGUI_MainMenu : MonoBehaviour
{
    [SerializeField] string firstScene;

    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject CreditsPanel;

    public void OnPlay()
    {
        SceneManager.LoadScene(firstScene);
    }

    public void OnQuit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void OnResume()
    {

    }

    public void EnterCredits()
    {
        ToggleTab(MainMenu, false);
        ToggleTab(CreditsPanel, true);
    }

    public void QuitCredits()
    {
        ToggleTab(MainMenu, true);
        ToggleTab(CreditsPanel, false);
    }

    public void EnterSettings()
    {
        ToggleTab(MainMenu, false);
        ToggleTab(SettingsMenu, true);
    }

    public void QuitSettings()
    {
        ToggleTab(MainMenu, true);
        ToggleTab(SettingsMenu, false);
    }
    void ToggleTab(GameObject tab, bool value)
    {
        if (tab != null)
        {
            tab.SetActive(value);
        }
    }
}
