using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    Vector3 pos1Camera;
    Vector3 pos2Camera;
    float smoothTime = 2;
    private Vector3 Velocity = Vector3.zero;

    private void LateUpdate()
    {
        pos2Camera = new Vector3(5f,3f,19f);
        pos1Camera = transform.position;
        //deplacement camera
        transform.position = Vector3.SmoothDamp(pos1Camera, pos2Camera, ref Velocity,smoothTime);
        Debug.Log(Velocity.magnitude);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
