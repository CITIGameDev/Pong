using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D bola;
    public float xInitialForce;
    public float yInitialForce;
    private Vector2 trajectoryOrigin;
    private float spd = 5.0f;
    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        bola.velocity = Vector2.zero;
    }
    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(0 , 2);

        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya di bawah 1, bola bergerak ke kiri. 
        // Jika tidak, bola bergerak ke kanan.
        if (randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakkan bola ini.
            bola.AddForce(new Vector2(-xInitialForce, yRandomInitialForce));

        }
        else
        {
            bola.AddForce(new Vector2(xInitialForce, yRandomInitialForce));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }
    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;

        // Mulai game
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
