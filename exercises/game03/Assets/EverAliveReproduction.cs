using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EverAliveReproduction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EverAliveChange(bool checkValue)
    {
        if (checkValue == true)
        {
            //everAlive still can reproduction
            this.transform.GetChild(1).GetComponent<Text>().text = "EverAlive can reproduction";
            GameObject.Find("GameManagerObject").GetComponent<GameManager>().everAliveReproduction = true;
        }
        else
        {
            //everAlive still cannot reproduction
            this.transform.GetChild(1).GetComponent<Text>().text = "EverAlive cannot reproduction";
            GameObject.Find("GameManagerObject").GetComponent<GameManager>().everAliveReproduction = false;
        }
    }
}
