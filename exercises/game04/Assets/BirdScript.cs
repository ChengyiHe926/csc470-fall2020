using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
	float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
		transform.Rotate(0, Random.Range(0,360), 0);
		speed = Random.Range(4, 10);
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
