using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private SpriteRenderer _playerSprite;
    private FrameInputs _inputs;

    private void Update()
    {
        GatherInputs();
        HandleGrounding();
        HandleWalking();
    }
    
    #region Inputs

    private bool _facingLeft;

    private void GatherInputs() {
        _inputs.RawX = (int) Input.GetAxisRaw("Horizontal");
        _inputs.RawY = (int) Input.GetAxisRaw("Vertical");
        _inputs.X = Input.GetAxis("Horizontal");
        _inputs.Y = Input.GetAxis("Vertical");
        
        _facingLeft = _inputs.RawX != 1 && (_inputs.RawX == -1 || _facingLeft);
        SetFacingDirection(_facingLeft);
        //if (!_grabbing) SetFacingDirection(_facingLeft); // Don't turn while grabbing the wall
    }
    
    private void SetFacingDirection(bool left)
    {
        _playerSprite.flipX = left;
        //_anim.transform.rotation = left ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
    }

    #endregion

    #region Detection

    [Header("Detection")] [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _grounderOffset = -1, _grounderRadius = 0.2f;
    [SerializeField] private float _wallCheckOffset = 0.5f, _wallCheckRadius = 0.05f;
    private bool _isAgainstLeftWall, _isAgainstRightWall, _pushingLeftWall, _pushingRightWall;
    public bool IsGrounded;

    private readonly Collider[] _ground = new Collider[1];
    private readonly Collider[] _leftWall = new Collider[1];
    private readonly Collider[] _rightWall = new Collider[1];

    private void HandleGrounding()
    {
        // Grounded
        var grounded = Physics.OverlapSphereNonAlloc(transform.position + new Vector3(0, _grounderOffset),
            _grounderRadius, _ground, _groundMask) > 0;
        
        if (!IsGrounded && grounded)
        {
            Debug.Log("Im touching the ground");
            IsGrounded = true;
        }

        else if (IsGrounded && !grounded)
        {
            Debug.Log("I'm no longer touching the ground");
            IsGrounded = false;
        }
    }

    private void DrawGrounderGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _grounderOffset), _grounderRadius);
    }

    private void OnDrawGizmos() {
        DrawGrounderGizmos();
    }

    #endregion
    
    #region Walking

    [Header("Walking")] [SerializeField] private float _walkSpeed = 4;
    [SerializeField] private float _acceleration = 2;
    [SerializeField] private float _currentMovementLerpSpeed = 100;

    private void HandleWalking() {
        // Slowly release control after wall jump
        //_currentMovementLerpSpeed = Mathf.MoveTowards(_currentMovementLerpSpeed, 100, _wallJumpMovementLerp * Time.deltaTime);

        //if (_dashing) return;
        // This can be done using just X & Y input as they lerp to max values, but this gives greater control over velocity acceleration
        var acceleration = IsGrounded ? _acceleration : _acceleration * 0.5f;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (_rb.velocity.x > 0) _inputs.X = 0; // Immediate stop and turn. Just feels better
            _inputs.X = Mathf.MoveTowards(_inputs.X, -1, acceleration * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            if (_rb.velocity.x < 0) _inputs.X = 0;
            _inputs.X = Mathf.MoveTowards(_inputs.X, 1, acceleration * Time.deltaTime);
        }
        else {
            _inputs.X = Mathf.MoveTowards(_inputs.X, 0, acceleration * 2 * Time.deltaTime);
        }

        var idealVel = new Vector3(_inputs.X * _walkSpeed, _rb.velocity.y);
        // _currentMovementLerpSpeed should be set to something crazy high to be effectively instant. But slowed down after a wall jump and slowly released
        _rb.velocity = Vector3.MoveTowards(_rb.velocity, idealVel, _currentMovementLerpSpeed * Time.deltaTime);
    }

    #endregion
    
    private struct FrameInputs {
        public float X, Y;
        public int RawX, RawY;
    }
}