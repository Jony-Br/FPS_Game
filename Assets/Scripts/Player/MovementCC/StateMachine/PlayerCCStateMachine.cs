using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCCStateMachine : MonoBehaviour
{
   

    [SerializeField] private CharacterController _controller;
    public CharacterController Controller { get { return _controller; } set { _controller = value; } }

   

  
    [Header("Gravity")]
    [SerializeField] private float _gravity = -9.81f;
    public float Gravity { get { return _gravity; } set { _gravity = value; } }
    /// �������� ������������ ������� private 
    [SerializeField] public Vector3 _velocity;
    //public Vector3 Velocity { get { return _velocity; } set { _velocity = value; } }

    [SerializeField] private Transform _groundCheck;
    public Transform GroundCheck { get { return _groundCheck; } set { _groundCheck = value; } }

    [SerializeField] private float _groundDistance = 0.4f;
    public float GroundDistance { get { return _groundDistance; } set { _groundDistance = value; } }
    [SerializeField] private LayerMask _groundMask;
    public LayerMask GroundMask { get { return _groundMask; } set { _groundMask = value; } }
    [SerializeField] private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded; } set { _isGrounded= value; } }

    [SerializeField] private float _jumpHeight = 3f;
    public float JumpHeight { get { return _jumpHeight; }set { _jumpHeight= value; } }

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    public Animator CCAnimator { get { return _animator; } set { _animator = value; } }

    [SerializeField] public int _isWalkingHash;
    public int IsWalkingHash { get { return _isWalkingHash; } set { _isWalkingHash = value; } }

    [SerializeField] public int _isRunningHash;
    public int IsRunningHash { get { return _isRunningHash; } set { _isRunningHash = value; } }

    [SerializeField] public int _isJumpingHash;
    public int IsJumpingHash { get { return _isJumpingHash; } set { _isJumpingHash= value; } }

    [SerializeField] public int _isCrouchingHash;
    public int IsCrouchingHash { get { return _isCrouchingHash; } set { _isCrouchingHash = value; } }


    [Header("Input")]

    [SerializeField] private float _speed ;
    public float Speed { get { return _speed; } set { _speed = value; } }

    [SerializeField] private float _horizontalInput;
    public float HorizontalInput { get { return _horizontalInput; } set { _horizontalInput = value; } }
    [SerializeField] private float _verticalInput;
    public float VericalInput { get { return _verticalInput; } set { _verticalInput = value; } }

    [SerializeField] private bool _isMovementPressed = false;
    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }

    [SerializeField] private bool _isRunPressed = false;
    public bool IsRunPressed { get { return _isRunPressed; } set { _isRunPressed = value; } }

    [SerializeField] private bool _isJumpPressed = false;
    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }

    [SerializeField] private bool _isCrouchPressed = false;
    public bool IsCrouchPressed { get { return _isCrouchPressed; } set { _isCrouchPressed = value; } }


    public Vector3 move;

    

    private bool _requireNewJumpPress = true;
    public bool RequireNewJumpPress { get { return _requireNewJumpPress; } set { _requireNewJumpPress = value; } }

    [Header("UI Elements")]
    [SerializeField] private Joystick _joystick;
    public Joystick Joystick { get { return _joystick; } set { _joystick = value; } }

    [SerializeField] private Jump _jumpingButton;
    public Jump JumpingButton { get { return _jumpingButton; } set { _jumpingButton = value; } }

    [SerializeField] private Crouch _crouchButton;
    public Crouch CrouchButton { get { return _crouchButton; } set { _crouchButton = value; } }

    


    


    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }


    private string _verticalPatameterName = "Vertical";
    private string _horizontalPatameterName = "Horizontal";
    public string VerticalPatameterName { get { return _verticalPatameterName; } set { _verticalPatameterName = value; } }
    public string HorizontalPatameterName { get { return _horizontalPatameterName; } set { _horizontalPatameterName = value; } }

    [Header("Camera")]
    [SerializeField] private Camera _mainCamera;
    public Camera CameraMainCamera { get { return _mainCamera;} set { _mainCamera = value; } }
    public Transform _LerpCameraFrom;
    public Transform _LerpCameraTo;

    PlayerBaseState _currentState;
    PlayerStateFactory _states;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        IsWalkingHash = Animator.StringToHash("IsWalking");
        IsRunningHash = Animator.StringToHash("IsRunning");
        IsJumpingHash = Animator.StringToHash("IsJumping");
        IsCrouchingHash = Animator.StringToHash("IsCrouching");

        _jumpingButton.OnJumpButtonPressed += OnJumpClick;
        _jumpingButton.OnJumpButtonReleased += OnJumpReleased;

        _crouchButton.OnCrouchButtonPressed += OnCrouchClick;
       //_crouchButton.OnCrouchButtonReleased += OnCrouchReleased;



        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnInputHandler();
        //GravityHandler();
        _currentState.UpdateStates();
        //_controller.Move(move* _speed * Time.deltaTime);

    }

    private void OnInputHandler()
    {

        /////////////////////////Walk

        _horizontalInput = Input.GetAxis(HorizontalPatameterName);
        _verticalInput = Input.GetAxis(VerticalPatameterName);

        /* _horizontalInput = _joystick.Direction.x;
         _verticalInput = _joystick.Direction.y;*/

        //Debug.Log(_joystick.Direction);

        _isMovementPressed = _horizontalInput != 0 || _verticalInput != 0;
        if (!_isMovementPressed)
        {
            CCAnimator.SetFloat(_horizontalPatameterName, _horizontalInput);
            CCAnimator.SetFloat(_verticalPatameterName, _verticalInput);
        }

        ///////////////////////Run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //_isMovementPressent = false;
            _isRunPressed = true;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //_isMovementPressent = true;
            _isRunPressed = false;
        }

        /*if ((_joystick.Direction.x >= 0.8 && _joystick.Direction.y >=0.8) || (_joystick.Direction.x <= -0.8 && _joystick.Direction.y <= -0.8))
        {
            //_isMovementPressent = false;
            _isRunPressed = true;

        }
        else 
        {
            //_isMovementPressent = true;
            _isRunPressed = false;
        }*/


        //////////////////////// jump
        if (Input.GetKeyUp(KeyCode.Space))
        {

            _isJumpPressed = true;
            _requireNewJumpPress = false;
        }
        else
        {
            _isJumpPressed = false;
            _requireNewJumpPress = true;
        }
        /*if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            _isJumpPressed = true;
            _requireNewJumpPress = false;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumpPressed = false;
            _requireNewJumpPress = true;
        }*/

        //////////////////////Crouch 

        if (Input.GetKeyUp(KeyCode.C))
        {

            if (_isCrouchPressed)
            {
                _isCrouchPressed = false;
            }
            else
            {
                _isCrouchPressed = true;
            }
        }

    }
    private void OnJumpClick()
    {
        _isJumpPressed = true;
        _requireNewJumpPress = false;
    }

    private void OnJumpReleased()
    {
        _isJumpPressed = false;
        //_requireNewJumpPress = true;
    }

    private void OnCrouchClick()
    {
        if (_isCrouchPressed)
        {
            _isCrouchPressed = false;
        }
        else 
        {
            _isCrouchPressed = true;
        }
    }

  /*  private void OnCrouchReleased()
    {
        // _isCrouchPressed = false;
    }*/

}
