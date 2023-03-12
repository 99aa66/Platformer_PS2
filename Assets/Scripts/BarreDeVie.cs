
using UnityEngine;
using UnityEngine.UI;
public class BarreDeVie : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int health)
    {
        // met la vie du joueur a 100 pourcent, quand le jeu demarre le joueur a 100 pourcent de ses points de vie
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        // indique le nombre de points de vie a afficher
        slider.value = health;
    }
}
