using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float smooth = 5;
    public float distance = 5;
    float x = 0f;    

    public float mouseSensitivity = 100;
	Transform camPos;
    Transform target;

	void Start()
	{
		camPos = GameObject.Find("CamPos").transform;
        target = GameObject.FindWithTag("Player").transform;
        x = transform.eulerAngles.y;        
	}

	void Update()
	{		
		transform.position = Vector3.Lerp(transform.position, camPos.position, Time.deltaTime * smooth);
		transform.forward = Vector3.Lerp(transform.forward, camPos.forward, Time.deltaTime * smooth);
	}

	void LateUpdate() 
	{
		if (target) 
		{			
			x += Input.GetAxis("Mouse X") * mouseSensitivity * 0.02f;			
			Quaternion rotation = Quaternion.Euler(0, x, 0);
			target.transform.rotation = rotation;
    	}
	}
}
