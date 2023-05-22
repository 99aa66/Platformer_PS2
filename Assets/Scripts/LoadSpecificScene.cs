using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private Animator fadeSystem;

    public AudioClip sucessSound;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(sucessSound, transform.position);
            StartCoroutine(loadNextScene());
        }
    }

    public IEnumerator loadNextScene()
    {
        LoadAndSaveData.instance.SaveData(); //enregistre les données lorsqu'on passe au niveau suivant
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
        fadeSystem.ResetTrigger("FadeIn");
    } 
}