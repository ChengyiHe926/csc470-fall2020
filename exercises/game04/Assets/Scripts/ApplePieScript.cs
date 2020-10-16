using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplePieScript : MonoBehaviour
{
	GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
    	// Because this apple apie was is instantiated at runtime, we need to dynamically 
    	// get the reference ot the game manager.
    	//
    	// NOTE: This way to finding a game object and then immediately get a component on it isn't
    	// "safe" because if GameObject.Find fails (returns null), we immediately attempt to call
    	// GetComponent on null. This will produce a runtime error. So only use this pattern when you
    	// are 100% sure that there is a GameObject with the name you are searching for.
		gm = GameObject.Find("GameManagerObj").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
	{
		// When we collide with the apple tree, update score in game manager, and destroy the tree.
		if (other.CompareTag("appleTree")) {
			Destroy(other.gameObject);
			gm.IncreaseScore();
		}
	}
}
