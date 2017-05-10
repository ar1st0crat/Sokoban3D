using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoverController : MonoBehaviour
{
	private Animator animator;
	private CharacterController controller;
    private Rigidbody rigidBody;

    Vector3 _moveDirection;

	private int hashDead = Animator.StringToHash("Base Layer.Dead");
	private int hashPunch = Animator.StringToHash("Base Layer.Punch");

	void Start()
    {
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update()
    {
        float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		bool isMoving = (vertical != 0.0f || horizontal != 0.0f);

        if (isMoving)
        {
            animator.speed = 2;
            animator.SetFloat("Speed", 3);
        }
        else
        {
            animator.speed = 1;
            animator.SetFloat("Speed", 0);
        }
		
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.Play(hashDead);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            animator.Play(hashPunch);
        }
    }

	void OnAnimatorMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _moveDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _moveDirection = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _moveDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _moveDirection = Vector3.right;
        }

        var movement = _moveDirection * animator.GetFloat("Speed") * Time.deltaTime;

        rigidBody.position += movement;
        rigidBody.rotation = Quaternion.LookRotation(_moveDirection);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the mover collides with a wall (i.e. not with a box)
        if (collision.collider.GetComponent<BoxController>() == null)
        {
            return;
        }
        
        FindObjectOfType<GameController>().TryMovingBox(collision.collider, _moveDirection);
    }
}
