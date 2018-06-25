using UnityEngine;
using System.Collections;

public class GravityLevels {

	public static Generation MapGeneration = new Generation();

	public class Generation {
		readonly public GameObject brick, hole, goal, coin;

		public static GravityMap GenerateSingleGoalMap(int w, int h) {
			GravityMap gm = new SingleGoalMap(w,h);
			return gm;
		}

		public Generation() {
			brick = Resources.Load ("Block") as GameObject;
			hole = Resources.Load ("BlackHole") as GameObject;
			goal = Resources.Load ("Goal") as GameObject;
			coin = Resources.Load ("Coin") as GameObject;
		}


	}

    public enum MapBlock
    {
        FREE,
        HOLE,
        BLOCKED,
        GOAL
    }

	public class SingleGoalMap : GravityLevels.GravityMap {

		public SingleGoalMap(int w, int h) : base(w,h) {}


		public override void LoadMap(MapLoader ml) {
			//FRAME
			ml.GenerateSolidRow(-5, this);
			ml.GenerateSides (-5, this);
			ml.GenerateSides (-4, this);
			ml.GenerateSides (-3, this);
			ml.GenerateSides (-2, this);
			ml.GenerateStart (-2, this);
			ml.GenerateSides (-1, this);

			int ccnt = 0;
			bool spawn_holes = GlobalVariables.Spawn_Black_Holes;

			for(int i = 0; i < h; i++)
			{
				ml.GenerateSides (i, this);
				for (int j = 0; j < w; j++)
				{
					switch( nodes[i,j] )
					{
					case GravityLevels.MapBlock.BLOCKED:
						ml.GenerateBlock (j,i, this, MapGeneration.brick);
//						GameObject bb = Instantiate(MapGeneration.brick, new Vector3(j - (-.5f + ((float)w / 2f)), -i, 1), Quaternion.identity) as GameObject;
//						bb.transform.parent = this.transform;
						break;
					case GravityLevels.MapBlock.HOLE:
						if (!spawn_holes)
							break;
						ml.GenerateBlock (j,i, this, MapGeneration.hole);
//						GameObject hh = Instantiate(MapGeneration.hole, new Vector3(j - (-.5f + ((float)w / 2f)), -i, 1), Quaternion.identity) as GameObject;
//						hh.transform.parent = this.transform;
						break;
					case GravityLevels.MapBlock.GOAL:
						ml.GenerateBlock (j,i, this, MapGeneration.goal);
//						GameObject gg = Instantiate (MapGeneration.goal, new Vector3 (j - (-.5f + ((float)w / 2f)), -i, 1), Quaternion.identity) as GameObject;
//						gg.transform.parent = this.transform;
						//helper.GetComponent<DrawGoalDirectionLine> ().SetGoal (gg);
						break;
					case GravityLevels.MapBlock.FREE:
						if ( GlobalVariables.variablesRx.mode.Value == Global.Enumerations.GameMode.SCAVENGER ) {
							ml.GenerateBlock (j,i, this, MapGeneration.coin);
//							GameObject cc = Instantiate (MapGeneration.coin, new Vector3 (j - (-.5f + ((float)w / 2f)), -i, 1), Quaternion.identity) as GameObject;
//							cc.transform.parent = this.transform;
							ccnt++;
						}
						break;
					default:
						break;
					}
				}
			}
			GlobalVariables.variablesRx.coins.SetValueAndForceNotify (ccnt);
			ml.GenerateSolidRow(h, this);
			ml.GenerateSides(h, this);
		}
		
	}

	public abstract class GravityMap {

	    public readonly int w, h;
	    public readonly MapBlock[,] nodes;

	    public GravityMap( int map_width, int map_height)
	    {
	        w = map_width;
	        h = map_height;
	        nodes = new MapBlock[map_height, map_width];
			RandomSeed();
	    }

		public abstract void LoadMap(MapLoader ml);

	    public MapBlock rand_trans()
	    {
			MapBlock r = (MapBlock)Random.Range(0, 3);
	        r = r == MapBlock.HOLE ? (Random.Range(0, 5) != 2 ? MapBlock.FREE : r) : r;
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

		private int RandRowIndex() {
			return Random.Range (0, w);
		}

	public void GeneratePath() {
		int start = RandRowIndex ();
		int goal = RandRowIndex ();
		int yt = start > goal ?  Random.Range(goal, start) : Random.Range(start, goal);
		//Debug.Log (": " + start + ", " + goal + " - " + yt);

		int x = start, y = 0;
		nodes [y, x] = MapBlock.FREE;
		while (y != h-1) {
			for (int xd = start; xd != goal;) {
				nodes [y, xd] = MapBlock.FREE;
				if (xd == yt) {
					y++;
					nodes [y, xd] = MapBlock.FREE;
				}
				xd += start > goal ? -1 : 1;
				if (xd == goal) {
					nodes [y, xd] = MapBlock.FREE;
					break;				
				}
			}
			start = goal;
			goal = RandRowIndex ();
			yt = start > goal ?  Random.Range(goal, start) : Random.Range(start, goal);
			//Debug.Log (": " + start + ", " + goal + " - " + yt);
		}

		//GOAL ZONE
		nodes [h-1, goal] = MapBlock.GOAL;
		nodes [h-2, goal] = MapBlock.FREE;
		if (goal - 1 >= 0) {
			nodes [h - 1, goal - 1] = MapBlock.FREE;
			nodes [h - 2, goal - 1] = MapBlock.FREE;
		} else {
			nodes [h-1, goal+1] = MapBlock.FREE;
			nodes [h-2, goal+1] = MapBlock.FREE;
		}
	}


	}

}
