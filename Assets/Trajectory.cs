﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	public BallController ball;
	private Rigidbody2D ballRigidBody;
	private CircleCollider2D ballCollider;

	public GameObject ballAtCollision;

	// Start is called before the first frame update
	void Start()
	{
		ballRigidBody = ball.gameObject.GetComponent<Rigidbody2D>();
		ballCollider = ball.gameObject.GetComponent<CircleCollider2D>();

		ballAtCollision.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		bool drawBallAtCollision = false;
		Vector2 offsetHitPoint = new Vector2();

		RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigidBody.position, ballCollider.radius, ballRigidBody.velocity.normalized);

		foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
		{
			if (circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallController>() == null)
			{
				Vector2 hitPoint = circleCastHit2D.point;

				Vector2 hitNormal = circleCastHit2D.normal;

				offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

				DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

				if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
				{
					// Hitung vektor datang
					Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

					// Hitung vektor keluar
					Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

					// Hitung dot product dari outVector dan hitNormal. Digunakan supaya garis lintasan ketika 
					// terjadi tumbukan tidak digambar.
					float outDot = Vector2.Dot(outVector, hitNormal);
					if (outDot > -1.0f && outDot < 1.0)
					{
						// Gambar lintasan pantulannya
						DottedLine.DottedLine.Instance.DrawDottedLine(
								offsetHitPoint,
								offsetHitPoint + outVector * 10.0f);

						// Untuk menggambar bola "bayangan" di prediksi titik tumbukan
						drawBallAtCollision = true;
					}
				}

				// Hanya gambar lintasan untuk satu titik tumbukan, jadi keluar dari loop
				break;
			}
		}

		if (drawBallAtCollision)
		{
			// Gambar bola "bayangan" di prediksi titik tumbukan
			ballAtCollision.transform.position = offsetHitPoint;
			ballAtCollision.SetActive(true);
		}
		else
		{
			// Sembunyikan bola "bayangan"
			ballAtCollision.SetActive(false);
		}
	}
}