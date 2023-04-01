using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barredeviecoq : MonoBehaviour
{

    public Slider slider;

    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health)
    {
        // met la vie du joueur a 100 pourcent, quand le jeu demarre le joueur a 100 pourcent de ses points de vie
        slider.maxValue = health;
        slider.value = health;

        //ref couleur fill, 1f = 100%, gradient tout à droite de la barre de valeur 1
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        // indique le nombre de points de vie a afficher
        slider.value = health;

        //valeur dans le slider = 0 et 100 donc on transforme cette valeur entre 0 et 1 
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
