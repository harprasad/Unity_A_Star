using UnityEngine;
using System.Collections;

public class Grid_script
{
	public node[,] grid ;
	
	public void create_grid (float x_len, float y_len, uint x_cell_height, uint y_cell_width)
	{
		int number_of_x_cells = (int)(x_len / x_cell_height);
		int number_of_y_cells = (int)(y_len / y_cell_width);
		grid = new node[number_of_x_cells, number_of_y_cells];
		 
	}

	public void draw_grid (GameObject grid_cell_prefab)
	{
		foreach (node cell in grid) {
			Vector3 posi = cell.position;
			GameObject cell_obj = (GameObject)GameObject.Instantiate (grid_cell_prefab, posi, Quaternion.identity);
			Renderer rendrr = (Renderer)cell_obj.GetComponent ("Renderer");
			GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacles"); 
			foreach (GameObject obstas in obstacles) {
				Renderer obstas_rendrr = (Renderer)obstas.GetComponent ("Renderer");
				if (obstas_rendrr.bounds.Intersects (rendrr.bounds)) {
					//rendrr.material.color = Color.black;
					cell.walkable = false;
				}
			}
		}
	}

	public void plot_cells (Vector3 start_posi, uint x_cell_height, uint y_cell_width)
	{
		int xlen = grid.GetLength (0);
		int ylen = grid.GetLength (1);
		for (int x = 0; x < xlen; x++) {
			for (int y = 0; y< ylen; y++) {
				Vector3 posi = start_posi;
				posi.x += x_cell_height * x;
				posi.z += y_cell_width * y;
				grid [x, y] = new node (posi, true);
			}
		}
	}
}
