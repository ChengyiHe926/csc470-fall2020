using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMineScript : MonoBehaviour
{
    public int goldMaxStorage = 4000;//金币最大存储量
    public int goldStorage = 4000;//现有金币储量

    public string goldMine = "GoldMine";

    public bool selected = false;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goldMaxStorage <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        selected = !selected;
        // If after clicking the unit is selected, tell the GameManager to select it.
        if (selected)
        {
            gm.SelectUnit(this.gameObject);
        }
    }
}
