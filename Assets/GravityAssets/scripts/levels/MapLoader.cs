using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

	GravityLevels.GravityMap gm;
	public GameObject MapParent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void clearBlocks() {
		foreach (Transform t in MapParent.transform) {
			Destroy(t.gameObject);
		}
	}

	public void LoadMap(GravityLevels.GravityMap map) {
		if(gm != null) clearBlocks ();
		gm = map;
		map.LoadMap (this);
	}

	public enum START_STRATEGY {
		TOP, CENTER
	}
	public void GenerateStart(int Y, GravityLevels.GravityMap gm) {
		for (int j = (gm.w/2)-2; j < (gm.w/2)+2; j++) {
			GenerateBlock (j, Y, gm, GravityLevels.MapGeneration.brick);
		}
	}

	public void GenerateSolidRow(int Y, GravityLevels.GravityMap gm) {
		for (int j = 0; j < gm.w; j++) {
			GenerateBlock (j, Y, gm, GravityLevels.MapGeneration.brick);
		}
	}

	public void GenerateSides(int Y, GravityLevels.GravityMap gm) {
		GenerateBlock (-1, Y, gm, GravityLevels.MapGeneration.brick);
		GenerateBlock (gm.w, Y, gm, GravityLevels.MapGeneration.brick);
	}

	public void GenerateBlock(int col, int row, GravityLevels.GravityMap gm, GameObject block, int default_z = 0){
		GameObject bb = Instantiate(block, new Vector3(col - (-.5f + ((float)gm.w / 2f)), -row, default_z), Quaternion.identity) as GameObject;
		bb.transform.parent = MapParent.transform;
	}

	public void ResetMap() {
		if (gm != null) {
			clearBlocks ();
			gm.LoadMap (this);
		}
	}
}
