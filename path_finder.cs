using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class path_finder : MonoBehaviour {
	public GameObject grid_cell_prefab;
	public GameObject path_prefab;
	public Transform start_point;
	public GameObject particles;

	Grid_script gs = new Grid_script();
	A_star astr = new A_star();
	// Use this for initialization
	void Start () {
		gs.create_grid (100f, 100f, (uint)grid_cell_prefab.transform.localScale.x, (uint)grid_cell_prefab.transform.localScale.z);
		gs.plot_cells (start_point.position, (uint)grid_cell_prefab.transform.localScale.x, (uint)grid_cell_prefab.transform.localScale.z);
		gs.draw_grid (grid_cell_prefab);
	//	StartCoroutine(draw_path());
	}
	
	// Update is called once per frame
	void Update () {
		on_mouse_click ();
	}

	IEnumerator draw_path() {

	
		List<node> path= astr.A_star_find_path (gs.grid [0, 0], gs.grid [32,32], gs.grid);
		foreach (node n in path) {
			GameObject.Instantiate(path_prefab,n.position,Quaternion.identity);
			yield return new WaitForSeconds(1);
		}
	}

	void on_mouse_click(){
		if(Input.GetMouseButtonUp(0)){
			Vector3 clickpos = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(clickpos);
			RaycastHit hitinfo;
			if(Physics.Raycast(ray,out hitinfo)){
				GameObject.Instantiate(particles,hitinfo.point,Quaternion.Euler(-90f,0f,0f));
				List<node> path= astr.A_star_find_path (gs.grid [0, 0],astr.position_to_node(hitinfo.point,gs.grid), gs.grid);
				//clear old path
				GameObject[] old_path_balls = GameObject.FindGameObjectsWithTag("pathballs");
				foreach(GameObject gob in old_path_balls){
					GameObject.Destroy(gob);
				}
				foreach (node n in path) {
					GameObject.Instantiate(path_prefab,n.position,Quaternion.identity);
				}
			}
		}
	}
}
