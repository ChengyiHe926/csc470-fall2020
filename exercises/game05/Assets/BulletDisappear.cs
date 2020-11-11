using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
