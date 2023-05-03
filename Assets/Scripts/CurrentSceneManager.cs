using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public Vector3 respawnPoint;

    public static CurrentSceneManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager dans la sc�ne");
            return;
        }

        instance = this;

        respawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position; //l'endroit o� le joueur commence
    }
}