using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
	public string baseName;

	public int GoldNumber = 10000;

	public int health;

	public GameObject endGame;
	public GameObject tips;

	public string constructType;
	GameObject constructObj;//用于指示将要创建的物体

	Vector3 constructPoint;//修建物体的位置

	bool beginConstruct = false;

	public int MaxGoldNumber = 100000;//设置基地最大金币数

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

	public int costOfBulletFactory = 1000;
	public int costOfEnginer = 300;
	public int costOfLaserFactory = 2000;
	public int costOfMiner = 500;
	public int costOfUnit = 1000;
	void Start()
	{
		UpdateVisuals();

		gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (selected)
		{
			GameObject.Find("HealthMeter").GetComponent<MeterScript>().SetMeter(health / 100f);
			GameObject.Find("GoldStorage").GetComponent<MeterScript>().SetMeter(GoldNumber / MaxGoldNumber);
		}

		if (health <= 0)
        {
			endGame.SetActive(true);
			endGame.transform.GetChild(1).GetComponent<Text>().text = "You Lost!!!";
		}

		if (beginConstruct == true)//确定修建的位置
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If we get in here, it means that the mouse was "over" a GameObject when the player clicked.
                // Check to see if what we clicked on the the "Ground" via this layer check.
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    constructObj.transform.position = hit.point;
					constructPoint = hit.point;

				}
                else
                {
                    //tips.SetActive(true);
                    //tips.transform.GetChild(0).GetComponent<Text>().text = "can't construct from this location";
                    //StartCoroutine(CancelTips());
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
				Destroy(constructObj);//销毁当前用于表示的修建物体
				beginConstruct = false;

				if (constructType == "Enginer")
				{
					if (GoldNumber >= 300)
					{
						StartCoroutine(delayConstruct("Enginer", constructPoint));//修建Enginer
					}
					else
					{
						tips.SetActive(true);
						tips.transform.GetChild(0).GetComponent<Text>().text = "not enough money!";
						StartCoroutine(CancelTips());
					}
				}

				if (constructType == "Miner")
				{
					if (GoldNumber >= 500)
					{
						StartCoroutine(delayConstruct("Miner", constructPoint));//修建Miner
					}
					else
					{
						tips.SetActive(true);
						tips.transform.GetChild(0).GetComponent<Text>().text = "not enough money!";
						StartCoroutine(CancelTips());
					}
				}

				if (constructType == "Unit")
				{
					if (GoldNumber >= 700)
					{
						StartCoroutine(delayConstruct("Unit", constructPoint));//修建Unit
					}
					else
					{
						tips.SetActive(true);
						tips.transform.GetChild(0).GetComponent<Text>().text = "not enough money!";
						StartCoroutine(CancelTips());
					}
				}

				if (constructType == "LaserFactory")
				{
					if (GoldNumber >= 700)
					{
						StartCoroutine(delayConstruct("LaserFactory", constructPoint));//修建LaserFactory
					}
					else
					{
						tips.SetActive(true);
						tips.transform.GetChild(0).GetComponent<Text>().text = "not enough money!";
						StartCoroutine(CancelTips());
					}
				}

				if (constructType == "BulletFactory")
				{
					if (GoldNumber >= 700)
					{
						StartCoroutine(delayConstruct("BulletFactory", constructPoint));//修建BulletFactory
					}
					else
					{
						tips.SetActive(true);
						tips.transform.GetChild(0).GetComponent<Text>().text = "not enough money!";
						StartCoroutine(CancelTips());
					}
				}
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

	public void constructTypeClick(string type)
    {
		constructType = type;
	}

	public void Construct()
	{
		if (constructType != "")
        {
            if (constructType == "BulletFactory")
			{
				constructObj = (GameObject)Instantiate(Resources.Load("BulletFactory"));
				Destroy(constructObj.GetComponent<BulletFactoryScript>());
			}
			else if (constructType == "LaserFactory")
            {
				constructObj = (GameObject)Instantiate(Resources.Load("LaserFactory"));
				Destroy(constructObj.GetComponent<LaserFactoryScript>());
			}
			else if (constructType == "Enginer")
			{
				constructObj = (GameObject)Instantiate(Resources.Load("Enginer"));
				Destroy(constructObj.GetComponent<EnginerScript>());
			}
			else if (constructType == "Unit")
			{
				constructObj = (GameObject)Instantiate(Resources.Load("Unit"));
				Destroy(constructObj.GetComponent<UnitScript>());
			}
			else//“Miner”
			{
				constructObj = (GameObject)Instantiate(Resources.Load("Miner"));
				Destroy(constructObj.GetComponent<MinerScript>());
			}

			beginConstruct = true;
		}
        else
        {
			tips.SetActive(true);
			tips.transform.GetChild(0).GetComponent<Text>().text = "didn't choose type to construct";
			StartCoroutine(CancelTips());
			//提示未选择修建类型
		}
	}

	IEnumerator CancelTips()
    {
		yield return new WaitForSeconds(1.5f);
		tips.SetActive(false);
		tips.transform.GetChild(0).GetComponent<Text>().text = "";
	}

	IEnumerator delayConstruct(string constructType,Vector3 targetPoint)
    {
		yield return new WaitForSeconds(3f);
        if (constructType == "Miner")
        {
			GameObject miner = (GameObject)Instantiate(Resources.Load("Miner"));
			miner.GetComponent<MinerScript>().targetPosition = targetPoint;
		}

		if(constructType == "Enginer")
        {
			GameObject Enginer = (GameObject)Instantiate(Resources.Load("Enginer"));
			Enginer.GetComponent<EnginerScript>().targetPosition = targetPoint;
		}

		if (constructType == "Unit")
		{
			GameObject Unit = (GameObject)Instantiate(Resources.Load("Unit"));
			Unit.transform.position = targetPoint;
		}

		if (constructType == "LaserFactory")
		{
			GameObject LaserFactory = (GameObject)Instantiate(Resources.Load("LaserFactory"));
			LaserFactory.transform.position = targetPoint;
		}

		if (constructType == "BulletFactory")
		{
			GameObject BulletFactory = (GameObject)Instantiate(Resources.Load("BulletFactory"));
			BulletFactory.transform.position = targetPoint;
		}
	}
}
