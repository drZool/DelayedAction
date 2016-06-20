
using UnityEngine;
using System.Collections;
using System;

public class DelayedActionCustomTime : DelayedActionBase {

	static float stub(){return 0;}
	public Func<float> timeProvider = stub; //Set this to something useful at runtime. 

	public float now{get{ return timeProvider(); }}

	void Update () {
		while (sortedActions.Count > 0 && sortedActions [0].time < timeProvider()) {
			var a = sortedActions [0];
			sortedActions.RemoveAt (0);
			a.action.Invoke ();
		}
	}

	public int Delay(float delay, Action action){
		return At (timeProvider() + delay, action);
	}

	public override int At(float time, Action action){

		if (time <= timeProvider()) { //Execute directly
			action.Invoke();
			uniqueId++; //Had to return a valid id even if not added to sortedActions 
			return uniqueId - 1;
		}

		return base.At (time, action);
	}
}

