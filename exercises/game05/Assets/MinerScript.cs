using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerScript : MonoBehaviour
{
    public int maxGold = 500;//金币最大运载量
    public int Gold = 0;//现有金币

    public string minerName = "Miner";
    public bool isWorking = false;

    public Vector3 StartPosition;
    public Vector3 targetPosition;

    public int health = 600;

    float rotateSpeed = 4f;

    public CharacterController cc;


    // How fast the Unit will move forward.
    public float speed = 5f;

    GameManager gm;

    // These two booleans are used to track the state based on the mouse (see the mouse functions below).
    public bool selected = false;
    public bool hover = false;

    // These colors are given values via the Unity inspector.
    public Color defaultColor;
    public Color hoverColor;
    public Color selectedColor;

    // This gets its value from the Unity inspector. We dragged the "Mesh Renderer" of the Prefab to do that.
    public Renderer[] rend;

    public GameObject targetGoldMine;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameObject.Find("Base").transform.position;
        StartPosition = GameObject.Find("Base").transform.position;

        //targetPosition = new Vector3(GameObject.Find("Base").transform.position.x + 1, GameObject.Find("Base").transform.position.y, GameObject.Find("Base").transform.position.z + 1);

        UpdateVisuals();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        if (isWorking == true)
        {
            if (targetPosition != GameObject.Find("Base").transform.position)
            {
                if (Vector3.Distance(transform.position, targetPosition) > 2f)
                {
                    Vector3 vectorToTarget = targetPosition - transform.position;
                    vectorToTarget = vectorToTarget.normalized;

                    float step = rotateSpeed * Time.deltaTime;

                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
                    transform.rotation = Quaternion.LookRotation(newDirection);

                    cc.Move(transform.forward * speed * Time.deltaTime);
                }
                else//采完矿回基地
                {
                    targetGoldMine.GetComponent<GoldMineScript>().goldStorage -= maxGold;
                    Gold = maxGold;

                    this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    targetPosition = GameObject.Find("Base").transform.position;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPosition) > 2f)
                {
                    Vector3 vectorToTarget = targetPosition - transform.position;
                    vectorToTarget = vectorToTarget.normalized;

                    float step = rotateSpeed * Time.deltaTime;

                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
                    transform.rotation = Quaternion.LookRotation(newDirection);

                    cc.Move(transform.forward * speed * Time.deltaTime);
                }
                else//到基地将金币倒出来
                {
                    GameObject.Find("Base").GetComponent<BaseScript>().GoldNumber += maxGold;
                    Gold = 0;
                    isWorking = false;
                    this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    targetPosition = GameObject.Find("Base").transform.position;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 vectorToTarget = targetPosition - transform.position;
                vectorToTarget = vectorToTarget.normalized;

                float step = rotateSpeed * Time.deltaTime;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
                transform.rotation = Quaternion.LookRotation(newDirection);

                cc.Move(transform.forward * speed * Time.deltaTime);
            }
        }      
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
