using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool dash = false;

	float originalRunSpeed; // Para armazenar a velocidade original

	//bool dashAxis = false;
	
	// Update is called once per frame
	void Start() {
		originalRunSpeed = runSpeed; // Armazena a velocidade original no início
	}

	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetKeyDown(KeyCode.W) && !controller.IsCrouching)
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			dash = true;
		}

		// Raycast para verificar colisão acima do jogador
		RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.8f, Vector2.up, 0.2f); // Ajuste a distância conforme necessário

		// Debug para visualizar o Raycast
		Debug.DrawRay(transform.position + Vector3.up * 0.8f, Vector2.up * 0.2f, Color.red); // Desenha o Raycast em vermelho

		if (hit.collider != null) // Se houver uma colisão
		{
			controller.Crouch(true); // Mantém o agachamento
			runSpeed = originalRunSpeed / 2; // Reduz a velocidade para um terço
		}
		else
		{
			if (Input.GetKey(KeyCode.S) && controller.IsGrounded)
			{
				controller.Crouch(true);
				runSpeed = originalRunSpeed / 2; // Reduz a velocidade para um terço
			}
			else
			{
				controller.Crouch(false);
				runSpeed = originalRunSpeed; // Restaura a velocidade original
			}
		}

		/*if (Input.GetAxisRaw("Dash") == 1 || Input.GetAxisRaw("Dash") == -1) //RT in Unity 2017 = -1, RT in Unity 2019 = 1
		{
			if (dashAxis == false)
			{
				dashAxis = true;
				dash = true;
			}
		}
		else
		{
			dashAxis = false;
		}
		*/

	}

	public void OnFall()
	{
		animator.SetBool("IsJumping", true);
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
		jump = false;
		dash = false;
	}
}
