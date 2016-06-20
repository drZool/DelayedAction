using UnityEngine;
using System.Collections;
using System;

public class DelayedActionRealtimeSinceStartup : DelayedActionBase {

	public float now{get{ return Time.realtimeSinceStartup; }}

	void Update () {
		while (sortedActions.Count > 0 && sortedActions [0].time < Time.realtimeSinceStartup) {
			var a = sortedActions [0];
			sortedActions.RemoveAt (0);
			a.action.Invoke ();
		}
	}

	public int Delay(float delay, Action action){
		return At (Time.realtimeSinceStartup + delay, action);
	}

	public override int At(float time, Action action){

		if (time <= Time.realtimeSinceStartup) { //Execute directly
			action.Invoke();
			uniqueId++; //Had to return a valid id even if not added to sortedActions 
			return uniqueId - 1;
		}

		return base.At (time, action);
	}

}
