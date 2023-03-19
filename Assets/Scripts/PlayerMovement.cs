using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _sprite;
    private BoxCollider2D _collider;
    private float _xDir;

    [SerializeField] private LayerMask _jumpableGround;
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _jumpForce = 14f;

    private enum MovementState { Idle, Running, Jumping, Falling }

    [SerializeField] private AudioSource _jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        _xDir = Input.GetAxisRaw("Horizontal");
        _rigidBody.velocity = new Vector2(_xDir * _moveSpeed, _rigidBody.velocity.y);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            _jumpSoundEffect.Play();
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }

        UpdateAnimationState();

        /*Debug.Log($"PLAYER Y: {rigidBody.transform.position.x}");*/
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (_xDir > 0f) // right
        {
            state = MovementState.Running;
            _sprite.flipX = false;
        }
        else if (_xDir < 0f) // left
        {
            state = MovementState.Running;
            _sprite.flipX = true;
        }
        else
        {
            state = MovementState.Idle;
        }

        if (_rigidBody.velocity.y > .1f)
        {
            state = MovementState.Jumping;
        }
        else if (_rigidBody.velocity.y < -.1f)
        {
            state = MovementState.Falling;
        }

        _animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
    }
}
