using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class Waves
{
    
    public List<GameObject> wave;
    public int spawnTime = 3;

}
[System.Serializable]
public class WavesList
{

    public List<Waves> waves_list;

}

public class Spawn_script : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI waveText;

    [SerializeField]
    TextMeshProUGUI nextWave;

    public bool switchingWave;

    [SerializeField]
    WavesList obj = new WavesList();

    [SerializeField]
    int wave_no = 0;
    [SerializeField] ParticleSystem PS;

    int current=0;
    int prev=0;
    int currentFlying=0;
    int prevFlying=0;
    public List<Transform> spawnpoints_active = new List<Transform>();
    public List<Transform> spawnpoints_active_flying = new List<Transform>();
    public List<Transform> spawnpoints_passive = new List<Transform>();
    public List<Transform> spawnpoints_passive_flying = new List<Transform>();
    GameObject CanvasManager;

    public List<GameObject> spawned_enemy = new List<GameObject>();

    public Transform[] p2a;
    private int noOfWaves;
    public void Awake()
    {

    }

    void Start()
    {
        StartCoroutine(delay());
        CanvasManager = GameObject.FindObjectOfType<PauseScript1>().gameObject;
        switchingWave = false;
        nextWave.text = "";
        waveText.text = "Wave 1";
        GameObject[] obj2 = GameObject.FindGameObjectsWithTag("spawn_point");
        foreach (GameObject t in obj2)
        {
            if (t.transform.parent.name.Equals("Active"))
            { spawnpoints_active.Add(t.transform); }
            if (t.transform.parent.name.Equals("Active_1"))
            { spawnpoints_active_flying.Add(t.transform); }

            /*else
            { spawnpoints_passive.Add(t.transform); }*/

        }
        noOfWaves = obj.waves_list.Count;
    }


    void Update()
    {
        
       /* while (obj.waves_list[wave_no].wave.Count > 0)
        {
            StartCoroutine(delay());
            //Invoke("spawnEnemy", obj.waves_list[wave_no].spawnTime);
        }*/
        if (obj.waves_list[wave_no].wave.Count == 0 && spawned_enemy.Count  == 0)
        {
            nextWave.text = "Press P for Next Wave";
            switchingWave = true;
        }


        if (switchingWave)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PS.Play();
                Invoke("updateWave", PS.main.duration);
            }
        }
        /*if (obj.waves_list[wave_no].wave.Count == 0)
        {
            wave_no++;
        }
        */
    }
    void spawnEnemy()
    {
        int enemyCount = obj.waves_list[wave_no].wave.Count;
        int enemyId = Random.Range(0, enemyCount);
        GameObject currentEnemy = obj.waves_list[wave_no].wave[enemyId];
        
        if (currentEnemy.CompareTag("drone") || currentEnemy.CompareTag("drone1"))
        {
            current = Random.Range(0, spawnpoints_active_flying.Count);
            while (current == prev)
            { current = Random.Range(0, spawnpoints_active_flying.Count); }
            spawned_enemy.Add(Instantiate(currentEnemy, spawnpoints_active_flying[current].position, Quaternion.identity));
            prev = current;
        }
        else
        {
            currentFlying = Random.Range(0, spawnpoints_active.Count);
            while (currentFlying == prevFlying)
            { currentFlying = Random.Range(0, spawnpoints_active.Count); }
            spawned_enemy.Add(Instantiate(currentEnemy, spawnpoints_active[Random.Range(0, spawnpoints_active.Count)].position, Quaternion.identity));
            prevFlying = currentFlying;
        }
           obj.waves_list[wave_no].wave.Remove(currentEnemy);
    }
    IEnumerator delay()
    {
        while (true)
        {
            if (obj.waves_list[wave_no].wave.Count > 0)
            {
                yield return new WaitForSeconds(obj.waves_list[wave_no].spawnTime);
                spawnEnemy();

            }
            else
                yield return null;
        }
    }
    public void enemy_killed(GameObject killed_enemy)
    {
        spawned_enemy.Remove(killed_enemy);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Region"))
        {
            p2a= other.GetComponentsInChildren<Transform>();

            foreach (Transform t in p2a)
            {
                t.SetParent(GameObject.FindGameObjectWithTag("Active").transform);
                spawnpoints_active.Add(t.transform);
            }
            spawnpoints_passive.Remove(other.transform);
            spawnpoints_active.Remove(other.transform);
            Destroy(other);

        }
        else if (other.CompareTag("RegionFlying"))
        {
            p2a = other.GetComponentsInChildren<Transform>();

            foreach (Transform t in p2a)
            {
                t.SetParent(GameObject.FindGameObjectWithTag("Flying").transform);
                spawnpoints_active_flying.Add(t.transform);

            }
            spawnpoints_passive_flying.Remove(other.transform);
            spawnpoints_active_flying.Remove(other.transform);
            Destroy(other);
        }
    }
    private void updateWave()
    {
        if(wave_no < noOfWaves - 1)
        {
            wave_no++;
        }
        else
        {
            CanvasManager.GetComponent<PauseScript1>().endGame();
        }
        
        waveText.text = "Wave " + (wave_no + 1).ToString();
        nextWave.text = "";
        switchingWave = false;
        
    }

}