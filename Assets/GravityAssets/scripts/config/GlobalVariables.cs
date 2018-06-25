using UnityEngine;
using System.Collections;
using UniRx;

public class GlobalVariables {

	protected static bool spawn_black_holes = true, overview_camera = false, helper_line = false;
	protected static float gravity_momentum = .1f;
	protected static float gravity_min = .1f, gravity_cap = 10f;

	public static ViewModels.GamePreferences preferencesRx = new ViewModels.GamePreferences();
	public static ViewModels.GameVariables variablesRx = new ViewModels.GameVariables();
	public static ViewModels.GameLevelPreferences levelPrefsRx = new ViewModels.GameLevelPreferences(10,10);



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
namespace Global {
	namespace Enumerations {

		public enum GameMode {
			NOT_SET, LEVELS, ENDLESS, PUZZLE, SCAVENGER
		}

	}

}

namespace ViewModels {

	public class GameLevelPreferences {
		public ReactiveProperty<int> width { get; private set; }
		public ReactiveProperty<int> height { get; private set; }

		public GameLevelPreferences(int w, int h) {
			width = new ReactiveProperty<int> (w);
			height = new ReactiveProperty<int> (h);
		}
	}

	public class GamePreferences {
		public ReactiveProperty<bool> camera_overview { get; private set;}

		public GamePreferences() {
			camera_overview = new ReactiveProperty<bool> ( false );
		}
	}

	public class GameVariables {
		public ReactiveProperty<Global.Enumerations.GameMode> mode { get; private set;}
		public ReactiveProperty<int> coins { get; private set; }
		public ReactiveProperty<Vector3> player_screen_coordinates { get; private set;}

		public GameVariables() {
			mode = new ReactiveProperty<Global.Enumerations.GameMode> ( Global.Enumerations.GameMode.NOT_SET );
			coins = new ReactiveProperty<int> (0);
			player_screen_coordinates = new ReactiveProperty<Vector3> ( Vector3.zero );
		}
	}
}