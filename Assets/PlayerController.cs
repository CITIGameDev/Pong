using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float spd = 10.0f;
    public float batas = 9.0f;
    private Rigidbody2D raket;
    private int skor;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    private ContactPoint2D lastContactPoint;

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }


    void Start()
    {
        raket = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector2 velocity = raket.velocity;
        if (Input.GetKey(upButton))
        {

            velocity.y = spd;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -spd;
        }
        else
        {
            velocity.y = 0.0f;
        }
        raket.velocity = velocity;

        Vector3 posisi = transform.position;
        if (posisi.y > batas)
        {
            posisi.y = batas;
        }
        else if (posisi.y < -batas)
        {
            posisi.y = -batas;
        }
        transform.position = posisi;
    }
    public void skornaik()
    {
        skor++;
    }
    public void resetskor()
    {
        skor = 0;
    }
    public int Skor
    {
        get { return skor; }
    }
}
