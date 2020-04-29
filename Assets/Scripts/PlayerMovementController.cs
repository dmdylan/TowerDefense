using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private float walkSpeed = 2;
	[SerializeField] private float runSpeed = 6;
	[SerializeField] private float gravity = -12;
	[SerializeField] private float jumpHeight = 1;

	[Range(0, 1)]
	public float airControlPercent;

	[SerializeField] private float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	[SerializeField] private float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	//Animator animator;
	[SerializeField] Transform cameraT;
	CharacterController controller;

	private void Start()
	{
		//animator = GetComponent<Animator>();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.visible = !Cursor.visible;
			if (Cursor.lockState == CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.Confined;
			else
				Cursor.lockState = CursorLockMode.Locked;
		}

		// input
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey(KeyCode.LeftShift);

		Move(inputDir, running);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		// animator
		float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
		//animator.SetFloat("SpeedPercentage", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}

	void Move(Vector2 inputDir, bool running)
	{
		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;
		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);
		currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded)
		{
			velocityY = 0;
		}
	}

	void Jump()
	{
		if (controller.isGrounded)
		{
			float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	float GetModifiedSmoothTime(float smoothTime)
	{
		if (controller.isGrounded)
		{
			return smoothTime;
		}

		if (airControlPercent == 0)
		{
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
}
