using UnityEngine;
using System.Collections;

namespace running 
{
	public class PlayerMovement : MonoBehaviour
	{
		public Camera playerCamera;
		
		public float speed = 15.0F;
		public float jumpSpeed = 10.0F;
		public float gravity = 9.8F;
		private Vector3 moveDirection = Vector3.zero;

		private GameObject pina = null;

		void Awake()
		{
			pina = this.gameObject;

			GameObject jumpbutton = GameObject.Find(RunningConst.NGUIJumpButtonPath);
		}

		void Start()
		{
			if (playerCamera == null)
			{
				playerCamera = Camera.main;
			}

			playerCamera.transparencySortMode = TransparencySortMode.Orthographic;
		}
		
		void Update()
		{    
			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded)
			{
				moveDirection = new Vector3(1, 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton("Jump"))
				{
					moveDirection.y = jumpSpeed; 
				}
				if (Input.touchCount > 0)
				{
					moveDirection.y = jumpSpeed;
				}
				
			}
			moveDirection.y -= gravity * Time.smoothDeltaTime;
			controller.Move(moveDirection * Time.smoothDeltaTime);
			
			//After we move, adjust the camera to follow the player
			playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 10, playerCamera.transform.position.z);
		}

		void JumpButtonClick(GameObject button)
		{

			Debug.Log ("JumpButtonClick :: JumpButtonClick is called.");

			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded)
			{
				moveDirection = new Vector3(1, 1, 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;

				moveDirection.y = jumpSpeed; 
				
			}
			moveDirection.y -= gravity * Time.smoothDeltaTime;
			controller.Move(moveDirection * Time.smoothDeltaTime);

		}


	}
}