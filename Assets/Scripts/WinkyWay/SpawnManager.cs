using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{


    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    public int originSpawner;


    [System.Serializable] // allows us to change values inside of inspector
    public class Wave
    {
        public string name; //name of wave
        public Transform item; // refrence to prehab we want to initiate
        public int count; // amount of waves
        public float rate; // spawn rate
        public float speed; // asteroid speed in sec
    }

    public Wave[] waves;
    private int nextWave = 0;

    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f; //time between waves 5 seconds
    private float waveCountdown;
    private int _asteroidNumber;

    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f
        ; //instantiate to check if all enemy are dead every 1 second instead of every frame

    private SpawnState state = SpawnState.COUNTING;

    public SpawnState State
    {
        get { return state; }
    }

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced."); // checks if a spawn point of refrenced
        }
        waveCountdown = timeBetweenWaves; //how long till next wave starts 
    }

    void Update()
    {
        if (state == SpawnState.WAITING) //checks if asteroids are still alive
        {
        //    if (!AsteroidIsStillThere())
         //   {
                WaveCompleted(); // if all asteroids dead start next wave
         //   }
         //   else
          //  {
         //       return; // if asteroids still there return till all asteroids dead
       //     }
        }
        if (waveCountdown <= 0) //if <= 0 then start check if spawning of not start spawning then else
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave])); //start spawning wave
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime; //makes countdown relevent to time and not frames per second
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (nextWave + 1 > waves.Length - 1) // if no more waves start wave 1 again looping
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping..."); // if final wave not completed add 1 wave
        }
        else
        {
            nextWave++; //increases wave by 1
        }
    }

    bool AsteroidIsStillThere()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f; //check if all asteroid are dead every 1 second instead of every frame
            if (GameObject.FindGameObjectWithTag("Asteroid") == null) //checks if all asteroid with the player tag asteroid are alive or dead
            {
                return false; // if enemy alive repeat step till enemies are dead check this every 1 second
            }
        }
        return true; // if all asteroid with tag asteroid are dead then return true
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING; //spawns asteroid
        for (int i = 0; i < _wave.count; i++) // = number of asteroid we want to spawn
        {
            SpawnItem(_wave.item, _wave.speed); // then waits for player to avoid asteroids
            {
                if (i == _wave.count - 1)
                {
                    yield return 0;

                }
            }
            yield return
                new WaitForSeconds(1f / _wave.rate); //how long we want to wait 1 / rate untill we have number of asteroid we want to spawn
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnItem(Transform _item, float speed) //spawn asteroid or special item 
    {
        Asteroid ast;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn point reference missing");
        }

        originSpawner = Random.Range(0, spawnPoints.Length); //number of the spawner chosen at random  
        print("origin spawner = " + originSpawner);
        Transform _sp = spawnPoints[originSpawner];
        Instantiate(_item, _sp.position, _sp.rotation);
        _item.name = "Item " + _asteroidNumber;
        ast = _item.GetComponent<Asteroid>();
        ast.speed = speed;
        _asteroidNumber++ ;
    }
}
