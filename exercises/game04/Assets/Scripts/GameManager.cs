using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject appleTreePrefab;
	public GameObject ringPrefab;

	public Text scoreText;
	int score = 0;

    // Start is called before the first frame update
    void Start()
    {
		// Initialize the score text to 0.
		scoreText.text = score.ToString();
		
		// Instantiate a bunch of apple trees all over the terrain.
		for (int i = 0; i < 100; i++) {
			// Choose a random position over the terrain
			float x = Random.Range(Terrain.activeTerrain.transform.position.x, Terrain.activeTerrain.transform.position.x + Terrain.activeTerrain.terrainData.size.x);
			float z = Random.Range(Terrain.activeTerrain.transform.position.z, Terrain.activeTerrain.transform.position.z + Terrain.activeTerrain.terrainData.size.z);
			Vector3 pos = new Vector3(x, 0, z);
			// Use sample height at that position to find out what y should be.
			float y = Terrain.activeTerrain.SampleHeight(pos);
			pos.y = y;
			// Instantiate the tree at that position.
			GameObject appleTree = Instantiate(appleTreePrefab, pos, Quaternion.identity);
			// Randomly rotate the tree on the y axis for variation
			appleTree.transform.Rotate(0, Random.Range(0,360), 0);
		}
		
		// Instantiate a bunch of rings all around the terrain (same process as with the tree)
		for (int i = 0; i < 100; i++) {
			float x = Random.Range(Terrain.activeTerrain.transform.position.x, Terrain.activeTerrain.transform.position.x + Terrain.activeTerrain.terrainData.size.x);
			float z = Random.Range(Terrain.activeTerrain.transform.position.z, Terrain.activeTerrain.transform.position.z + Terrain.activeTerrain.terrainData.size.z);
			Vector3 pos = new Vector3(x, 0, z);
			float y = Terrain.activeTerrain.SampleHeight(pos) + Random.Range(10, 80);
			pos.y = y;
			GameObject ring = Instantiate(ringPrefab, pos, Quaternion.identity);
			ring.transform.Rotate(0, Random.Range(0, 360), Random.Range(0, 360));
		}
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // This function is called from the ApplePieScript whenever it collides with an apple tree.
    public void IncreaseScore()
	{
		score++;
		scoreText.text = score.ToString();
	}
}
