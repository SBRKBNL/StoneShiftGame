using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SwipeDetection : MonoBehaviour
{
	public static SwipeDetection instance;
	public delegate void Swipe(Vector2 direction);
	public event Swipe swipePerformed;
	public TouchedObject touchedObject;
	

	
	[SerializeField] private InputAction position, press;

	[SerializeField] private float swipeResistance = 100;
	private Vector2 initialPos;
	private Vector2 currentPos => position.ReadValue<Vector2>();
	private void Awake()
	{
		position.Enable();
		press.Enable();
		press.performed += _ => { initialPos = currentPos; };
		press.canceled += _ => DetectSwipe();
		instance = this;
	}

	private void DetectSwipe()
	{
		
		Vector2 delta = currentPos - initialPos;
		Vector2 direction = Vector2.zero;
		//touchedObject.TouchObjectFunc(initialPos);
		

		DetectDirection(delta.x, delta.y);
		

		if (Mathf.Abs(delta.x) > swipeResistance)
		{
			direction.x = Mathf.Clamp(delta.x, -1, 1);
		}
		if (Mathf.Abs(delta.y) > swipeResistance)
		{
			direction.y = Mathf.Clamp(delta.y, -1, 1);
		}
		if (direction != Vector2.zero & swipePerformed != null)
			swipePerformed(direction);

	}

	private void DetectDirection(float x, float y)
	{

		if (x < 90 && x > -90)
		{
			if (y > 0)
				Debug.Log("Up Swipe");
				
			else

			{
				Debug.Log("Down Swipe");

				Debug.Log("y value: " + y + "\nx value: " + x);
			}
		}
		else
		{
			if (x > 0)
				Debug.Log("Right Swipe");
			else
				Debug.Log("Left Swipe");
		}




	}
}
