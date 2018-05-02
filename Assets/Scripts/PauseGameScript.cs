using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Script pour la mise en pause du jeu  
 */
public class PauseGameScript : MonoBehaviour
{
    // Text du bouton à modifier
    public Text pausetext;

    // Méthode de freeze du temps
    public void pauseGame()
    {
        //Temps active -> mise en pause
         if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        pausetext.text = "REPRENDRE";
             
        }
         // Reprise du temps
        else
        {
            Time.timeScale = 1;
        pausetext.text = "PAUSE";
        }
    }

}
