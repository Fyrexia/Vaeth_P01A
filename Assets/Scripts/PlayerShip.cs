using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem ParticleShip1;
    public ParticleSystem ParticleForward1;
    public ParticleSystem ParticleForward2;
    public ParticleSystem ParticleBackward1;
    public ParticleSystem ParticleBackward2;

    [Header("Changables")]
    [SerializeField] public int count = 0;
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float turnSpeed = 3f;
    public bool Won = false;
    
    [Header("Visual Feedback")]
    [SerializeField] TrailRenderer trail = null;
    [SerializeField] TrailRenderer trail2 = null;
    [SerializeField] GameObject TeleportRef = null;
    [SerializeField] Text CoinText = null;
    [SerializeField] Text TeleportInstructions = null;

    [Header("Audio Feedback")]
    [SerializeField] AudioClip DeathExplosion = null;
    [SerializeField] AudioClip PowerUpBoost = null;
    [SerializeField] AudioClip PowerUpTeleporter = null;
    [SerializeField] AudioClip TeleportPickUp = null;
    [SerializeField] AudioClip Music = null;
    [SerializeField] AudioClip CoinsNoise = null;

    [Header("Rigid Body autofind")]
    public  Rigidbody rb = null;

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

        trail.enabled = false;
        trail2.enabled = false;

        TeleportInstructions.enabled = false;

        TeleportRef.SetActive(false);
        AudioHelper.PlayClip2D(Music, 0.1f);
        
        SetCountText();
    }

    
    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }




    //Checking Coin Collides and counts
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            AudioHelper.PlayClip2D(CoinsNoise, 0.1f);
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        CoinText.text = "Collect " + count.ToString() + " more coins to win! ";
        if (count <= 0)
        {
            CoinText.text = "Get to the Win Zone!";
        }
    }





    //Ship movement
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
    }


    void TurnShip()
    {
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * turnSpeed;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    //Kill the ship
    public void Kill()
    {
        Debug.Log("Player has been killed!");
        AudioHelper.PlayClip2D(DeathExplosion, 0.1f);
        this.gameObject.SetActive(false);
    }




    //SpeedPowers
    public void SetSpeed(float speedChange)
    {
        moveSpeed += speedChange;
    }

    public void SetBoosters(bool activeState)
    {
        AudioHelper.PlayClip2D(PowerUpBoost, 0.2f);
        trail.enabled = activeState;
    }



    //TeleporterPowers
    public void SetTeleportBoost(bool activeState)
    {
        trail2.enabled = activeState;
        AudioHelper.PlayClip2D(PowerUpTeleporter, 0.4f);
        DelayHelper.DelayAction(this, Trail2Off, .2f);
    }
    public bool teleportnoiseOn = false;
    public void SetTeleportReference(bool activeState)
    {
        if (teleportnoiseOn == false)
        {
            TeleportInstructions.enabled = true;
            AudioHelper.PlayClip2D(TeleportPickUp, 0.4f);
            teleportnoiseOn = true;
        }
        TeleportRef.SetActive(activeState);
    }

    public void Trail2Off()
    {
        TeleportInstructions.enabled = false;
        trail2.enabled = false;
        teleportnoiseOn = false;
    }

    public void TeleportPower()
    {
        rb.position += transform.forward * 11.0f;
    }






}
