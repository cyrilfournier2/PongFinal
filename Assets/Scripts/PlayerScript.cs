using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Script attaché au joueur
 *
 */
public class PlayerScript : MonoBehaviour
{
    // Touches pour faire bouger la palette
    public KeyCode moveUp = KeyCode.Z;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.Q;
    public KeyCode moveRight = KeyCode.D;
    // Limites de deplacement du player
    public float restriction_v_p = 2.2F;
    public float restriction_v_n = -0.6F;
    public float restriction_horizontale = 1.65f;
    // VItesse de deplacement du player
    public float speed = 10.0f;
    // RigidBody du player
    private Rigidbody rb;
    // Matériaux feu et eau
    public Material mat;
    public Material feu;
    public Material eau;
    // La balle
    public GameObject balle;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        mat = eau;
    }

    // Update is called once per frame
    void Update()
    {
        //Deplacements de la palette
        var vel = rb.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mat == feu)
            {
                mat = eau;
            }
            else if (mat == eau)
            {
                mat = feu;
            }
            GetComponent<MeshRenderer>().material = mat;
        }
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else if (Input.GetKey(moveLeft))
        {
            vel.z = -speed;
        }
        else if (Input.GetKey(moveRight))
        {
            vel.z = speed;
        }

        else if (!Input.anyKey)
        {
            vel.y = 0;
            vel.z = 0;
        }
        rb.velocity = vel;

        // Restrictions de deplacement de la palette
        var pos = transform.position;
        if (pos.y > restriction_v_p)
        {
            pos.y = restriction_v_p;
            GetComponent<MeshRenderer>().material = feu;
        }
        else if (pos.y < -restriction_v_n)
        {
            pos.y = -restriction_v_n;
            GetComponent<MeshRenderer>().material = eau;
        }
        if (pos.z > restriction_horizontale)
        {
            pos.z = restriction_horizontale;
            GetComponent<MeshRenderer>().material = feu;
        }
        else if (pos.z < -restriction_horizontale)
        {
            pos.z = -restriction_horizontale;
            GetComponent<MeshRenderer>().material = eau;
        }
        transform.position = pos;
    }
}
