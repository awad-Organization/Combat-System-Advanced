using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public float walkSpeed = 6f;
	public Camera cameraPlayer;
	public Animator animator;

	private Vector2 moveInput;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		// Camera forward/right
		Vector3 camForward = cameraPlayer.transform.forward;
		Vector3 camRight = cameraPlayer.transform.right;

		camForward.y = 0;
		camRight.y = 0;

		camForward.Normalize();
		camRight.Normalize();

		// Calculate movement direction
		Vector3 moveDirection = (camForward * moveInput.y) + (camRight * moveInput.x);

		// Apply velocity
		rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed,rb.linearVelocity.y,moveDirection.z * walkSpeed
		);

		
	}


	// Properties
	public Vector3 CurrentLocalVelocity()
	{
		return transform.InverseTransformDirection(rb.linearVelocity);
	}

	public Vector3 CurrentMoveDirection()
	{
		return rb.linearVelocity.normalized;
	}

	public Vector2 MoveInput()
	{
		return moveInput;
	}

	public bool HasMoveInput() // Controller Input
	{
		Debug.Log(moveInput.magnitude);
		return Mathf.Abs(moveInput.magnitude) > 0.1f;
	}

	public bool HasVelocity()
	{
		return rb.linearVelocity.magnitude > 0.1f;
	}

	public bool IsActuallyMoving()
	{
		return HasVelocity() && HasMoveInput();
	}
	public float VelocityAbs()
	{
		return Mathf.Abs(rb.linearVelocity.sqrMagnitude);
	}

	public void OnMove(InputValue value)
	{
		moveInput = value.Get<Vector2>();
	}

}