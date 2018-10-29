using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Animator))]
[RequireComponent (typeof (SphereCollider))]
public class PlayerController : MonoBehaviour {

	public float playerSpeed = 10f;
	bool allowHeavy = true;
	bool allowLight = true;
	
	Animator playerAnim;
	VaseOfEternity vase;
	

	void Start()
	{		
		playerAnim = GetComponent<Animator>();
		vase = GameObject.FindObjectOfType<VaseOfEternity>();
	}

	void Update()
	{
		float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        
		MovePlayer(horizontal, vertical);
		Attack();        
	}

    void MovePlayer(float h, float v)
    {
        float walking = (Mathf.Abs(h) + v);
        playerAnim.SetFloat("Speed", walking);
        playerAnim.SetFloat("Direction", h);            
    }

	void Attack()
	{
		if (Input.GetButtonDown("Fire1") && allowLight)
		{			
			playerAnim.SetFloat("AttackType", Mathf.RoundToInt(Random.Range(0f,1.0f)));
			playerAnim.SetBool("LightAttack", true);
		}
		if (Input.GetButtonDown("Fire2") && allowHeavy)
		{
			playerAnim.SetBool("HeavyAttack", true);
		}
	}

	void ChangeAttackStatus(int status)
	{			
		switch (status)
		{
			case 0:
				allowHeavy = true;
				allowLight = true;
				playerAnim.SetBool("LightAttack", false);
				playerAnim.SetBool("HeavyAttack", false);
				break;
			case 1:
				allowHeavy = true;
				allowLight = false;
				playerAnim.SetBool("LightAttack", false);
				if (vase.isAttacked)
					vase.Triggered();		
				break;
			case 2:
				allowHeavy = false;
				allowLight = true;
				if (vase.isAttacked)
					vase.Triggered();
				break;		
		}
	}	
}
