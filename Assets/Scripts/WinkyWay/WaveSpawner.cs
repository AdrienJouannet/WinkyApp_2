using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private GameObject target;
    public int originSpawner;

    //create the asteroids
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    };

    [System.Serializable]

    public class Wave
    {
        public string name;
        public Transform asteroid;
        public int count;
        public float rate;
    }
    
    public Wave[] waves;
    public Transform[] spawnPoints;
    private int nextWave = 0;

    public float timeBetweenWaves = 2f; // 2s is asteroids has disappeared

    private float waveCountdown;
    private float searchCountdown;
    private int _asteroidNumber;
    //  private GameObject _targettingCol;
    private SpawnState state = SpawnState.COUNTING;
    // Use this for initialization
    void Start()
    {
        _asteroidNumber = 0;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn point reference missing");
        }
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            //check if asteroids are still there
            if (AsteroidIsStillThere() == false)
            {
                print("no more asteroids");
                print("state = " + state);
                print("wc = " + waveCountdown);
                return;
            }
            else
            {
                return;
            }
        }
        if (waveCountdown <= 0)
        {
            print("wavecountdown < 0 ");
            print(state);
            if (state != SpawnState.SPAWNING)
            {
                print("start");
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool AsteroidIsStillThere()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Asteroid") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        print("spawning wave");
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnItem(_wave.asteroid);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnItem(Transform _item) //spawn asteroid or special item 
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn point reference missing");
        }

        originSpawner = Random.Range(0, spawnPoints.Length); //number of the spawner chosen at random  
        print("origin spawner = " + originSpawner);
        Transform _sp = spawnPoints[originSpawner];
        Instantiate(_item, _sp.position, _sp.rotation);
        _item.name = "Item " + _asteroidNumber;
        _asteroidNumber++ ;
        //	_item.localScale = Vector3.one * Random.Range(0.5f, 2f);
    }
}
