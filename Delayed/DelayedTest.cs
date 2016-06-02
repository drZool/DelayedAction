using UnityEngine;
using System.Collections;

public class DelayedTest : MonoBehaviour
{
	void Start ()
	{
		float now = Time.time;
		var t = DelayedAction.time;

		t.At (10f, () => LogTime ("G 10"));
		t.At (1f, () => LogTime ("C 1"));
		t.At (3f, () => LogTime ("F 3"));
		t.At (2f, () => LogTime  ("E 2"));
		t.At (1.5f, () => LogTime  ("D at time 115"));
		t.At (0.1f, () => LogTime  ("B 0.1"));
		int id = t.Delay (0.0f, () => LogTime  ("A unremovable 0"));
		t.Remove (id);
		id = t.Delay (0.0000001f, () => LogTime  ("A removable 0.0000001"));
		t.Remove (id);

		t.At ((int)Time.time + 1, SpawnDelay);

		var u = DelayedAction.unscaledTime;

//		u.At (1, () => Time.timeScale = 0f);
//		u.At (1, () => LogTime ("--------- Timescale at " + Time.timeScale));
//		u.At (2, () => LogTime ("Unscaled 2"));
//		u.At (5, () => Time.timeScale = 2f);
//		u.At (5, () => LogTime ("--------- Timescale at " + Time.timeScale));
//		u.At (6, () => LogTime ("Unscaled 6"));

		var r = DelayedAction.realtimeSinceStartup;
		r.At (2, () => LogTime("Realtime At 2"));
		r.At ( r.now + 1, () => LogTime("Realtime At >now< + 1"));

		var c = DelayedAction.CreateUsingCustomTime (GetMusicTime);
		c.Delay(1, () => LogTime("Custom time 1"));

		t.Delay (0.1f, () => Heavy(3));
	}

	void Heavy(int left){
		var t = DelayedAction.time;

		if (left <= 0) {
			t.At( t.lastTime , Done);
			return;
		}
		
		LogTime ("Heavy add random delays "+countMax );

		for (int i = 0; i < countMax; ++i) {
			t.Delay (Random.value * 5, Add);
		}
		t.Delay(2.5f, () => Heavy(left-1) );

	}

	void Done ()
	{
		LogTime ("Added " + count + "/" + countMax);
	}

	void Add(){
		count++;
	}

	int countMax = 1000;
	int count = 0;

	float GetMusicTime ()
	{
		return 0;
	}

	void LogTime (string message = null)
	{
		Debug.LogFormat ("{3} | time {0:0.00} unscaled {1:0.00} realtime {2:0.00}", Time.time, Time.unscaledTime, Time.realtimeSinceStartup, message);
	}

	bool on;

	void SpawnDelay ()
	{
		Debug.Log ("SpawnDelay " + Time.time);

		on = !on;

		if (on) {
			DelayedAction.time.At ((int)Time.time + 1, SpawnDelay);
		} else {
			DelayedAction.time.Delay (1, SpawnDelay);
		}
	}
}

