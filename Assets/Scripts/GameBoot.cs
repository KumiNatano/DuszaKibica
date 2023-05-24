using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBoot : MonoBehaviour
{
    private IEnumerator Start()
    {
        var essentials = SceneManager.LoadSceneAsync("Essentials", LoadSceneMode.Additive);
        var menu = SceneManager.LoadSceneAsync("XMenu", LoadSceneMode.Additive);

        menu.allowSceneActivation = false;

        while (!menu.isDone || !essentials.isDone)
        {
            if (menu.progress >= 0.9f){
                menu.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Boot");
    }
}
