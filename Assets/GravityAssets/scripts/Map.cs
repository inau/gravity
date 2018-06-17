using UnityEngine;
using System.Collections;

public class GravityMap {

    public enum GravityTransition
    {
        FREE,
        HOLE,
        BLOCKED,
        GOAL
    }

    public readonly int w, h;
    public readonly GravityTransition[,] nodes;

    public GravityMap( int map_width, int map_height)
    {
        w = map_width;
        h = map_height;
        nodes = new GravityTransition[map_height, map_width];
    }

    public GravityTransition rand_trans()
    {
        GravityTransition r = (GravityTransition)Random.Range(0, 3);
        r = r == GravityTransition.HOLE ? (Random.Range(0, 10) != 2 ? GravityTransition.FREE : r) : r;
        return r;
    }

    public void RandomSeed()
    {

        for (int i = 0; i < h; i++)
        {
           // nodes[i, 0] = GravityTransition.BLOCKED;
           // nodes[i, w-1] = GravityTransition.BLOCKED;
            for (int j = 0; j < w; j++)
            {
				nodes [i, j] = rand_trans();
            }
        }
		GeneratePath();
    }

	private int RowIndex() {
		return Random.Range (0, w);
	}

	public void GeneratePath() {
		int start = RowIndex ();
		int goal = RowIndex ();
		int yt = start > goal ?  Random.Range(goal, start) : Random.Range(start, goal);
		//Debug.Log (": " + start + ", " + goal + " - " + yt);

		int x = start, y = 0;
		nodes [y, x] = GravityTransition.FREE;
		while (y != h-1) {
			for (int xd = start; xd != goal;) {
				nodes [y, xd] = GravityTransition.FREE;
				if (xd == yt) {
					y++;
					nodes [y, xd] = GravityTransition.FREE;
				}
				xd += start > goal ? -1 : 1;
				if (xd == goal) {
					nodes [y, xd] = GravityTransition.FREE;
					break;				
				}
			}
			start = goal;
			goal = RowIndex ();
			yt = start > goal ?  Random.Range(goal, start) : Random.Range(start, goal);
			//Debug.Log (": " + start + ", " + goal + " - " + yt);
		}

		//GOAL ZONE
		nodes [h-1, goal] = GravityTransition.GOAL;
		nodes [h-2, goal] = GravityTransition.FREE;
		if (goal - 1 >= 0) {
			nodes [h - 1, goal - 1] = GravityTransition.FREE;
			nodes [h - 2, goal - 1] = GravityTransition.FREE;
		} else {
			nodes [h-1, goal+1] = GravityTransition.FREE;
			nodes [h-2, goal+1] = GravityTransition.FREE;
		}
	}

}
