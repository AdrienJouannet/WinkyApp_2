using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentTiles : MonoBehaviour
{
    public static float radius = 1.0f;
    public static Vector3 origin;
    public static List<GameObject> PossibleMoves = new List<GameObject>();

    private void Start()
    {
     //   SearchForNeighbourgTiles(this.gameObject);
    }
    public static void SearchForNeighbourgTiles(GameObject go)
    {
        origin = go.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
        foreach (Collider col in hitColliders)
        {
			if (col.gameObject.name != "Plane" && (col.gameObject.tag == "Path" || col.gameObject.tag == "WinkyPosition"))
            {
    //            Renderer rend = col.gameObject.GetComponent<Renderer>();
     //           rend.material.color = Color.red;
                PossibleMoves.Add(col.gameObject);
            }    //      print(col.name);
        }
    }
}


