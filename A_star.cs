using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class A_star
{
	List<node> closed_nodes = new List<node> ();
	List<node> open_nodes = new List<node> ();

	public void compute_h_costs (node[,] grid, node desination)
	{
		foreach (node n in grid) {
			n.extimated_cost = Vector3.Distance (n.position, desination.position);
		}
		foreach (node n in grid) {
			if (n.walkable == false) {
				closed_nodes.Add (n);
			} else {
				open_nodes.Add (n);
			}
		}
	}
	
	public float fx (node current_node, node destiny_node)
	{
		float travel_cost = Vector3.Distance (current_node.position, destiny_node.position);
		return travel_cost + destiny_node.extimated_cost;
	}
	
	public node chose_node (List<node> neighbour_nodes, node current_node)
	{
		List<float> fx_vals = new List<float> ();
		foreach (node nod in neighbour_nodes) {
			float fx_val = fx (current_node, nod);
			fx_vals.Add (fx_val);
		}
		fx_vals.Sort ();
		foreach (node nod in neighbour_nodes) {
			float fx_cost =fx (current_node, nod);
			if(fx_cost == fx_vals [0]) {
				return nod;
			}
		}
		return current_node;
	}

	public List<node> find_neighbour_nodes (node current, node[,] grid)
	{
		List<node> neighbours = new List<node> ();
		float max_distance = Vector3.Distance (grid [0, 0].position, grid [1, 1].position);
		foreach (node n in open_nodes) {
			float node_distance = Vector3.Distance (current.position, n.position);
			if(node_distance == 0) continue;
			if (node_distance <= max_distance) {
				neighbours.Add (n);
			}
		}
		return neighbours;
	}

	public List<node> A_star_find_path (node current, node goal, node[,] grid)
	{
		List<node> path = new List<node> ();
		if (goal.walkable == false) {
			Debug.Log ("you clicked on a non rechable area");
			return path;
		}
		compute_h_costs (grid, goal); //pre compute h costs
		//find all open neighbour nodes arround current node
		node current_n = current;
		while (current_n != goal) {
			if(!path.Contains(current_n))
				path.Add (current_n);
			List<node> neighbours = find_neighbour_nodes (current_n, grid);
			if(neighbours.Count == 0){
				Debug.Log("dead lock");
				path.Remove(current_n); //remove it from path
				open_nodes.Remove(current_n);
				closed_nodes.Add(current_n);
				current_n = path[path.Count-2];
//				open_nodes.Add(current_n);
//				if (path.Count > 22 ) {
//					Debug.Log("No route possible");
//					break;
//				}
//				break;
				continue;
			}
			node next_node = chose_node (neighbours, current_n);
			if(next_node == current_n)
			{
				//failed to choose because no open neighbour exits 
				Debug.Log("No route possible");
				break;
			}
			current_n = next_node;
			open_nodes.Remove(current_n);
			if(path.Contains(current_n)){
				//path reversing 
				node last_entry = path[path.Count-1];
				path.Remove(last_entry);
				open_nodes.Remove(last_entry); //remove last node
				closed_nodes.Add(last_entry);  //add that to closed path
			}
		}
		if(current_n == goal){
			Debug.Log("It worked you got it");
		}
		return path;
	}

	public node position_to_node(Vector3 posi,node[,] grid){
		List<float> distances = new List<float>();

		foreach (node n in grid) {
			float dis = Vector3.Distance(n.position,posi);
			distances.Add(dis);
		}
		distances.Sort ();
		foreach (node n in grid) {
			float dis = Vector3.Distance(n.position,posi);
			if(dis == distances[0]){
				return n;//nearest node
			}
		}
		return grid [0, 0];
	}
}
