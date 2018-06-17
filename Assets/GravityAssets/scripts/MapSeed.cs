using UnityEngine;
using System.Collections;

public class MapSeed : MonoBehaviour {

    public GameObject brick, hole, goal;
	public GameObject helper;

	protected GravityMap gm;
    private int width, height;

	private void GenerateStart(int Y) {
		for (int j = (gm.w/2)-2; j < (gm.w/2)+2; j++) {
			GameObject bb = Instantiate(brick, new Vector3(j - (-.5f + ((float)gm.w / 2f)), -Y, 1), Quaternion.identity) as GameObject;
			bb.transform.parent = this.transform;
		}
	}
	private void GenerateSolidRow(int Y) {
		for (int j = 0; j < gm.w; j++) {
			GameObject bb = Instantiate(brick, new Vector3(j - (-.5f + ((float)gm.w / 2f)), -Y, 1), Quaternion.identity) as GameObject;
			bb.transform.parent = this.transform;
		}
	}
	private void GenerateSides(int Y) {
			GameObject bl = Instantiate(brick, new Vector3(-1 - (-.5f + ((float)gm.w / 2f)), -Y, 1), Quaternion.identity) as GameObject;
			bl.transform.parent = this.transform;
		GameObject br = Instantiate(brick, new Vector3((float)gm.w - (-.5f + ((float)gm.w / 2f)), -Y, 1), Quaternion.identity) as GameObject;
			br.transform.parent = this.transform;
	}

	// Use this for initialization
	void Start () {
		height = GlobalVariables.Map_Height;
		width = GlobalVariables.Map_Width;
        gm = new GravityMap(width, height);
        gm.RandomSeed();

		bool spawn_holes = GlobalVariables.Spawn_Black_Holes;
		Debug.Log ("seed spawn hole " + spawn_holes);

		//FRAME
		GenerateSolidRow(-5);
		GenerateSides (-5);
		GenerateSides (-4);
		GenerateSides (-3);
		GenerateSides (-2);
		GenerateStart (-2);
		GenerateSides (-1);

        for(int i = 0; i < gm.h; i++)
        {
			GenerateSides (i);
            for (int j = 0; j < gm.w; j++)
            {
                switch( gm.nodes[i,j] )
                {
                    case GravityMap.GravityTransition.BLOCKED:
                        GameObject bb = Instantiate(brick, new Vector3(j - (-.5f + ((float)gm.w / 2f)), -i, 1), Quaternion.identity) as GameObject;
                        bb.transform.parent = this.transform;
                        break;
				case GravityMap.GravityTransition.HOLE:
						if (!spawn_holes)
							break;
                        GameObject hh = Instantiate(hole, new Vector3(j - (-.5f + ((float)gm.w / 2f)), -i, 1), Quaternion.identity) as GameObject;
                        hh.transform.parent = this.transform; break;
				case GravityMap.GravityTransition.GOAL:
					GameObject gg = Instantiate (goal, new Vector3 (j - (-.5f + ((float)gm.w / 2f)), -i, 1), Quaternion.identity) as GameObject;
					gg.transform.parent = this.transform;
					helper.GetComponent<DrawGoalDirectionLine> ().SetGoal (gg);
					break;
                default:
                    break;
                }
            }
        }
		GenerateSolidRow(gm.h);
		GenerateSides(gm.h);
	
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
