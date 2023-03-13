using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //Dynamisme de la barre de vie en visuel graphique
    public Gradient gradient;
    public Image fill;

   //Initialisation des points de vie au début du jeu, 100% de vie
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //ref couleur fill, 1f = 100%, gradient tout à droite de la barre de valeur 1
        fill.color = gradient.Evaluate(1f);
    }

    //Nombre de points de vie à afficher sur l'écran
    public void SetHealth(int health)
    {
        slider.value = health;

        //valeur dans le slider = 0 et 100 donc on transforme cette valeur entre 0 et 1 
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
