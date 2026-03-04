using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class DirictionsCharacterGizmos : MonoBehaviour
{
    
    public Transform localTransform;
	public Transform cameraTransform;


	public float localDirectionDistance = 2f;
	public float camDirectionRelativeDistance = 2f;
	public float inputDirectionDistance = 2f;
	public float lineThick = 10f;

	private Vector3 inputDir;


	[Header("Refference")]
    public PlayerController playerController;

  


    private void OnDrawGizmos()
    {
		Vector3 startPos = transform.position;


#if UNITY_EDITOR
		Handles.color = Color.blue;
		Handles.DrawAAPolyLine(lineThick, startPos, startPos +  localTransform.forward * localDirectionDistance);

		Handles.color = Color.red;
		Handles.DrawAAPolyLine(lineThick, startPos, startPos + localTransform.right * camDirectionRelativeDistance);

		Handles.color = Color.yellow;
		Vector3 worldInputDir = new Vector3(playerController.MoveInput().x, 0f, playerController.MoveInput().y);
		Handles.DrawAAPolyLine(lineThick, startPos, startPos + worldInputDir * 3f);
#endif
	}

	public void OnMove(InputValue value)
	{
		inputDir = Vector2.ClampMagnitude(value.Get<Vector2>(), 1f);
	}
}
