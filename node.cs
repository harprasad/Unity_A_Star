using UnityEngine;
using System.Collections;

public class node {
	public Vector3 position ;
	public bool walkable;
	public float extimated_cost ;
	public node(Vector3 _position ,bool _walkable){
		position = _position;
		walkable = _walkable;
	}
}
