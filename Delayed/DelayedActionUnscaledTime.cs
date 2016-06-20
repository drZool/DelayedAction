using UnityEngine;
using System.Collections;
using System;

public class DelayedActionUnscaledTime : DelayedActionBase {

	public float now{get{ return Time.unscaledTime; }}

	void Update () {
		while (sortedActions.Count > 0 && sortedActions [0].time < Time.unscaledTime) {
			var a = sortedActions [0];
			sortedActions.RemoveAt (0);
			a.action.Invoke ();
		}
	}

	public int Delay(float delay, Action action){
		return At (Time.unscaledTime + delay, action);
	}

	public override int At(float time, Action action){

		if (time <= Time.unscaledTime) { //Execute directly
			action.Invoke();
			uniqueId++; //Had to return a valid id even if not added to sortedActions 
			return uniqueId - 1;
		}

		return base.At (time, action);
	}

}