using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour 
{
    public GameObject optionsManager;

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
        if (sceneName == "Gameplay")
            optionsManager.GetComponent<OptionsManager>().CheckOptions();
    }

    public void LoadSceneWithConfirmation(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
