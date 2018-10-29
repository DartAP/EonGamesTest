using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskOfOrdinary : MonoBehaviour {

	public bool isActivated = false;	
	
	public float roundProgress = 0f;
	public float firstContactAngle = 0f;
	public Vector3 firstContact;
	public Transform target;
	public Transform observer;



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
	
	void Update()
	{
		if (target != null)
		{
			observer.LookAt(target);
			observer.position = new Vector3(observer.position.x, target.position.y, observer.position.z);
		}		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			target = other.transform;
			firstContactAngle = observer.rotation.y;		
		}
	}
	
	void OnTriggerStay(Collider other)
	{		
		float observerRotation = observer.rotation.y;
		roundProgress = observerRotation - firstContactAngle;
		Vector3 targetDirection = target.position - transform.position;
    	float angleBetween = Vector3.Angle(firstContact, targetDirection);		
		roundProgress = angleBetween;	
	}	
		

	void OnTriggerExit(Collider other)
	{		
		roundProgress = 0f;
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
