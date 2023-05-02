using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; //statut actuel du jeu : jeu en pause ou non, jeu pas en pause par défaut

    public GameObject pauseMenuUI;

    //public GameObject settingsWindow;

    void Update()
    {
        if (Input.GetButtonDown("Cancel")) //touche échap + bouton start xbox
        {
            if (gameIsPaused)
            {
                Resume(); //si jeu en pause, reprendre cours normal du jeu
            }
            else
            {
                Paused(); //Si pas en pause, on active le menu pause
            }
        }
    }

    void Paused()
    {
        PlayerController.instance.enabled = false; //le joueur ne se déplace plus
        pauseMenuUI.SetActive(true); // afficher menu à l'écran
        Time.timeScale = 0; // freeze le temps
        gameIsPaused = true; //jeu se met en pause
    }

    public void Resume()
    {
        PlayerController.instance.enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    /*public void OpenSettingsWindow()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingsWindow.SetActive(false);
    }*/

    public void LoadMainMenu()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}