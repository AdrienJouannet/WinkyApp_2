using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public Transform target;
    [SerializeField]
    private GameObject _mainActionPanel;
    [Header("Attributes")]
    public float range = 2f;
    public float fireRate = 1f; // 1 bullet per seconds
//    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

//	private UnitClic _unitPanelManager;
	private Vector3 _initialPosition;
	private Vector3 _initialRotation;

	private Transform _enemies;
	private Transform _obstacles;
	private Transform[] _winkyTiles;
	private GameObject _tile;

    private GameObject _activeActionPanel;

    // Use this for initialization
	void Start () {
		_enemies	= GameObject.Find ("Enemies").transform;
		_obstacles	= GameObject.Find ("Obstacles").transform;
		_winkyTiles = GameObject.Find ("Grid").GetComponent<BuildGrid> ().GetWinkyTiles ();


		SetInitialPosition (this.transform.position);
		SetInitialRotation (this.transform.eulerAngles);
//        InvokeRepeating("UpdateTarget", 0f, 0.5f); // Appelle UpdateTarget 2x par secondes
//		_unitPanelManager = this.gameObject.GetComponent<UnitClic> ();
        _activeActionPanel = _mainActionPanel;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			if (this.GetComponent<InMoveState> ().GetInMove ())
			{
				GameObject.Find ("Movables").GetComponent<MovableScript> ().nbMoves--;
				this.GetComponent<InMoveState> ().SetInMove (false);
			}
			this.gameObject.SetActive (false);
		}
	}
	
    public void UpdateTarget()
    {
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; // shortest dist to enemy to choose the closest
		Transform nearestEnemy = null;
		float angle;

//		Debug.Log("NOUVEAU TOUR\n");
		foreach (Transform enemy in _enemies) {
//            Vector3 directionToTarget = transform.position - enemy.transform.position;
//            float angle = Vector3.SignedAngle(transform.position, enemy.transform.position, transform.up);
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.position);

			angle = Vector3.Angle (transform.forward, enemy.position - transform.position);
//			Debug.Log ("enemy angle " + angle);

			if ((distanceToEnemy < shortestDistance) && angle < 10) {
				shortestDistance = distanceToEnemy;
//				Debug.Log ("distance ennemi: " + shortestDistance);

				nearestEnemy = enemy;
			}
		}
//		Debug.Log ("Enemy: " + shortestDistance);
		foreach (Transform obstacle in _obstacles) {
			float distanceToObstacle = Vector3.Distance (transform.position, obstacle.position);
//			Debug.Log ("obstacle: " + distanceToObstacle);

			angle = Vector3.Angle (transform.forward, obstacle.position - transform.position);
//			Debug.Log ("obstacle angle " + angle);

			if ((distanceToObstacle < shortestDistance) && angle < 15) {
				shortestDistance = distanceToObstacle;
//				Debug.Log ("ShortestDistance: " + shortestDistance);
				nearestEnemy = null;
				break ;
			}
		}
//		Debug.Log("ici");
		foreach (Transform winkyTile in _winkyTiles)
		{
			float distanceToWinkyTile = Vector3.Distance (transform.position, winkyTile.position);
			angle = Vector3.Angle (transform.forward, winkyTile.position - transform.position);
			if ((distanceToWinkyTile <= shortestDistance) && angle < 15) {
				shortestDistance = distanceToWinkyTile;
				nearestEnemy = null;
				break ;
			}
		}

//		Debug.Log ("distance updated: " + shortestDistance);
		if (nearestEnemy != null && shortestDistance <= range) {
//			Debug.Log ("distance updated: " + shortestDistance);
			target = nearestEnemy;
		} else {
			target = null;
		}
//		Debug.Log (">>> Target updated.");
    }

	// Update is called once per frame
/*	void Update () {
        if (target == null)
            return;
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }
    */
    public void Shoot ()
    {
		if (target == null) {
//			Debug.Log ("No target.");
			return;
		}
//		print("SHOOT ! target  = "+target.name);
//        if (fireCountdown <= 0f)
//        {
//            fireCountdown -= Time.deltaTime;
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
//		bulletGO.transform.position = new Vector3 (firePoint.position.x, 0.2f, firePoint.position.z);
//		bulletGO.transform.localEulerAngles = new Vector3 (0, 0, 90);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
//            fireCountdown = 1f / fireRate;
            if (bullet != null)
            {
                bullet.Seek(target);
            }
//        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range); //current position, radius    
        float totalFOV = 30.0f;
        float rayRange = 5f;
        float halfFOV = totalFOV / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
    }

	public Vector3 GetInitialPosition()
	{
		return _initialPosition;
	}

	public void SetInitialPosition(Vector3 position)
	{
		_initialPosition = position;
	}

	public Vector3 GetInitialRotation()
	{
		return _initialRotation;
	}

	public void SetInitialRotation(Vector3 rotation)
	{
		_initialRotation = rotation;
	}

	public GameObject GetTile()
	{
//		if (_tile == null)
//			Debug.Log ("_tile is null!");
//		else
//			Debug.Log ("Get Tile at : " + _tile.transform.position);
		return (_tile);
	}

	public void SetTile(GameObject tile)
	{
		_tile = tile;
//		Debug.Log ("_tile is set : " + _tile.transform.position);
	}

    public GameObject GetActiveActionPanel()
    {
        return (_activeActionPanel);
    }

    public void SetActiveActionPanel(GameObject actionPanel)
    {
        _activeActionPanel = actionPanel;
    }
}
