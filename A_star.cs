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
			if (fx (current_node, nod) == fx_vals [0]) {
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
			if (node_distance <= max_distance) {
				neighbours.Add (n);
			}
		}
		return neighbours;
	}

	public List<node> A_star_find_path (node current, node goal, node[,] grid)
	{
		List<node> path = new List<node> ();
		compute_h_costs (grid, goal); //pre compute h costs
		//find all open neighbour nodes arround current node
		node current_n = current;
		while (current_n != goal) {
			path.Add (current_n);
			List<node> neighbours = find_neighbour_nodes (current_n, grid);
			if(neighbours.Count == 0){
				Debug.Log("No route possible");
				break;
			}
			node next_node = chose_node (neighbours, current_n);
			if(next_node == current_n)
			{
				//failed to choose because no open neighbour exits 
				Debug.Log("No route possible");
				break;
			}
			current_n = next_node;
			if(path.Contains(current_n)){
				//path reversing 
				open_nodes.Remove(path[path.Count]); //remove last node
				closed_nodes.Add(path[path.Count]);  //add that to closed path
			}
		}
		return path;
	}
}
