using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
		
	Vector3 mapSize;
	Transform player;
	public GameObject[] builds;
	public GameObject[] uiIcons;
	public Sprite[] images;
	public Slider progressBar;
	

	void Start()
	{
		mapSize = GetComponent<BoxCollider>().size;
		player = GameObject.FindWithTag("Player").transform;
		player.position = RandomizePosition();

		builds = GameObject.FindGameObjectsWithTag("Building");
		RandomizeArray(builds);
		
		progressBar = GameObject.FindObjectOfType<Slider>();	
		progressBar.value = 0;	
		uiIcons = GameObject.FindGameObjectsWithTag("UI");
		SetUIIcons();
		ShuffleObjects();		
	}	

	void ShuffleObjects()
	{
		foreach (GameObject item in builds)
		{
			item.transform.position = player.position = RandomizePosition();
		}
	}

	Vector3 RandomizePosition()
	{
		return new Vector3(Random.Range(-mapSize.x,mapSize.x)/2, player.position.y, Random.Range(-mapSize.z,mapSize.z)/2);		
	}

	static void RandomizeArray (GameObject[] arr)
	{
		for (var i = arr.Length - 1; i > 0; i--) {
			var r = Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
    	}	
	}

	void SetUIIcons()
	{
		for (int i = 0; i < builds.Length; i++)
		{
			switch (builds[i].name)
			{
				case "Obelisk":
					uiIcons[i].GetComponent<Image>().sprite = images[0];
				break;
				case "Square":
					uiIcons[i].GetComponent<Image>().sprite = images[1];
				break;
				case "Vase":
					uiIcons[i].GetComponent<Image>().sprite = images[2];
				break;
			}
		}
	}

	public void ActivateBuilding(string objName)
	{
		if (builds[(int)progressBar.value].name == objName)
		{			
			builds[(int)progressBar.value].SendMessage("Activate");
			progressBar.value++;
		}
		else
		{			
			DropQueue();
		}
	}

	void DropQueue()
	{		
		progressBar.value = 0;
		foreach (GameObject build in builds)
		{
			build.SendMessage("Deactivate");
		}
	}
}
