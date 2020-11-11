using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWave : MonoBehaviour
{
    public int waves = 3;//调整敌人波数，目前总共三波

    public int firstWaveEnemyNumber = 4;//第一波敌人的数量

    public GameObject[] generatePoints;

    public GameObject endGame;

    public int CurrentWaveEnemyNumber;//这一波需要生成的敌人数量

    public float enemyTime = 0.5f;//每隔多久生成一个敌人

    int currentWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        //Debug.Log(enemys.Length);
        //Debug.Log(generate);
        if (enemys.Length <= 0)
        {
            currentWave += 1;
            EnemyWaveTips(currentWave);
            //generate = false;
        }
    }

    void EnemyWaveTips(int wave)
    {
        endGame.SetActive(true);
        if (wave == 1)
        {
            endGame.transform.GetChild(1).GetComponent<Text>().text = wave.ToString() + "st" + "" + "Wave";
        }
        else if(wave == 2)
        {
            endGame.transform.GetChild(1).GetComponent<Text>().text = wave.ToString() + "nd" + "" + "Wave";
        }
        else
        {
            endGame.transform.GetChild(1).GetComponent<Text>().text = wave.ToString() + "rd" + "" + "Wave";
        }

        enemy(wave);
        StartCoroutine(CancelTips());
    }

    IEnumerator CancelTips()
    {
        yield return new WaitForSeconds(1f);

        endGame.transform.GetChild(1).GetComponent<Text>().text = "";
        endGame.SetActive(false);
    }

    void enemy(int whichWave)
    {
        CurrentWaveEnemyNumber = (int)(firstWaveEnemyNumber * whichWave);//每过一次波数，敌人数量增加。

        for (int i = 0; i< CurrentWaveEnemyNumber; i++)
        {
            enemyGenerate();
        }
    }

    void enemyGenerate()
    {
        int generatePoint = (int)Random.Range(0, generatePoints.Length);

        //Debug.Log(generatePoint);

        if (generatePoint == 0 || generatePoint == 2)
        {
            GameObject enemy = (GameObject)Instantiate(Resources.Load("Enemy"));
            enemy.transform.position = new Vector3(generatePoints[generatePoint].transform.position.x, generatePoints[generatePoint].transform.position.y, generatePoints[generatePoint].transform.position.z + Random.Range(-70, 70));

            //Debug.Log(enemy.transform.position);

            enemy.GetComponent<EnemyScript>().startPosition = enemy.transform.position;

            enemy.name = "enemy";
        }
        else
        {
            GameObject enemy = (GameObject)Instantiate(Resources.Load("Enemy"));
            enemy.transform.position = new Vector3(generatePoints[generatePoint].transform.position.x + Random.Range(-70, 70), generatePoints[generatePoint].transform.position.y, generatePoints[generatePoint].transform.position.z);
            //Debug.Log(enemy.transform.position);
            enemy.GetComponent<EnemyScript>().startPosition = enemy.transform.position;

            enemy.name = "enemy";
        }
    }
}
