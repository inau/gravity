using UnityEngine;
using System.Collections;

public class MapSeed : MonoBehaviour {

    public GameObject brick, hole, goal;
    protected GravityMap gm;
    private int width, height;

	// Use this for initialization
	void Start () {
		height = GlobalVariables.Map_Height;
		width = GlobalVariables.Map_Width;
        gm = new GravityMap(width, height);
        gm.RandomSeed();

		bool spawn_holes = GlobalVariables.Spawn_Black_Holes;
		Debug.Log ("seed spawn hole " + spawn_holes);

        for(int i = 0; i < gm.h; i++)
        {
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
                        GameObject gg = Instantiate(goal, new Vector3(j - (-.5f + ((float)gm.w / 2f)), -i, 1), Quaternion.identity) as GameObject;
                        gg.transform.parent = this.transform; break;
                    default:
                        break;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
