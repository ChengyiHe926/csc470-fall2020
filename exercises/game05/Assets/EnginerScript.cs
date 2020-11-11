using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnginerScript : MonoBehaviour
{
    public int bulletMaxAmount = 50;//能运输的最大子弹数
    public int LaserMaxAmount = 40;//现存子弹数

    public int bulletAmount = 0;//能运输的最大光线能量数
    public int LaserAmount = 0;//现存光线能量数

    public string enginerName = "Enginer";

    public bool isWorking = false;

    public Vector3 StartPosition;
    public Vector3 targetPosition;

    public Vector3 TemTargetPosition;
    public GameObject FuelObj;

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
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = GameObject.Find("Base").transform.position;
        StartPosition = GameObject.Find("Base").transform.position;

        //targetPosition = new Vector3(GameObject.Find("Base").transform.position.x, GameObject.Find("Base").transform.position.y, GameObject.Find("Base").transform.position.z + 1);

        UpdateVisuals();
        gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking == true)
        {
            if (bulletAmount < bulletMaxAmount)//补充弹药
            {
                GameObject[] BulletFactorys = GameObject.FindGameObjectsWithTag("BulletFactory");
                for (int i = 0; i < BulletFactorys.Length; i++)
                {
                    if (BulletFactorys[BulletFactorys.Length - 1].GetComponent<BulletFactoryScript>().bulletAmount < bulletMaxAmount)
                    {
                        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                        GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "not enough bullet";
                        StartCoroutine(CancelTips());//将提示关闭
                    }

                    if (BulletFactorys[i].GetComponent<BulletFactoryScript>().bulletAmount > bulletMaxAmount)
                    {
                        BulletFactorys[i].GetComponent<BulletFactoryScript>().bulletAmount -= bulletMaxAmount;
                        bulletAmount = bulletMaxAmount;
                    }
                }
            }

            if (LaserAmount < LaserMaxAmount)//补充光线能量
            {
                GameObject[] LaserFactorys = GameObject.FindGameObjectsWithTag("LaserFactory");
                for (int i = 0; i < LaserFactorys.Length; i++)
                {

                    if (LaserFactorys[LaserFactorys.Length - 1].GetComponent<LaserFactoryScript>().LaserAmount < LaserMaxAmount)
                    {
                        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                        GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "not enough laser";
                        StartCoroutine(CancelTips());//将提示关闭
                    }

                    if (LaserFactorys[i].GetComponent<LaserFactoryScript>().LaserAmount > LaserMaxAmount)
                    {
                        LaserFactorys[i].GetComponent<LaserFactoryScript>().LaserAmount -= LaserMaxAmount;
                        LaserAmount = LaserMaxAmount;
                    }
                }
            }

            targetPosition = TemTargetPosition;

            if (Vector3.Distance(transform.position, targetPosition) > 2f)
            {
                Vector3 vectorToTarget = targetPosition - transform.position;
                vectorToTarget = vectorToTarget.normalized;

                float step = rotateSpeed * Time.deltaTime;

                Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
                transform.rotation = Quaternion.LookRotation(newDirection);

                cc.Move(transform.forward * speed * Time.deltaTime);
            }
            else//补充完毕
            {
                isWorking = false;
                bulletAmount = 0;
                LaserAmount = 0;
                FuelObj.GetComponent<UnitScript>().bulletAmount += 50;
                FuelObj.GetComponent<UnitScript>().LaserAmount += 40;
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
    IEnumerator CancelTips()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "";
        GameObject.Find("tips").SetActive(false);
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
