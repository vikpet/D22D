using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	public float speed;
	public CharacterController controller;

	public AnimationClip run;
	public AnimationClip Idle;
	
	private Vector3 position;

	private Vector3 aimPosition;
	public static bool attack;
	
	private bool rotateMouse;


	// Use this for initialization
	void Start () {
	
		position = transform.position;

		rotateMouse = true;

		animation[run.name].speed = speed/3;

	}
	
	// Update is called once per frame
	void Update () 
	{

		if(!attack && !Input.GetKey (KeyCode.LeftShift))
		{
			if(Input.GetMouseButton(0))
			{
			//Locate position of click
				locatePosition();
			}
			moveToPosition();
		}
		if(attack | Input.GetKey (KeyCode.LeftShift))
		{
			rotateMouse = true;
			rotateToMouse ();
			//Reset position so we don't keep stuttering
			position = transform.position;
			moveToPosition();
		}
	}

	void locatePosition()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast (ray, out hit, 100))
		{
			if(hit.collider.tag!="Player"&&hit.collider.tag!="Enemy")
			{
			position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			}
		}

	}

	void moveToPosition()
	{
		if(Vector3.Distance(transform.position, position)>1)
		{
			Quaternion newRotation = Quaternion.LookRotation(position-transform.position);

			newRotation.x = 0f;
			newRotation.z = 0f;

			transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime + 10);

			controller.SimpleMove(transform.forward * speed);

			animation.CrossFade(run.name);
		}
		else
		{

			animation.CrossFade(Idle.name);
		}
	}

	void rotateToMouse()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast (ray, out hit, 100) &&rotateMouse)
		{
			if(hit.collider.tag!="Player")
			{

				if(hit.collider.tag=="Enemy")
				{
					aimPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
				}
				else
				{
					aimPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z-0.8f);
				}
				Debug.Log (position);

				Quaternion newRotation = Quaternion.LookRotation(aimPosition-transform.position);
				
				newRotation.x = 0f;
				newRotation.z = 0f;
				
				transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime + 10);
			}
		}

		rotateMouse = false;
	}
}
