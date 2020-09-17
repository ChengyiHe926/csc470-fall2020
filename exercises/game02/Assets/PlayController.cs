using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    float speed = 20f;
    float rotateSpeed = 70f;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal"); //-1 left, 1 right
        transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);

        transform.Translate(transform.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hearts"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }

    
}
