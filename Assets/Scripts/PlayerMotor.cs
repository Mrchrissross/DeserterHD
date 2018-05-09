using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Camera      cam;
    private GameObject  flashLight;
    private Rigidbody   rb;
    private Vector3     velocity = Vector3.zero,
                        rotation = Vector3.zero;
    private float       cameraRotationX = 0f,
                        currentCameraRotationX = 0f,
                        cameraRotationLimit = 85f;

    void Start()
    {
        cam = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        rb = GetComponent<Rigidbody>();
        flashLight = this.gameObject.transform.GetChild(3).gameObject;
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // Gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a rotational vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    // Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    //Perform rotation
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //Apply our rotation to the transform of our camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            flashLight.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

}