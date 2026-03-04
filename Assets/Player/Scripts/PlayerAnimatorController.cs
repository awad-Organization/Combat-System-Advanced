using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public string VelocityXParameter;
    public string VelocityYParameter;


    [Range(0.01f, 1f)]
    public float smoothDamp;

    [Header("Refference")]
	public Animator animator;
    public PlayerController playerController;
	public AnimatorController animatorControllerEditor;


	public AnimationData[] animationData;



	private void Start()
	{
		animatorControllerEditor = animator.runtimeAnimatorController as AnimatorController;

		foreach (var layer in animatorControllerEditor.layers)
		{
			foreach (var state in layer.stateMachine.states)
				Debug.Log(state.state.nameHash);
		}
	}

	private void Update()
    {
		animator.SetBool("HasMoveInput",playerController.HasMoveInput());
		animator.SetFloat("VelocityAbs", playerController.VelocityAbs());
		
		UpdateVelocity();
	}


	private void UpdateVelocity()
	{
		Vector3 velocityNormalize = playerController.CurrentLocalVelocity().normalized;

		animator.SetFloat(VelocityXParameter, velocityNormalize.normalized.x, smoothDamp, Time.deltaTime); // -1 , +1
		animator.SetFloat(VelocityYParameter, velocityNormalize.normalized.z, smoothDamp, Time.deltaTime); // -1 , +1
	}


	//Properties
	public AnimationClip GetCurrentAnimation()
	{
		return animator.GetCurrentAnimatorClipInfo(0)[0].clip;
	}

	//Utilits
	public AnimatorState GetAnimationState(int layerIndex, string stateName)
	{
		for(int i = 0; i < animatorControllerEditor.layers.Length; i++)
		{
			if (layerIndex != i)
				continue;

			var layer = animatorControllerEditor.layers[i];

			foreach (var state in layer.stateMachine.states)
				if (state.state.name == stateName)
					return state.state;

		}

		return null;
	}

	public List<AnimatorState> GetAllAnimationsState(int layerIndex)
	{
		var statesCatched = new List<AnimatorState>();

		for (int i = 0; i < animatorControllerEditor.layers.Length; i++)
		{
			if (layerIndex != i) continue;

			var state = animatorControllerEditor.layers[i].stateMachine.states[i].state;
			statesCatched.Add(state);
		}

		return null;
	}
}
// should be scriptable object + imporving adn expanded
[Serializable]
public class AnimationData 
{
	public string tageName;
	public bool isBlendTree { get { return isBlendTree; } set { isBlendTree = value; } }
	public bool IsSmoothDamp;
	[Range(0.01f, 1f)]
	public float smoothDamp;
	public float speedAnimation;

	public AnimationData(string tageName, float smoothDamp = 1f, float speedClip = 1f , bool IsSmoothDampp = false)
	{
		this.tageName = tageName;
		this.IsSmoothDamp = IsSmoothDampp;
		this.smoothDamp = smoothDamp;
		this.speedAnimation = speedClip;
	}
}
