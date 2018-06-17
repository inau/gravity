using UnityEngine;
using System.Collections;

public class GlobalVariables {

	protected static bool spawn_black_holes = true, overview_camera = false, helper_line = false;
	protected static float gravity_momentum = .1f;
	protected static float gravity_min = .1f, gravity_cap = 10f;
	protected static int map_w = 10, map_h = 5;

	public static bool Spawn_Black_Holes {
		get { 
			return spawn_black_holes;
		}
		set { 
			spawn_black_holes = value;
		}
	}

	public static bool Show_Helper_Line {
		get { 
			return helper_line;
		}
		set { 
			helper_line = value;
		}
	}


	public static bool Overview_Camera {
		get { 
			return overview_camera;
		}
		set { 
			overview_camera = value;
		}
	}

	public static int Map_Width {
		get { 
			return map_w;
		}
		set { 
			map_w = value;
		}
	}

	public static int Map_Height {
		get { 
			return map_h;
		}
		set { 
			map_h = value;
		}
	}

	public static float GravityMin {
		get { 
			return gravity_min;
		}
	}

	public static float GravityMax {
		get { 
			return gravity_cap;
		}
	}

	public static float GravityMomentum {
		get { 
			return gravity_momentum;
		}
		set { 
			gravity_momentum = value;
		}
	}
}
