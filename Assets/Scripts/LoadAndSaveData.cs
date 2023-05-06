using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la scène");
            return;
        }

        instance = this;
    }


    public void SaveData()
    {
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1)) //on veut rejouer à d'anciens niveaux, la condition vérifie si le niveau qu'on vient de terminer n'a pas encore été atteint, valeur par défaut 1 donc 1er niveau
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock); //on enregistre le niveau actuel, comme étant celui le plus loin atteint
        }
    }
}
