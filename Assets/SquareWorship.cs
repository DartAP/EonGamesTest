using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareWorship : MonoBehaviour {

	public bool isActivated = false;

	public ParticleSystem successParticle;
	public ParticleSystem failParticle;
	Animator playerAC;
	GameManager gameManager;
	
	void Start()
	{
		playerAC = GameObject.FindWithTag("Player").GetComponent<Animator>();
		gameManager = GameObject.FindObjectOfType<GameManager>();
		successParticle.Stop();
		failParticle.Stop();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && playerAC.GetFloat("Speed") == 0 && !isActivated)		
			Triggered();		
	}

	public void Triggered()
	{
		if (!isActivated)
		{
			gameManager.ActivateBuilding(gameObject.name);
		}
	}

	public void Activate()
	{
		isActivated = true;
		successParticle.Play();
		failParticle.Stop();
	}	

	public void Deactivate()
	{
		if (isActivated)
		{			
			isActivated = false;
			successParticle.Stop();
			failParticle.Play();
		}
	}
}
