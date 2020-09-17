using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManage : MonoBehaviour
{
	
	public GameObject heartPrefab;
	public GameObject rainDropPrefab;

	
	GameObject ground;

	
	float makeRainTimer = 0.01f;
	float makeRainRate = 0.01f;

	int numHearts = 0;
	
	void Start()
	{
		ground = GameObject.Find("ground");
	}

	
	void Update()
	{
		
		makeRainTimer -= Time.deltaTime;
		if (makeRainTimer < 0)
		{
		
			Vector3 pos = new Vector3(ground.transform.position.x + Random.Range(-10, 10)
								, ground.transform.position.y + 50,
								ground.transform.position.z + Random.Range(-10, 10));
			
			GameObject drop = Instantiate(rainDropPrefab, pos, Quaternion.identity);
			
			Renderer rend = drop.GetComponent<Renderer>();
			rend.material.color = new Color(Random.value, Random.value, Random.value);

			
			Destroy(drop, 5f);

			
			makeRainTimer = makeRainRate;
		}
	}

	public void MakeMoreHearts()
	{
		numHearts++;

		if (numHearts < 10)
		{
			Debug.Log("Make heart");

			Vector3 pos = new Vector3(ground.transform.position.x + Random.Range(-10, 10)
									, ground.transform.position.y + 10,
									ground.transform.position.z + Random.Range(-10, 10));
			Instantiate(heartPrefab, pos, Quaternion.identity);
		}
        else
        {
			SceneManager.LoadScene("level");
        }
	}
}
