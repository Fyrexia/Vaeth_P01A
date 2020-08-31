using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
      ParticleSystem ParticleShip1;
    public ParticleSystem ParticleForward1;
    public ParticleSystem ParticleForward2;

    public ParticleSystem ParticleBackward1;
    public ParticleSystem ParticleBackward2;

    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float turnSpeed = 3f;
    
    Rigidbody rb = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ParticleForward1.Play();
            ParticleForward2.Play();

            ParticleBackward1.Stop();
            ParticleBackward2.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ParticleForward1.Stop();
            ParticleForward2.Stop();

            ParticleBackward1.Play();
            ParticleBackward2.Play();

        }

      


    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ParticleShip1 = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();

 


    }

    void MoveShip()
    {
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * moveSpeed;
        
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        rb.AddForce(moveDirection);

        if(moveAmountThisFrame<1 && moveAmountThisFrame>-1)
        {
            ParticleForward1.Stop();
            ParticleForward2.Stop();
        }

        /*if (moveAmountThisFrame > 1)
        {
            ParticleShip1.Play();
        }
        else if (moveAmountThisFrame < -1)
        {
            ParticleShip1.Stop();
        }*/





    }

    void TurnShip()
    {
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * turnSpeed;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    public void Kill()
    {
        Debug.Log("Player has been killed!");
        this.gameObject.SetActive(false);
    }

}
