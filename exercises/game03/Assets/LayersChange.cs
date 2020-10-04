using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayersChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(bool checkValue)
    {
        if (checkValue == true)
        {
            //运行第一层
            this.transform.GetChild(1).GetComponent<Text>().text = "1 Layers";
            GameObject.Find("GameManagerObject").GetComponent<GameManager>().boolLayers = true;
        }
        else
        {
            //运行第二层
            this.transform.GetChild(1).GetComponent<Text>().text = "2 Layers";
            GameObject.Find("GameManagerObject").GetComponent<GameManager>().boolLayers = false;
        }
    }
}
