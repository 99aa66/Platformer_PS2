using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath()
    {
        if(CurrentSceneManager.instance.isPlayerPresentByDefault) 
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad(); //récupère tous les objets et désactive le DontDestroyOnLoad
        }

        gameOverUI.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Recharger la scène
        PlayerHealth.instance.Respawn(); //replacer joueur au spawn
        gameOverUI.SetActive(false); 
    }

    public void MainMenuButton()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        SceneManager.LoadScene("MainMenu"); //Retour au menu principal
    }

    public void QuitButton()
    {
        Application.Quit(); //Fermer le jeu
    }
}