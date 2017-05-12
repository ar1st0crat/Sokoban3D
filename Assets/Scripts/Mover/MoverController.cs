using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MoverController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _controller;
    private Rigidbody _rigidBody;

    Vector3 _moveDirection;

    // some additional actions provided by a rabbit asset from Unity Store
    private int _hashDead = Animator.StringToHash("Base Layer.Dead");
    private int _hashPunch = Animator.StringToHash("Base Layer.Punch");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _rigidBody = GetComponent<Rigidbody>();
    }
	
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        bool isMoving = (vertical != 0.0f || horizontal != 0.0f);

        if (isMoving)
        {
            _animator.speed = 2;
            _animator.SetFloat("Speed", 3);
        }
        else
        {
            _animator.speed = 1;
            _animator.SetFloat("Speed", 0);
        }
		
        if (Input.GetKeyDown(KeyCode.G))
        {
            _animator.Play(_hashDead);
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            _animator.Play(_hashPunch);
        }
    }

    void OnAnimatorMove()
    {
        // if state == NotMoving

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

        var movement = _moveDirection * _animator.GetFloat("Speed") * Time.deltaTime;

        _rigidBody.position += movement;
        _rigidBody.rotation = Quaternion.LookRotation(_moveDirection);


        // else move rabbit to next cell
        // and then set state to not moving
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the mover collides with a wall (i.e. not with a box)
        if (collision.collider.GetComponent<Box>() == null)
        {
            return;
        }
        
        FindObjectOfType<GameController>().TryMovingBox(collision.collider, _moveDirection);
    }
}
