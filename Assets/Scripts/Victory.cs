using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public GameObject VictoryAnimationContainer;
    public Animator victoryAnimator;

    public string sceneName;
    public AudioClip achieveSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(achieveSound, transform.position);
            StartCoroutine(PlayVictoryAnimation());
        }
    }
    private IEnumerator PlayVictoryAnimation()
    {
        VictoryAnimationContainer.SetActive(true); // Afficher le canvas pour jouer l'animation
        victoryAnimator.Play("Victory");

        yield return new WaitForSeconds(victoryAnimator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneName);
    }
}
