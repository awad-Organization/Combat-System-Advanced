using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AniamtionState
{
	public string stateName;
	public UnityEvent OnStateEnter;
	public UnityEvent OnStateExit;
}
