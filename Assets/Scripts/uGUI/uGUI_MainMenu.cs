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
        MainMenu.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void QuitCredits()
    {
        MainMenu.SetActive(true);
        CreditsPanel.SetActive(false);
    }

    public void EnterSettings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void QuitSettings()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
