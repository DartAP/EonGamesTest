using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseOfEternity : MonoBehaviour {

	public bool isActivated = false;
	public bool isAttacked = false;

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

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Sword" && !isActivated)	
			isAttacked = true;
	}
	
	void OnTriggerExit(Collider other)
	{		
		isAttacked = false;	
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
		isActivated = false;
		successParticle.Stop();
		failParticle.Play();
	}
}
