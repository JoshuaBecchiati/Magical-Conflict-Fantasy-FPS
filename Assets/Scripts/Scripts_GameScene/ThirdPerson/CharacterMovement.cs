using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // --- Constant ---
    private const float MIN_MOVE_SPEED = 0.1f;
    private const float WALK_VALUE = 0.5f;
    private const float RUN_VALUE = 1.0f;

    // --- Inspector ---
    [Header("Camera")]
    [SerializeField] private float m_cameraSpeed = 320f;
    [SerializeField] private Transform m_cameraPivot;

    [Header("Animation")]
    [SerializeField] private Animator m_animator;
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private float m_animationTransition;

    // --- Private ---
    private Vector3 _currentSpeed;
    private Vector3 _wantedSpeed;

    private float _horizontal;
    private float _vertical;
    private float _speedMagnitude;

    private bool _isRunning;
    private bool _isMoving;
    private bool _isDefending;

    #region Unity methods
    void Start()
    {
        _speedMagnitude = WALK_VALUE;

        if (PlayerInputSingleton.instance != null)
        {
            PlayerInputSingleton.instance.Actions["Move"].performed += OnMoveInput;
            PlayerInputSingleton.instance.Actions["Move"].canceled += OnMoveCanceled;
            PlayerInputSingleton.instance.Actions["Sprint"].started += OnSprintStarted;
            PlayerInputSingleton.instance.Actions["Sprint"].canceled += OnSprintCanceled;
        }
    }

    private void OnValidate()
    {
        if (!m_animator) m_animator = GetComponent<Animator>();
        if (!m_characterController) m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_isDefending) return;

        HandleMovement();
        ApplyFinalMovement();
    }
    #endregion

    #region Movement
    private void HandleMovement()
    {
        _speedMagnitude = _isRunning ? RUN_VALUE : WALK_VALUE;
        m_animator.SetBool("IsRunning", _isRunning);
        
        _wantedSpeed.z = _vertical * _speedMagnitude;
        _wantedSpeed.x = _horizontal * _speedMagnitude;

        if (Mathf.Abs(_horizontal) > MIN_MOVE_SPEED || Mathf.Abs(_vertical) > MIN_MOVE_SPEED)
            OrientCharToCamera();

        _currentSpeed = Vector3.MoveTowards(_currentSpeed, _wantedSpeed, m_animationTransition * Time.deltaTime);

        UpdateAnimator();
    }
    #endregion

    #region Apply movement
    private void ApplyFinalMovement()
    {
        m_characterController.Move(
            m_cameraPivot.TransformDirection(_currentSpeed) * Time.deltaTime
        );
    }
    #endregion

    #region Input
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        _isMoving = true;
        _horizontal = input.x;
        _vertical = input.y;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _isMoving = false;
        _horizontal = 0f;
        _vertical = 0f;
    }

    private void OnSprintStarted(InputAction.CallbackContext context)
    {
        _isRunning = true;
    }

    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        _isRunning = false;
    }
    #endregion

    #region Animator + rotation
    private void UpdateAnimator()
    {
        if (Mathf.Abs(m_animator.GetFloat("X")) < 0.005f &&
            Mathf.Abs(m_animator.GetFloat("Y")) < 0.005f &&
            !_isMoving)
        {
            _currentSpeed = Vector3.zero;
        }

        if (Mathf.Abs(m_animator.GetFloat("X")) > 1f && _isMoving)
        {
            _currentSpeed.x = 1f;
        }

        if (Mathf.Abs(m_animator.GetFloat("Y")) > 1f && _isMoving)
        {
            _currentSpeed.z = 1f;
        }
        m_animator.SetFloat("X", _currentSpeed.x);
        m_animator.SetFloat("Y", _currentSpeed.z);
    }

    private void OrientCharToCamera()
    {
        Vector3 lookDirection = m_cameraPivot.forward;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            m_cameraSpeed * Time.deltaTime
        );
    }
    #endregion

    public void SetDefending(bool state)
    {
        _isDefending = state;
    }
}
