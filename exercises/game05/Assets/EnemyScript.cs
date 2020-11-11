using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Vector3 targetPosition;
    public Vector3 startPosition;

    public CharacterController cc;

	float rotateSpeed = 4f;

	public int damageNumber = 10;//每次攻击的伤害

	public bool moving = false;

    public float attackDistant = 5f;
    public float attackSpeed = 0.4f;

    bool attackOnce = false;

    public int health = 100;

    // How fast the Unit will move forward.
    public float speed = 1f;

    GameObject attackObj;//正在被攻击的物体
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("Base").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)//无生命，自毁
        {
            StartCoroutine(destoryThis());
            //this.gameObject.SetActive(false);
        }

        if (startPosition == transform.position)
        {
            moving = true;
            startPosition = new Vector3(100,100,100);
        }
        // If we are not close to our target position, rotate toward the targetPosition, and move forward.
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f && moving)
        {
            Vector3 vectorToTarget = targetPosition - transform.position;
            vectorToTarget = vectorToTarget.normalized;

            float step = rotateSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
            transform.rotation = Quaternion.LookRotation(newDirection);

            cc.Move(transform.forward * speed * Time.deltaTime);
        }

        GameObject[] detectUnits = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] detectMiners = GameObject.FindGameObjectsWithTag("Miner");
        GameObject detectBase = GameObject.FindGameObjectWithTag("Base");

        GameObject[] detectObts = new GameObject[detectUnits.Length + detectMiners.Length + 1];
        for (int i = 0; i < detectUnits.Length; i++)
        {
            detectObts[i] = detectUnits[i];
        }
        for (int i = 0; i < detectMiners.Length; i++)
        {
            detectObts[detectUnits.Length + i] = detectMiners[i];
        }
        detectObts[detectMiners.Length + detectUnits.Length] = detectBase;

        for (int i = 0; i < detectObts.Length; i++)
        {

            if (Vector3.Distance(detectObts[i].transform.position, transform.position) < attackDistant)
            {
                if (attackObj != detectObts[i])
                {
                    attackOnce = false;//物体被摧毁，重置攻击
                    attackObj = detectObts[i];
                }

                moving = false;
                if (attackOnce == false)
                {
                    StartCoroutine(attack(detectObts[i]));
                    attackOnce = true;
                }
                break;
            }
            else
            {
                moving = true;
            }
        }
    }

    IEnumerator attack(GameObject attackObject)
    {
        while (moving == false)
        {
            yield return new WaitForSeconds(2);

            GameObject bullet = Instantiate(this.transform.GetChild(7).gameObject, this.transform.GetChild(7).transform.position, this.transform.GetChild(7).transform.rotation);

            bullet.AddComponent<BulletDisappear>();

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
            bulletCurve.AddKey(new Keyframe(attackSpeed, attackObject.transform.localPosition.x));
            bulletClip.SetCurve("", typeof(Transform), "localPosition.x", bulletCurve);

            bulletCurve = new AnimationCurve();
            bulletCurve.AddKey(new Keyframe(0f, bullet.transform.localPosition.y));
            bulletCurve.AddKey(new Keyframe(attackSpeed, attackObject.transform.localPosition.y));
            bulletClip.SetCurve("", typeof(Transform), "localPosition.y", bulletCurve);

            bulletCurve = new AnimationCurve();
            bulletCurve.AddKey(new Keyframe(0f, bullet.transform.localPosition.z));
            bulletCurve.AddKey(new Keyframe(attackSpeed, attackObject.transform.localPosition.z));
            bulletClip.SetCurve("", typeof(Transform), "localPosition.z", bulletCurve);

            bulletAnimation.AddClip(bulletClip, bulletClip.name);
            bulletAnimation.Play(bulletClip.name);
            StartCoroutine(destoryBulletAndDamage(bullet, attackObject)); //降低物体的血量
        }
    }

    IEnumerator destoryBulletAndDamage(GameObject bullet, GameObject attackObject)
    {
        yield return new WaitForSeconds(attackSpeed);
        Destroy(bullet);

        if (attackObject.tag == "Base")
        {
            attackObject.GetComponent<BaseScript>().health -= damageNumber;
        }
        else if (attackObject.tag == "Unit")
        {
            attackObject.GetComponent<UnitScript>().health -= damageNumber;
        }
        else if (attackObject.tag == "Miner")
        {
            //attackOnce = false;//物体被摧毁才能重置
        }
    }
    IEnumerator destoryThis()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}
