using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1); //niveau atteint � chercher dans les playerrefs(sauvegarde ordi), le nom de la donn�e, valeur par d�faut = le premier niveau 

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached) //ne pas bloquer ce premier niveau
            {
                levelButtons[i].interactable = false; // bloquer le bouton donc acc�s au niveau
            }
        }
    }
    public void LoadLevelPassed(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
