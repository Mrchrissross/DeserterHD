using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Speed Settings")]
    [Tooltip("This is the speed that the player can run when not sprinting.")]
    public float        speed = 5f;
    [Tooltip("This is the players speed whilst sprinting.")]
    public float        sprintSpeed = 8f;
    [Tooltip("This is the players movement speed whilst crouching.")]
    public float        crouchSpeed = 1.5f;
    private float       normalSpeed;

    [Header("Look Settings")]
    [Tooltip("This is the speed at which you can look around.")]
    public float        lookSensitivity = 3f;

    [Header("Extra Settings")]
    [Tooltip("The height that the player can jump."), Range(1, 10)]
    public float        jumpHeight = 4.5f;
    [Tooltip("This is the speed that the player can climb up ladders."), Range(10, 12)]
    public float        climbSpeed = 11f;
    [Header("Texts")]
    [Tooltip("Press E to open door.")]
    public Text doorText;

    private bool        canUseDoor,
                        canUseUnlockableDoor,
                        isGrounded,
                        jump,
                        climbable,
                        climb,
                        sprinting = false,
                        movementEnabled = true;
    
    private GameObject  door,
                        flashLight;

    float               _xMov,
                        _zMov,
                        _yRot,
                        _xRot;

    private Animator    _animator;
    private PlayerMotor motor;
    static public bool  playerCanMove = true;
    static public bool  isCrouched = false;

    void Start()
    {        
        _animator =     GetComponent<Animator>();
        motor =         GetComponent<PlayerMotor>();
        flashLight =    this.gameObject.transform.GetChild(3).gameObject;
        normalSpeed =   speed;
    }

    void Update()
    {
        #region Movement
            // Used for developer to continue to move even after game has finished
            if (playerCanMove == false)
            {
                movementEnabled = false;
                playerCanMove = true;
            }
            if (Input.GetButtonDown("Jump") && Input.GetButtonDown("Climb"))
                movementEnabled = true;

            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (movementEnabled)
            {
                // Calculate movement velocity as a 3D vector
                _xMov = Input.GetAxis("Horizontal");
                _zMov = Input.GetAxis("Vertical");               

                //Calculate rotation as a 3D vector (turning around)
                _yRot = Input.GetAxisRaw("Mouse X");
                _xRot = Input.GetAxisRaw("Mouse Y");
            }
            else
            {
                _xMov = 0;
                _zMov = 0;
                _yRot = 0;
                _xRot = 0;
            }

        if (((!sprinting) && (isCrouched)) && ((_xMov > 0) || (_zMov > 0)))
        {
            speed = crouchSpeed;
            _animator.SetBool("CrouchedMoving", true);
        }
        else
            _animator.SetBool("CrouchedMoving", false);

        if (((_xMov > 0) || (_zMov > 0)) && ((!sprinting) && (!isCrouched)))
            _animator.SetBool("running", true);
        else
            _animator.SetBool("running", false);

            Vector3 _movHorizontal = transform.right * _xMov;
            Vector3 _movVertical = transform.forward * _zMov;

            // Final movement vector
            Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

            //Apply movement
            motor.Move(_velocity);

            Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

            //Apply rotation
            motor.Rotate(_rotation);
            
            float _cameraRotationX = _xRot * lookSensitivity;

            //Apply camera rotation
            motor.RotateCamera(_cameraRotationX);
        #endregion

        #region Jumping, Climbing, and Sprinting

            Vector3 direction = transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(transform.position, direction, 1.2f))
                isGrounded = true;
            else
                isGrounded = false;

            if ((isGrounded)&&(movementEnabled))
            {
                if (Input.GetButtonDown("Jump"))
                    jump = true;
            }

            if (climbable)
            {
                if (Input.GetButtonDown("Climb"))
                    climb = true;
                else if (Input.GetButtonUp("Climb"))
                    climb = false;
            }

            if ((Input.GetButtonDown("Sprint")) && ((_xMov > 0) || (_zMov > 0)))
            {
                sprinting = true;
                _animator.SetBool("sprinting", true);
                speed = sprintSpeed;              
            }
            else if ((Input.GetButtonUp("Sprint")) || ((_xMov == 0) && (_zMov == 0)))
            {
                sprinting = false;
                _animator.SetBool("sprinting", false);
                speed = normalSpeed;
            }
        #endregion

        #region Doors
        if ((canUseDoor) && (ItemManager.visibleItem == ""))
        {
            bool doorOpened = door.GetComponent<DoorScript>().open;

            if (doorOpened)
                doorText.text = "Press E to close";
            else if (!doorOpened)
                doorText.text = "Press E to open";

            if (Input.GetButtonDown("Use"))
            {
                if (!doorOpened)
                    door.GetComponent<DoorScript>().open = true;
                else if (doorOpened)
                    door.GetComponent<DoorScript>().open = false;
            }
        }
        else
            doorText.text = "";

        if ((canUseUnlockableDoor) && (ItemManager.visibleItem == ""))
        {
            bool doorOpened = door.GetComponent<LockedDoorScript>().open;

            if (doorOpened)
                doorText.text = "Press E to close";
            else if (!doorOpened)
                doorText.text = "Press E to open";

            if (Input.GetButtonDown("Use"))
            {
                if (!doorOpened)
                    door.GetComponent<LockedDoorScript>().open = true;
                else if (doorOpened)
                    door.GetComponent<LockedDoorScript>().open = false;
            }
        }
        #endregion

        #region FlashLight
        if (ItemManager.hasFlashLight == true)
            {
                if (Input.GetButtonDown("FlashLight"))
                {
                    if (flashLight.activeInHierarchy == true)
                        flashLight.SetActive(false);
                    else if (flashLight.activeInHierarchy == false)
                        flashLight.SetActive(true);
                }
            }
        #endregion

        #region Crouch
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            if(isCrouched == false)
            {
                isCrouched = true;
                _animator.SetBool("isCrouched", true);
            }
            else if (isCrouched == true)
            {
                speed = normalSpeed;
                isCrouched = false;
                _animator.SetBool("isCrouched", false);
            }
        }
        #endregion
    }

    void FixedUpdate()
    {
        if (jump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jump = false;
        }

        if (climb)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * climbSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "ClimbableWall":
                {
                    climbable = true;
                    break;
                }
            case "Door":
                {
                    door = other.gameObject;
                    canUseDoor = true;                   
                    break;
                }
            case "UnlockableDoor":
                {
                    door = other.gameObject;
                    canUseUnlockableDoor = true;
                    break;
                }
            case "Risk":
                {
                    GameController.instance.RiskTaken();
                    Destroy(other.gameObject);
                    break;
                }
            case "SpeechCollider":
                {
                    SpeechManager.instance.FindSpeech(other.gameObject.name);
                    break;
                }
            default:
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {     
        switch (other.tag)
        {
            case "ClimbableWall":
                {
                    climbable = false;
                    climb = false;
                    break;
                }
            case "Door":
                {
                    canUseDoor = false;
                    doorText.text = null;
                    break;
                }
            case "UnlockableDoor":
                {
                    canUseUnlockableDoor = false;
                    doorText.text = null;
                    break;
                }
            case "SpeechCollider":
                {
                    SpeechManager.instance.FindSpeech("");
                    break;
                }
            default:
                break;
        }
    }
}