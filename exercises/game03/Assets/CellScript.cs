using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{
	public Renderer rend;
	public Color aliveColor;
	public Color deadColor;
	public Color everAliveColor;

	public int x = -1;
	public int y = -1;

	float goalHeight = 1;
	float growSpeed = 0.9f;


	Vector3 startPosition;

	public bool everAlive = false;
	public bool nextAlive;
	private bool _alive = false;

	public float possibilitiesOfAlive;

	FPS fPS;
	public bool Alive
    {
        get
        {
            return this._alive;
        }
        set
        {
            this._alive = value;
            
            if (this._alive) {
				everAlive = true;
				rend.material.color = aliveColor;

				goalHeight += 1;
			} else {
				if (everAlive) {
					rend.material.color = everAliveColor;
				} else {
					rend.material.color = deadColor;
				}
			}
		}
    }

    // Start is called before the first frame update
    void Start()
    {
		fPS = GameObject.Find("FPS").GetComponent<FPS>();
		startPosition = transform.position;
    
		//rend = gameObject.GetComponentInChildren<Renderer>();
		this.Alive = Random.value < possibilitiesOfAlive;
    }

    // Update is called once per frame
    void Update()
    {
		//float offsetX = Mathf.Sin(x + Time.fixedTime * 0.001f);
		//transform.Translate(offsetX, 0, 0);
    
		float actualGrowSpeed = growSpeed;
    	if (!this.Alive) {
			actualGrowSpeed *= -1;
		}
		if (transform.localScale.y < goalHeight) {
			float newHeight = transform.localScale.y + actualGrowSpeed * Time.deltaTime;
			newHeight = Mathf.Clamp(newHeight, 1, 10);
			transform.localScale = new Vector3(transform.localScale.x, newHeight, transform.localScale.z);
		}
		
		
		if (transform.position.y < -10) {
			//reset position
			transform.position = startPosition;
		}	
	}

	//private void OnMouseDown()
	//{
	//	this.Alive = !this.Alive;
	//}

	private void OnTriggerEnter(Collider other)
	{
		fPS.standOnObj = this.gameObject;
	}
}
