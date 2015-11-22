using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class path_finder : MonoBehaviour {
	public GameObject grid_cell_prefab;
	public GameObject path_prefab;
	public Transform start_point;
	Grid_script gs = new Grid_script();
	A_star astr = new A_star();
	// Use this for initialization
	void Start () {
		gs.create_grid (100f, 100f, (uint)grid_cell_prefab.transform.localScale.x, (uint)grid_cell_prefab.transform.localScale.z);
		gs.plot_cells (start_point.position, (uint)grid_cell_prefab.transform.localScale.x, (uint)grid_cell_prefab.transform.localScale.z);
		gs.draw_grid (grid_cell_prefab);
		StartCoroutine(draw_path());
	}
	
	// Update is called once per frame
	void Update () {
		int obs = 0;
		int cells = 0;
		foreach (node n in gs.grid) {
			cells++;
			if(n.walkable == false){
				obs++;
			}
		}
		print ("found " + obs + " blocked grids out of "+ cells);
	
	}

	IEnumerator draw_path() {

	
		List<node> path= astr.A_star_find_path (gs.grid [0, 0], gs.grid [32,32], gs.grid);
		foreach (node n in path) {
			GameObject.Instantiate(path_prefab,n.position,Quaternion.identity);
			yield return new WaitForSeconds(1);
		}
	}
}
