using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class AnimationsStateReceiver : MonoBehaviour
{
	[Header("Make sure you added 'AnimationsStateSender' \n in animator nodes")]
	[SerializeField] List<AniamtionState> animationEvents = new();

	public void OnStatEnter(string stateName)
	{
		AniamtionState matchingEvent = animationEvents.Find(se => se.stateName == stateName);
		matchingEvent?.OnStateEnter?.Invoke();
	}
	public void OnStatExit(string eventName)
	{
		AniamtionState matchingEvent = animationEvents.Find(se => se.stateName == eventName);
		matchingEvent?.OnStateExit?.Invoke();
	}

	public AniamtionState GetAnimationState(string stateName)
	{
		return animationEvents.Find(se => se.stateName == stateName);
	}
}
