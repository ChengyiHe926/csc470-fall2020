using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactoryScript : MonoBehaviour
{
    public string BulletFactoryName = "BulletFactory";

    GameManager gm;

    public int bulletMaxAmount = 5000;//最大存弹量

    public int bulletAmount = 100;//现在已有子弹量

    // These two booleans are used to track the state based on the mouse (see the mouse functions below).
    public bool selected = false;
    public bool hover = false;

    // These colors are given values via the Unity inspector.
    public Color defaultColor;
    public Color hoverColor;
    public Color selectedColor;

    // This gets its value from the Unity inspector. We dragged the "Mesh Renderer" of the Prefab to do that.
    public Renderer[] rend;
    // Start is called before the first frame update
    void Start()
    {
        UpdateVisuals();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            if (selected)
            {
                rend[i].material.color = selectedColor;
            }
            else
            {
                if (hover)
                {
                    rend[i].material.color = hoverColor;
                }
                else
                {
                    rend[i].material.color = defaultColor;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        hover = true;
        UpdateVisuals();
    }

    private void OnMouseExit()
    {
        hover = false;
        UpdateVisuals();
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
