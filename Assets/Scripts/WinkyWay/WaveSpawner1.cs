using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner1 : MonoBehaviour {

    /*
    private GameObject target;


    //create the asteroids
    public enum SpawnState {
		SPAWNING, WAITING, COUNTING
	};

	[System.Serializable]

	public class Wave
	{
		public string name;
	//	public Transform asteroid;
		public int count;
		public float rate;
        public Asteroid asteroid;
    }
	public Wave[] waves;
	public Transform[] spawnPoints;
	private int nextWave = 0;

	public float timeBetweenWaves = 2f; // 2s is asteroids has disappeared

	private float waveCountdown;
	private float searchCountdown;

  //  private GameObject _targettingCol;
	private SpawnState state = SpawnState.COUNTING;
	// Use this for initialization
	void Start () {
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("Spawn point reference missing");
		}
		waveCountdown = timeBetweenWaves;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (state == SpawnState.WAITING)
		{
			//check if asteroids are still there
			if (AsteroidIsStillThere() == false)
			{
				print("no more asteroids");
				return;
			}
			else
			{
				return;
			}
		}	
		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
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
		print ("spawning wave");
		state = SpawnState.SPAWNING;
		for (int i = 0; i < _wave.count; i++)
		{
			SpawnItem(_wave.asteroid);
			yield return new WaitForSeconds(1f/_wave.rate);
		}
		state = SpawnState.WAITING;
		yield break;
	}
  
     Spawn asteroid or special item  with two values : 
      the origin spawner, giving the INDEX of the final collider to check
     

    
    void SpawnItem(Asteroid _item) 
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("Spawn point reference missing");
		}
        int originSpawner = Random.Range(0, spawnPoints.Length); //number of the spawner chosen at random  
        Transform _sp = spawnPoints[originSpawner];
        print(_sp.gameObject.name);
        Instantiate(_item, _sp.position, _sp.rotation);
        _item.origin = originSpawner;
        print("CIBLE = " + originSpawner);
        print("origin ="  + _item.origin);
        //	_item.localSale = Vector3.one * Random.Range(0.5f, 2f);

    }*/
}
