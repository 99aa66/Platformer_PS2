using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Animator fadeSystem;
    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) //skip les credits
        {
            StartCoroutine(loadSceneMenu());
        }
    }
    public IEnumerator loadSceneMenu()
    {
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        LoadMainMenu();
        fadeSystem.ResetTrigger("FadeIn");
    }
}