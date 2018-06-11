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

        for (int i = 0; i < h-1; i++)
        {
            nodes[i, 0] = GravityTransition.BLOCKED;
            nodes[i, w-1] = GravityTransition.BLOCKED;
            for (int j = 1; j < w-1; j++)
            {
                nodes[i,j] = rand_trans();
            }
        }
        nodes[h-1, 0] = GravityTransition.BLOCKED;
        nodes[h-1, w - 1] = GravityTransition.BLOCKED;
        for (int j = 1; j < w - 1; j++)
        {
            nodes[h-1, j] = GravityTransition.GOAL;
        }
    }

}
