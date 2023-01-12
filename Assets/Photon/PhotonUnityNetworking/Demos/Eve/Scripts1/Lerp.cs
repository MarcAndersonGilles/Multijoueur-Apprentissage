using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{

    Vector3 pos1Camera;
    Vector3 pos2Camera;
    private float smoothSpeed = 2f;
    // Start is called before the first frame update

    private void LateUpdate()
    {
        pos1Camera = transform.position;  
        pos2Camera= new Vector3(2.6f,1.2f,9);
        transform.position=Vector3.Lerp(pos1Camera, pos2Camera, smoothSpeed*Time.deltaTime);
    }
}
