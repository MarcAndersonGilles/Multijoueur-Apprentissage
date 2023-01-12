using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class GestionAnimations : MonoBehaviourPun
{

    Animator animator;
    CharacterController controller;
    public static bool isShooting;
    bool isJumping = false;
    Vector3 Velocity;
    float velocityY; //vit. verticale
    float currentSpeed;
    float gravity  = -10;
    float jumpHeight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator=GetComponent<Animator>();
        controller=GetComponent<CharacterController>();
        Velocity=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }  
       
        if (!animator)
        {
            return;
        }
        //saut
        if (Input.GetKeyDown(KeyCode.Space)&&controller.isGrounded)
        {
            isJumping = true;
            float jumpVelocity = Mathf.Sqrt(-2*gravity*jumpHeight);
            velocityY=jumpVelocity;
        }
        else
        {
            isJumping=false;
        }
        //deplacement 
        float v = Input.GetAxis("Vertical");
        if(v<0)
        {
            v = 0;
        }
        //parametres animator
        animator.SetFloat("Speed", v);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isShooting", isShooting);

        //Physique
        velocityY += gravity * Time.deltaTime;
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.y).magnitude;
        Velocity = currentSpeed * transform.forward+velocityY*Vector3.up;
        controller.Move(Velocity * Time.deltaTime);



    }
}
