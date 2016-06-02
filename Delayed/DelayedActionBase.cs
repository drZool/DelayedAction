using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DelayedActionBase : MonoBehaviour
{
	internal int uniqueId = int.MinValue;
	internal List<TimedAction> sortedActions = new List<TimedAction>(32);
	internal float lastTime { get; private set;}
	
	public virtual int At(float time, Action action){
		
		if (time < lastTime) { //Optimization, lastTime can be removed
			for (int i = 0; i < sortedActions.Count; ++i) {
				if (sortedActions [i].time > time) {
					sortedActions.Insert (i, new TimedAction (time, action, uniqueId++));
					return uniqueId - 1;
				}
			}
		}
		lastTime = time;
		sortedActions.Add(new TimedAction (time, action, uniqueId++));
		return uniqueId - 1;
	}

	public void Remove(int id){		
		for (int i = 0; i < sortedActions.Count; ++i) {
			if (sortedActions [i].id == id) {
				sortedActions.RemoveAt (i);
				return;
			}
		}
	}

}

