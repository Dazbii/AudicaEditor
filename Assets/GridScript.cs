using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour {

	public GameObject point;
	public GameObject musicController;
	public Material noteMaterial;
    public Material pointMaterial;
	public Dropdown difficulty;

	// private List<Cue> cues;

	private CuesContainer cuesContainer;
	private GameObject[,] grid;
	private static int gridSize = 12;
	private GameObject selected = null;
	private bool clickReset = true;
	private int jumpSpeed = 800;

	// Use this for initialization
	void Start () {
		cuesContainer = new CuesContainer();
		grid = new GameObject[12, 7];
		for (int i = 0; i < 12; i++) {
			for (int j = 0; j < 7; j++) {
				grid[i,j] = Instantiate(point, new Vector3(this.transform.position.x + (i * gridSize), this.transform.position.y + (j * gridSize), this.transform.position.z), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float minDist = Mathf.Infinity;
		GameObject min = null;
		int minX = 0;
		int minY = 0;
        for (int i = 0; i < 12; i++) {
            for (int j = 0; j < 7; j++) {
				if (Vector3.Distance(mousePos, grid[i,j].transform.position) < minDist) {
					min = grid[i, j];
					minDist = Vector3.Distance(mousePos, grid[i, j].transform.position);
					minX = i;
					minY = j;
				}
			}
		}
		if (selected) {
            selected.transform.localScale = new Vector3(1, 1, 1);
		}
		selected = min;
		List<int> usedPositions = new List<int>(); // Yo this real gross you gotta get rid of it
		foreach (Cue c in cuesContainer.cues) {
			float timeDiff = c.GetTick() - musicController.GetComponent<AudioController>().GetCurrentTick();
			if(0 <= timeDiff && timeDiff < jumpSpeed) {
				if (timeDiff == 0) {
                    grid[c.GetRow(), c.GetCol()].GetComponent<Renderer>().material = noteMaterial;
				}
				grid[c.GetRow(), c.GetCol()].transform.localScale = new Vector3(Mathf.Lerp(10, 1, timeDiff/jumpSpeed), Mathf.Lerp(10, 1, timeDiff/jumpSpeed), Mathf.Lerp(10, 1, timeDiff/jumpSpeed));
				usedPositions.Add((10 * c.GetCol()) + c.GetRow()); // This shit right here dude, I'm tellin u it real gross
			} else if(!usedPositions.Contains((10 * c.GetCol()) + c.GetRow())) { // Get this shit outta here man, u can't keep doin this
                grid[c.GetRow(), c.GetCol()].GetComponent<Renderer>().material = pointMaterial;
                grid[c.GetRow(), c.GetCol()].transform.localScale = new Vector3(1, 1, 1);
			}
		}
		usedPositions.Clear();
        min.transform.localScale = new Vector3(10, 10, 10);
		if (Input.GetMouseButtonDown(0) && clickReset) {
			clickReset = false;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition))) {
                cuesContainer.cues.Add(new Cue().SetPos(minX, minY).SetTick(musicController.GetComponent<AudioController>().GetCurrentTick()));
			}
		}
		if (Input.GetMouseButtonUp(0)) {
			clickReset = true;
		}
	}

	public void Export() {
		cuesContainer.cues.Sort();
		string jsonString = JsonUtility.ToJson(cuesContainer, true);
		using (StreamWriter streamWriter = File.CreateText("./" + difficulty.options[difficulty.value].text + ".cues")) {
			streamWriter.Write(jsonString);
		}
	}
}
