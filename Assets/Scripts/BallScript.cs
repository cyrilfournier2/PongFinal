using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 *  Script attaché sur la Ball permettant de gérer toutes ces collisions 
 */
public class BallScript : MonoBehaviour {
    //Vitesse initiale de la balle
    public float speed = 4.0f;
    //RigidBody De la ball
    private Rigidbody rb;
    // Material permettant de gerer les autres materiaux
    private Material mat;
    // Matérial avec l'image feu
    public Material feu;
    // Material avec l'image eau
    public Material eau;
    // Effect de particule quand la balle tape un material eau
    public GameObject feubleu;
    // Effect de particule lorsque le joueur perds
    public GameObject explosion;
    // Effect de particule lorsque la ball touche le feu
    public GameObject feurouge;
    // Caméra principale attachée au player
    public Camera maincam;
    // Score du joueur
    public static int PlayerScore = 0;
    // Texte a afficher pour modifier le score
    public Text scoreText;
    // Permet de savoir l'etat du jeu, si il est commencé ou pas
    private bool inGame;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        var vel = rb.velocity;
        vel.y = speed;
        vel.x = speed;
        vel.z = speed;
        rb.velocity = vel;
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update () {
        // Permet de faire accelerer la balle si elle reste trop longtemps sur un axe
        if (inGame)
        {
            var vel = rb.velocity;
            if (Mathf.Abs(vel.x) < 1.0f)
                vel.x += 1.0f;
            if (Mathf.Abs(vel.z) < 1.0f)
                vel.z += 1.0f;
            if (Mathf.Abs(vel.y) < 1.0f)
                vel.y += 1.0f;
            rb.velocity = vel;
        }    
    }

    /**
     * Méthode de lancement de la balle 
     */
    void GoBall()
    {
        inGame = true;
        var vel = rb.velocity;
        vel.y = speed;
        vel.x = speed;
        vel.z = speed;
        rb.velocity = vel;
        mat = GetComponent<MeshRenderer>().material;
    }

    /*
     * Méthode qui définit l'action à réaliser suivant la collision rencontrée par la balle
     */ 
    void OnCollisionEnter(Collision col)
    {
        // La balle prend l'element du mur qu'elle touche    
        if (col.gameObject.name == "MurGauche")
        {
            mat = eau;
            GetComponent<MeshRenderer>().material = mat;
        }
        if (col.gameObject.name == "Sol")
        {
            mat = eau;
            GetComponent<MeshRenderer>().material = mat;
        }
        if (col.gameObject.name == "Plafond")
        {
            mat = feu;
            GetComponent<MeshRenderer>().material = mat;
        }
        if (col.gameObject.name == "MurDroit")
        {
            mat = feu;
            GetComponent<MeshRenderer>().material = mat;
        }
        // Si la balle touche le mur du fond, le joueur à perdu et la partie recommence
        if (col.gameObject.name == "MurFond")
        {
            RestartGame();
        }
        // Coordonnées du point de collision 
        Vector3 pointcol = col.contacts[0].point;
        var rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        // Action si collision avec le player
        if (col.gameObject.name == "Player")
        {
            Material mat1 = GameObject.Find("Player").GetComponent<MeshRenderer>().material;
            Material mat2 = GetComponent<MeshRenderer>().material;      
            // Si la balle n'est pas du meme element que le player, le player perd
            if (mat1.name != mat2.name)
            {
                // Animation d'Explosion au point de collision
                var animation = (GameObject)Instantiate(explosion, pointcol, rotation);
                // La camera tremble
                maincam.GetComponent<CameraShake>().shakeDuration = 1;
                // Méthode restart : Le jeu reprend à zero
                RestartGame();
            }
            // Si meme matériaux
            else
            {
                // Méthode score
                Score();
                // Accéleration de la balle
                var vel = rb.velocity;
                vel.x += 1.0f;
                rb.velocity = vel;
                // Petit tremblement de la caméra
                maincam.GetComponent<CameraShake>().shakeDuration = 0.1F;
            }
        }

        // Si la balle tape le mur du fond le player a perdu
        if( col.gameObject.name == "MurFond")
        {
            // ANimation d'explosion
            var animation = (GameObject)Instantiate(explosion, pointcol, rotation);
            // Tremblement de la camera
            maincam.GetComponent<CameraShake>().shakeDuration = 1;
        }
        // SI ce n'est pas le mur du fond, animation de feu rouge ou bleu
        else
        {
            if (mat == feu)
            {
                var animation = (GameObject)Instantiate(feurouge, pointcol, rotation);
            }
            else if (mat == eau)
            {
                var animation = (GameObject)Instantiate(feubleu, pointcol, rotation);
                
            }
        }
    }

    /**
     * Méthode de remise à zero de la balle 
     */

     void ResetBall()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0F, 1F, 0F);

        Debug.Log("Resetball " + rb.velocity);
        StartCoroutine(StartLoader());
    }

    /*
     * Méthode d'attente avant de reprendre la partie
     */ 
    IEnumerator StartLoader()
    {
        Debug.Log("Startloader 1");
        yield return new WaitForSeconds(3);

        Debug.Log("Startloader 2");
        ResetScore();
        Invoke("GoBall", 1);  
    }

    /*
     * Méthode de reset du score
     */ 
    public void ResetScore()
    {
        PlayerScore = 0;
        scoreText.text = "Score : " + PlayerScore;
    }

    /*
     * Méthode d'augmentation du score
     */ 
    public void Score()
    {
        PlayerScore++;
        scoreText.text = "Score : " + PlayerScore;
    }
    /*
     * Méthode de remise à zero du jeux, HUD compris
     */
   public void RestartGame()
    {
        inGame = false;
         ResetBall();
    }
}
