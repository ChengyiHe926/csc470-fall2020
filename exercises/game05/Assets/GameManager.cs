using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public UnitScript selectedUnit;

	public GameObject namePanel;
	public GameObject nameText;
	public GameObject healthMeter;
	public GameObject BulletAmount;
	public GameObject LaserAmount;
	public GameObject GoMining;
	public GameObject FuelAnmo;
	public GameObject GoldStorage;
	public GameObject BulletFactory;
	public GameObject LaserFactory;
	public GameObject Enginer;
	public GameObject Unit;
	public GameObject Miner;
	public GameObject Construct;

	public int MaxGoldNumber;//设置基地最大金币数

	GameObject SelectUnitObj;

	GameObject SelectGoldMine;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	}
    
    public void GoButtonClicked()
	{
		if (selectedUnit != null) {
			//selectedUnit.StartFollowingPath();
		}
	}

	// This function takes a Unit's UnitScript, makes it selected, and deselects any other units that were selected.
	// If null is passed in, it will just deselect everything.
	// This function also populates the nameText UI element with the currently selected unit's name, and also ensures
	// that the namePanel UI element is only active is a unit is selected.
	public void SelectUnit(GameObject SelectGameobject)
	{
		// Get an array of all GameObjects that have the tag "Unit".
		GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
		// Loop through all units and make sure they are not selected.
        if (units.Length > 0)
        {
			for (int i = 0; i < units.Length; i++)
			{
				UnitScript unitScript = units[i].GetComponent<UnitScript>();
				unitScript.selected = false;
				unitScript.UpdateVisuals();
			}
		}

		GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
		BaseScript baseScript = baseObj.GetComponent<BaseScript>();
		baseScript.selected = false;
		baseScript.UpdateVisuals();

		GameObject[] BulletFactorys = GameObject.FindGameObjectsWithTag("BulletFactory");
		if (BulletFactorys.Length > 0)
		{
			for (int i = 0; i < BulletFactorys.Length; i++)
			{
				BulletFactoryScript BulletFactoryScript = BulletFactorys[i].GetComponent<BulletFactoryScript>();
				BulletFactoryScript.selected = false;
				BulletFactoryScript.UpdateVisuals();
			}
		}

		GameObject[] LaserFactorys = GameObject.FindGameObjectsWithTag("LaserFactory");
		if (LaserFactorys.Length > 0)
		{
			for (int i = 0; i < LaserFactorys.Length; i++)
			{
				LaserFactoryScript LaserFactoryScript = LaserFactorys[i].GetComponent<LaserFactoryScript>();
				LaserFactoryScript.selected = false;
				LaserFactoryScript.UpdateVisuals();
			}
		}

		GameObject[] Enginers = GameObject.FindGameObjectsWithTag("Enginer");
		if (Enginers.Length > 0)
		{
			for (int i = 0; i < Enginers.Length; i++)
			{
				EnginerScript EnginerScript = Enginers[i].GetComponent<EnginerScript>();
				EnginerScript.selected = false;
				EnginerScript.UpdateVisuals();
			}
		}

		GameObject[] Miners = GameObject.FindGameObjectsWithTag("Miner");
		if (Miners.Length > 0)
		{
			for (int i = 0; i < Miners.Length; i++)
			{
				MinerScript MinerScript = Miners[i].GetComponent<MinerScript>();
				MinerScript.selected = false;
				MinerScript.UpdateVisuals();
			}
		}

		GameObject[] GoldMines = GameObject.FindGameObjectsWithTag("GoldMine");
		if (GoldMines.Length > 0)
		{
			for (int i = 0; i < GoldMines.Length; i++)
			{
				GoldMineScript GoldMineScript = GoldMines[i].GetComponent<GoldMineScript>();
				GoldMineScript.selected = false;
			}
		}

		if (SelectGameobject.tag == "Unit")
        {
			SelectUnitObj = SelectGameobject;
		    selectedUnit = SelectGameobject.GetComponent<UnitScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				selectedUnit.selected = true;

				UpdateUI(selectedUnit);

				selectedUnit.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "Base")
		{
			BaseScript BaseScript = SelectGameobject.GetComponent<BaseScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				BaseScript.selected = true;

				UpdateBaseUI(BaseScript);

				BaseScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "BulletFactory")
		{
			BulletFactoryScript BulletFactoryScript = SelectGameobject.GetComponent<BulletFactoryScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				BulletFactoryScript.selected = true;

				UpdateBulletFactoryUI(BulletFactoryScript);

				BulletFactoryScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "LaserFactory")
		{
			LaserFactoryScript LaserFactoryScript = SelectGameobject.GetComponent<LaserFactoryScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				LaserFactoryScript.selected = true;

				UpdateLaserFactoryUI(LaserFactoryScript);

				LaserFactoryScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "Enginer")
		{
			EnginerScript EnginerScript = SelectGameobject.GetComponent<EnginerScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				EnginerScript.selected = true;

				UpdateEnginerUI(EnginerScript);

				EnginerScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "Miner")
		{
			MinerScript MinerScript = SelectGameobject.GetComponent<MinerScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				MinerScript.selected = true;

				UpdateMinerUI(MinerScript);

				MinerScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}

		if (SelectGameobject.tag == "GoldMine")
		{
			SelectGoldMine = SelectGameobject;
			GoldMineScript GoldMineScript = SelectGameobject.GetComponent<GoldMineScript>();

			if (SelectGameobject != null)
			{
				// If there is a selected, mark it as selected, update its visuals, and update the UI elements.
				GoldMineScript.selected = true;

				UpdateGoldMineUI(GoldMineScript);

				//GoldMineScript.UpdateVisuals();
			}
			else
			{
				// If we get in here, it means that the toSelect parameter was null, and that means that we 
				// should deactivate the namePanel.
				namePanel.SetActive(false);
			}
		}
	}
	
	public void UpdateUI(UnitScript unit)
	{
		healthMeter.GetComponent<MeterScript>().SetMeter(unit.health / 100f);
		BulletAmount.GetComponent<MeterScript>().SetMeter(unit.bulletAmount / unit.bulletMaxAmount);
		LaserAmount.GetComponent<MeterScript>().SetMeter(unit.LaserAmount / unit.LaserMaxAmount);
		nameText.GetComponent<Text>().text = unit.unitName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);
		namePanel.SetActive(true);
		healthMeter.SetActive(true);
		BulletAmount.SetActive(true);
		LaserAmount.SetActive(true);
		FuelAnmo.SetActive(true);
	}

	public void UpdateBaseUI(BaseScript BaseScript)
	{
		//healthMeter.SetMeter(unit.health / 100f);
		healthMeter.GetComponent<MeterScript>().SetMeter(BaseScript.health / 100f);
		GoldStorage.GetComponent<MeterScript>().SetMeter(BaseScript.GoldNumber / MaxGoldNumber);
		nameText.GetComponent<Text>().text = BaseScript.baseName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);

		healthMeter.SetActive(true);
		GoldStorage.SetActive(true);
		BulletFactory.SetActive(true);
		LaserFactory.SetActive(true);
		Enginer.SetActive(true);
		Unit.SetActive(true);
		Miner.SetActive(true);
		Construct.SetActive(true);
	}

	public void UpdateBulletFactoryUI(BulletFactoryScript BulletFactoryScript)
	{
		BulletAmount.GetComponent<MeterScript>().SetMeter(BulletFactoryScript.bulletAmount / BulletFactoryScript.bulletMaxAmount);
		nameText.GetComponent<Text>().text = BulletFactoryScript.BulletFactoryName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);

		BulletAmount.SetActive(true);
	}

	public void UpdateLaserFactoryUI(LaserFactoryScript LaserFactoryScript)
	{
		LaserAmount.GetComponent<MeterScript>().SetMeter(LaserFactoryScript.LaserAmount / LaserFactoryScript.LaserMaxAmount);
		nameText.GetComponent<Text>().text = LaserFactoryScript.LaserFactoryName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);

		LaserAmount.SetActive(true);
	}

	public void UpdateEnginerUI(EnginerScript EnginerScript)
	{
		LaserAmount.GetComponent<MeterScript>().SetMeter(EnginerScript.LaserAmount / EnginerScript.LaserMaxAmount);
		BulletAmount.GetComponent<MeterScript>().SetMeter(EnginerScript.bulletAmount / EnginerScript.bulletMaxAmount);
		nameText.GetComponent<Text>().text = EnginerScript.enginerName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);

		LaserAmount.SetActive(true);
		BulletAmount.SetActive(true);
	}

	public void UpdateMinerUI(MinerScript MinerScript)
	{
		GoldStorage.GetComponent<MeterScript>().SetMeter(MinerScript.Gold / MinerScript.maxGold);
		nameText.GetComponent<Text>().text = MinerScript.minerName;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);
		GoldStorage.SetActive(true);
	}

	public void UpdateGoldMineUI(GoldMineScript GoldMineScript)
	{
		GoldStorage.GetComponent<MeterScript>().SetMeter(GoldMineScript.goldStorage / GoldMineScript.goldMaxStorage);
		nameText.GetComponent<Text>().text = GoldMineScript.goldMine;

		for (int i = 0; i < GameObject.Find("InfoPanel").transform.childCount; i++)
		{
			GameObject.Find("InfoPanel").transform.GetChild(i).gameObject.SetActive(false);
		}

		nameText.SetActive(true);
		GoMining.SetActive(true);
		GoldStorage.SetActive(true);
	}

	public void NeedAnmo()
    {
		GameObject[] Enginers = GameObject.FindGameObjectsWithTag("Enginer");
        if (Enginers.Length > 0)
        {
			for (int i = 0; i < Enginers.Length; i++)
			{
				if (Enginers[Enginers.Length - 1].GetComponent<EnginerScript>().isWorking == true)
				{
					GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
					GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "No Aviable Enginer";
					StartCoroutine(CancelTips());//将提示关闭
				}

				if (Enginers[i].GetComponent<EnginerScript>().isWorking == false)
				{
					Enginers[i].GetComponent<EnginerScript>().isWorking = true;
					Enginers[i].GetComponent<EnginerScript>().TemTargetPosition = SelectUnitObj.transform.position;
					Enginers[i].GetComponent<EnginerScript>().FuelObj = SelectUnitObj;
				}
			}
        }
        else
		{
			GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
			GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "No Aviable Enginer";
			StartCoroutine(CancelTips());//将提示关闭
		}
	}

	public void GoMiningAction()
	{
		GameObject[] Miners = GameObject.FindGameObjectsWithTag("Miner");
        if (Miners.Length > 0)
        {
			for (int i = 0; i < Miners.Length; i++)
			{
				if (Miners[Miners.Length - 1].GetComponent<MinerScript>().isWorking == true)
				{
					GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
					GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "No Aviable Miner";
					StartCoroutine(CancelTips());//将提示关闭
				}

				if (Miners[i].GetComponent<MinerScript>().isWorking == false)
				{
					Miners[i].GetComponent<MinerScript>().isWorking = true;
					Miners[i].GetComponent<MinerScript>().targetGoldMine = SelectGoldMine;
					Miners[i].GetComponent<MinerScript>().targetPosition = SelectGoldMine.transform.position;
				}
			}
        }
        else
        {
			GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
			GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "No Aviable Miner";
			StartCoroutine(CancelTips());//将提示关闭
		}
	}

	IEnumerator CancelTips()
	{
		yield return new WaitForSeconds(0.5f);
		GameObject.Find("tips").transform.GetChild(0).GetComponent<Text>().text = "";
		GameObject.Find("tips").SetActive(false);
	}
}
