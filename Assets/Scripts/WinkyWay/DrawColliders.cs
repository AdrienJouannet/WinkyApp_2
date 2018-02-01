using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using UnityEngine;

public class DrawColliders : MonoBehaviour {

	PolygonCollider2D[] hex;

	private Vector2[] OuterColl;
	private Vector2[] CenterColl;

	
	void Start () {
			SetPoly();
	}

	private Vector2[] GetHexCoordinates(float r)
	{
		Vector2[] hex = new Vector2[6];
		float n = 6;
		for (var j = 0; j < n; j++)
		{
			hex[j] = new Vector2(r * Mathf.Cos(2 * Mathf.PI * j / n), r * Mathf.Sin(2 * Mathf.PI * j / n));
		}
		return(hex);
	}

	public void SetPoly()
	{
		int j = -1;
		hex = GetComponentsInChildren<PolygonCollider2D>();
		OuterColl = GetHexCoordinates(4f);
		CenterColl = GetHexCoordinates(2f);
		foreach (var col in hex)
		{
			Vector2[] points = col.GetPath(0);
			if (points.Length > 4)
			{
				for (int i = 0; i < points.Length; i++)
				{
					points[i] = CenterColl[i];
				}
			}
			else
			{			
				j++;
				for (int i = 0; i < points.Length; i++)
				{

					if (i == 0)
						points[i] = CenterColl[j];
					else if (i == 1)
					{
						if (j == 5)
							points[i] = CenterColl[0];
						else
							points[i] = CenterColl[j + 1];
					}
					else if (i == 2)
					{
						if (j == 5)
							points[i] = OuterColl[0];
						else
							points[i] = OuterColl[j + 1];
					}
					else if (i == 3)
						points[i] = OuterColl[j];
				}				
			}
			col.SetPath(0, points);
		}

	}
}
