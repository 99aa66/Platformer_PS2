using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyHealthTest : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;
    public Barredeviecoq healthBar;
    [SerializeField] GameObject HEALTHBAR;

    // Start is called before the first frame update
    void Start()
    {
        HEALTHBAR.SetActive(false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter2D(Collider2D Player)
    {

        if (Player.transform.name == "Player")
        {
            TakeDamage(25);
            StartCoroutine(ShowBar());
            Debug.Log("perso touche boite");
        }

    }
    private IEnumerator ShowBar()
    {
        HEALTHBAR.SetActive(true);
        yield return new WaitForSeconds(5f);
        HEALTHBAR.SetActive(false);
    }
}
