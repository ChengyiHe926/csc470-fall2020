using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitScript : MonoBehaviour
{
	GameManager gm;

	public string unitName = "Unit";
	public int health = 200;
	public int charisma;

	bool attackOnce = false;

	// These two booleans are used to track the state based on the mouse (see the mouse functions below).
	public bool selected = false;
	public bool hover = false;

	// These colors are given values via the Unity inspector.
	public Color defaultColor;
	public Color hoverColor;
	public Color selectedColor;

	// This gets its value from the Unity inspector. We dragged the "Mesh Renderer" of the Prefab to do that.
	public Renderer rend;

	//public CharacterController cc;

	public float attackDistant = 10f;
	public float attackBulletSpeed = 0.2f;
	public float attackLaserSpeed = 1f;

	public float attackBulletInterval = 2f;//子弹攻击间隔
	public float attackLaserInterval = 3.5f;//光线攻击间隔

	GameObject enemy;

	public float bulletMaxAmount = 50;//最大存弹量
	public float LaserMaxAmount = 30;

	public float bulletAmount = 50;//剩余子弹量
	public float LaserAmount = 30;

	public int bulletDamageNumber = 20;//子弹每次攻击的伤害
	public int LaserDamageNumber = 40;//光线每次攻击的伤害

	// Start is called before the first frame update
	void Start()
	{
		// Set the color of the rendere's material based on the mouse state variables.
		UpdateVisuals();

		gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (selected)
		{
			GameObject.Find("HealthMeter").GetComponent<MeterScript>().SetMeter(health / 500f);
			GameObject.Find("BulletAmount").GetComponent<MeterScript>().SetMeter(bulletAmount / bulletMaxAmount);
			GameObject.Find("LaserAmount").GetComponent<MeterScript>().SetMeter(LaserAmount / LaserMaxAmount);
		}

		if (health <= 0)
		{
			this.gameObject.SetActive(false);
			//Destroy(this.gameObject);
		}

		GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy");
		if (Enemys.Length > 0)
		{
			for (int i = 0; i < Enemys.Length; i++)
			{
				if (Vector3.Distance(Enemys[i].transform.position, transform.position) < attackDistant)
				{
                    if (enemy != Enemys[i])
                    {
						enemy = Enemys[i];
						attackOnce = false;
					}
					if (attackOnce == false)
					{
						attackOnce = true;
						if (bulletAmount > 0)
						{
							StartCoroutine(bulletAttack(Enemys[i]));
						}

                        if (LaserAmount > 0)
                        {
							StartCoroutine(laserAttack(Enemys[i]));
						}
					}

					break;
				}
			}
        }
        else
        {
			enemy = null;
			attackOnce = false;
		}
	}

	// This function is called manually by the mouse event functions whenever
	// the hover, or selection bools are modified.
	public void UpdateVisuals()
	{
		if (selected) {
			rend.material.color = selectedColor;
		} else {
			if (hover) {
				rend.material.color = hoverColor;
			} else {
				rend.material.color = defaultColor;
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
		if (selected) {
			gm.SelectUnit(this.gameObject);
		}
	}

	IEnumerator bulletAttack(GameObject attackObject)
	{
        if (attackOnce == true)
        {
			yield return new WaitForSeconds(attackBulletInterval);

			GameObject bullet1 = null;
			GameObject bullet2 = null;

			for (int i = 0; i < 2; i++)
			{
				GameObject bullet = Instantiate(this.transform.GetChild(i).gameObject, this.transform.GetChild(i).transform.position, this.transform.GetChild(i).transform.rotation);

				if (i == 0)
				{
					bullet1 = bullet;
				}
				else
				{
					bullet2 = bullet;
				}

				Animation bulletAnimation = null;
				bulletAnimation = bullet.AddComponent<Animation>();

				var bulletClip = new AnimationClip()
				{
					name = "bulletAnimation",
					legacy = true,
					wrapMode = WrapMode.Once
				};

				/* dot动画 */
				var bulletCurve = new AnimationCurve();
				bulletCurve.AddKey(new Keyframe(0f, bullet.transform.localPosition.x));
				bulletCurve.AddKey(new Keyframe(attackBulletSpeed, attackObject.transform.localPosition.x));
				bulletClip.SetCurve("", typeof(Transform), "localPosition.x", bulletCurve);

				bulletCurve = new AnimationCurve();
				bulletCurve.AddKey(new Keyframe(0f, bullet.transform.localPosition.y));
				bulletCurve.AddKey(new Keyframe(attackBulletSpeed, attackObject.transform.localPosition.y));
				bulletClip.SetCurve("", typeof(Transform), "localPosition.y", bulletCurve);

				bulletCurve = new AnimationCurve();
				bulletCurve.AddKey(new Keyframe(0f, bullet.transform.localPosition.z));
				bulletCurve.AddKey(new Keyframe(attackBulletSpeed, attackObject.transform.localPosition.z));
				bulletClip.SetCurve("", typeof(Transform), "localPosition.z", bulletCurve);

				bulletAnimation.AddClip(bulletClip, bulletClip.name);
				bulletAnimation.Play(bulletClip.name);
			}

			StartCoroutine(destoryBulletAndDamage(bullet1, bullet2, attackObject)); //降低物体的血量

            if (bulletAmount > 0)
            {
				StartCoroutine(bulletAttack(attackObject));
			}
		}		
	}

	IEnumerator destoryBulletAndDamage(GameObject bullet1, GameObject bullet2, GameObject attackObject)
	{
		yield return new WaitForSeconds(attackBulletSpeed);
		Destroy(bullet1);
		Destroy(bullet2);

		bulletAmount -= 1;
		attackObject.GetComponent<EnemyScript>().health -= bulletDamageNumber;
	}

	IEnumerator laserAttack(GameObject attackObject)
	{
		if (attackOnce == true)
		{
			yield return new WaitForSeconds(attackLaserInterval);

			LineRenderer laser = this.GetComponent<LineRenderer>();
			laser.SetVertexCount(2);

			GameObject tem = new GameObject();
			tem.transform.SetParent(this.transform);
			tem.transform.localPosition = new Vector3(0, 2f, 0);

			laser.SetPosition(0, tem.transform.position);
			laser.SetPosition(1, attackObject.transform.GetChild(3).transform.position);

			Destroy(tem);

			StartCoroutine(destoryLaserAndDamage(attackObject)); //降低物体的血量

			if (LaserAmount > 0)
			{
				StartCoroutine(bulletAttack(attackObject));
			}
		}
	}

	IEnumerator destoryLaserAndDamage(GameObject attackObject)
	{
		yield return new WaitForSeconds(attackLaserSpeed);

		bulletAmount -= 1;
		attackObject.GetComponent<EnemyScript>().health -= bulletDamageNumber;

		LineRenderer laser = this.GetComponent<LineRenderer>();
		laser.SetVertexCount(0);

		LaserAmount -= 1;
		attackObject.GetComponent<EnemyScript>().health -= LaserDamageNumber;
	}
}
