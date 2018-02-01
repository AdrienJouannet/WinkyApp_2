using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    private GameObject currentTile;
	private GameObject prevTile;
	private Transform target;
//    Winky winky;

	private MovablesMoves movablesMoves;
	private DefenseMode _defenseMode;

//	private EnemyDestroyUnit _enemyDestroyUnit;

	void Start()
	{
		currentTile = getOriginTile(gameObject);
		prevTile = currentTile;
//		winky = GameObject.Find("Winky").GetComponent<Winky>();

		GameObject Movables = GameObject.Find ("Movables");
		movablesMoves = Movables.GetComponent<MovablesMoves> ();
		_defenseMode = Movables.GetComponent<DefenseMode> ();
//		_enemyDestroyUnit = GameObject.Find ("Units").transform.GetComponent <EnemyDestroyUnit> ();
	}

	GameObject getOriginTile(GameObject go)
	{
		Vector3 origin;

		origin = go.transform.position;
		Collider[] hitColliders = Physics.OverlapSphere(origin, 0.2f);
		foreach (Collider col in hitColliders)
		{
			if (col.gameObject.name != "Plane" && col.gameObject.tag == "Path")
				return(col.gameObject);        
		}
		return null;
	}

	public IEnumerator GetPaths(MovableScript script)
	{
		AdjacentTiles.SearchForNeighbourgTiles(currentTile); //récupere la liste des cases adjacentes
		foreach (GameObject target in AdjacentTiles.PossibleMoves)
		{
			//            print(target.name);
			if ((target.name != currentTile.name) && (target.name != prevTile.name) &&
				((target.transform.position.x == transform.position.x) || (target.transform.position.z == transform.position.z)))//si la case n'est pas en diagonale
			{
				//				print("new target = " + target.name);

				Transform toMove = this.gameObject.transform;
				Vector3 startPosition = toMove.position;
				Vector3 endPosition = target.transform.position;
				AdjacentTiles.PossibleMoves.Clear ();
				script.nbMoves++;
//				Debug.Log ("enemy move from " + startPosition + " to " + endPosition);
				yield return (StartCoroutine (movablesMoves.MoveFromTo (toMove, startPosition, endPosition, script)));
				//              gameObject.transform.position = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z);

//				_enemyDestroyUnit.CheckDestroyUnit (endPosition);

				prevTile = currentTile;
				currentTile = target;
				//                print("prev tile = " + prevTile.name + "current = " + currentTile.name);
				if(currentTile.tag == "WinkyPosition")
				{
					_defenseMode.ReturnToDefense ();
				}
				break;
			}        
		}
	}

//    public void GetPaths2()
//    {
////        print("Get paths");
// 
//        AdjacentTiles.SearchForNeighbourgTiles(currentTile); //récupere la liste des cases adjacentes
//        foreach (GameObject target in AdjacentTiles.PossibleMoves)
//        {
////            print(target.name);
//			if ((target.name != currentTile.name) && (target.name != prevTile.name) &&
//				((target.transform.position.x == transform.position.x) || (target.transform.position.z == transform.position.z)))//si la case n'est pas en diagonale
//			{
////				print("new target = " + target.name);
//
//				Transform toMove = this.gameObject.transform;
//				Vector3 startPosition = toMove.position;
//				Vector3 endPosition = target.transform.position;
//
//				StartCoroutine (movablesMoves.MoveFromTo (toMove, startPosition, endPosition));
////              gameObject.transform.position = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z);
//                prevTile = currentTile;
//                currentTile = target;
////                print("prev tile = " + prevTile.name + "current = " + currentTile.name);
//                if(currentTile.tag == "WinkyPosition")
//                {
//					winky.TakeDamage(1.0f);
//                    Destroy(gameObject);
//                }
//				AdjacentTiles.PossibleMoves.Clear ();
//				break;
//            }        
//        }
//    }
}