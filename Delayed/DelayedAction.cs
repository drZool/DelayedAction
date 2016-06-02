using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DelayedAction : MonoBehaviour {
	
	static GameObject CreateGhost(string name){
		var go = new GameObject ("DelayedAction");
		GameObject.DontDestroyOnLoad (go);
		go.hideFlags = HideFlags.HideAndDontSave;
		return go;
	}

	static DelayedActionTime _time;
	public static DelayedActionTime time{
		get{ 
			if (_time == null) _time = CreateGhost("DelayedAction").AddComponent<DelayedActionTime> ();
			return _time;
		}
	}

	static DelayedActionUnscaledTime _unscaledTime;
	public static DelayedActionUnscaledTime unscaledTime{
		get{ 
			if (_unscaledTime == null) _unscaledTime = CreateGhost("DelayedActionUnscaledTime").AddComponent<DelayedActionUnscaledTime> ();
			return _unscaledTime;
		}
	}

	static DelayedActionRealtimeSinceStartup _realtimeSinceStartup;
	public static DelayedActionRealtimeSinceStartup realtimeSinceStartup{
		get{ 
			if (_realtimeSinceStartup == null) _realtimeSinceStartup = CreateGhost("DelayedActionRealtimeSinceStartup").AddComponent<DelayedActionRealtimeSinceStartup> ();
			return _realtimeSinceStartup;
		}
	}

	/// <summary>
	/// Creates the using custom time. You are responsible for this object! it does not go away by itself
	/// </summary>
	/// <returns>The using custom time.</returns>
	/// <param name="timeProvder">Time provder.</param>
	public static DelayedActionCustomTime CreateUsingCustomTime( Func <float> timeProvder){
		var component = CreateGhost ("DelayedActionCustomTime").AddComponent<DelayedActionCustomTime> ();
		component.timeProvider = timeProvder;
		return component;
	}

}

public struct TimedAction{
	public Action action;
	public float time;
	public int id;

	public TimedAction(float time, Action action, int id){
		this.time = time;
		this.action = action;
		this.id = id;
	}
}