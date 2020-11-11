using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
		startPosition = transform.position;
	}

    // Update is called once per frame
    void Update()
    {
		//空格键抬升高度
		if (Input.GetKey(KeyCode.Space))
		{
			transform.position = startPosition;
		}

		//w键前进
		if (Input.GetKey(KeyCode.W))
		{
			this.gameObject.transform.Translate(new Vector3(0, 1, 0));
		}
		//s键后退
		if (Input.GetKey(KeyCode.S))
		{
			this.gameObject.transform.Translate(new Vector3(0, -1, 0));
		}
		//a键后退
		if (Input.GetKey(KeyCode.A))
		{
			this.gameObject.transform.Translate(new Vector3(-1, 0, 0));
		}
		//d键后退
		if (Input.GetKey(KeyCode.D))
		{
			this.gameObject.transform.Translate(new Vector3(1, 0, 0));
		}
	}
}
