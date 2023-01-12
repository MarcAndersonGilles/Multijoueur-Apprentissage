using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCamera : MonoBehaviour
{
    private float distance = 1.5f;
    private float height = 1f;
    [SerializeField]
    private bool followOnStart = false;

    private float smoothSpeed = 2;
    Transform cameraTransform;
    bool isFollowing;
    Vector3 cameraoffset = Vector3.zero;
    float mouseSensitivity = 10;
    float yaw;
    float pitch;
    Vector3 CurrentRotation;
    Vector3 RotationSmoothVelocity;
    Vector2 PitchMinMax = new Vector2(10, 30);
    public float rotationSmoothTime = .12f;

    // Start is called before the first frame update
    void Start()
    {
        if (followOnStart)
            OnStartFollowing();

    }

    private void LateUpdate()
    {
        if (cameraTransform == null && isFollowing)
            OnStartFollowing();

        if (isFollowing)
            Follow();
        if (isFollowing)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, PitchMinMax.x, PitchMinMax.y);
            CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw, 0), ref RotationSmoothVelocity, rotationSmoothTime);
            cameraTransform.eulerAngles = CurrentRotation;
            transform.eulerAngles = Vector3.up * CurrentRotation.y;
            //Debug.Log(pitch);
        }

    }

    public void OnStartFollowing()
    {
        cameraTransform = Camera.main.transform;
        isFollowing = true;
        Cut();
    }

    void Follow()
    {
        cameraoffset.z = -distance;
        cameraoffset.y = height;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + transform.TransformVector(cameraoffset), smoothSpeed * Time.deltaTime);
        cameraTransform.LookAt(transform.position);
    }

    void Cut()
    {
        cameraoffset.z = -distance;
        cameraoffset.y = height;
        cameraTransform.position = transform.position + transform.TransformVector(cameraoffset);
    }

    // Update is called once per frame
    void Update()
    {

    }
}