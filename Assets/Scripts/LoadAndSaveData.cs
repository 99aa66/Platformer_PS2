using UnityEngine;
using System.Linq;

public class LoadAndSaveData : MonoBehaviour
{
    public static LoadAndSaveData instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de LoadAndSaveData dans la sc�ne");
            return;
        }

        instance = this;
    }


    public void SaveData()
    {
        if (CurrentSceneManager.instance.levelToUnlock > PlayerPrefs.GetInt("levelReached", 1)) //on veut rejouer � d'anciens niveaux, la condition v�rifie si le niveau qu'on vient de terminer n'a pas encore �t� atteint, valeur par d�faut 1 donc 1er niveau
        {
            PlayerPrefs.SetInt("levelReached", CurrentSceneManager.instance.levelToUnlock); //on enregistre le niveau actuel, comme �tant celui le plus loin atteint
        }
    }
}
