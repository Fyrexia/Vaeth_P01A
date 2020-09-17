using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerupTeleport : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float TeleportUses = 1;
    public float TeleportHolder = 0;

    [Header("Setup")]
    [SerializeField] GameObject visualsToDeactivate = null;

    Collider colliderToDeactivate = null;
    bool poweredUp = false;

    private PlayerShip playerShip = null;

    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();
        EnableObject();
    }



    private void OnTriggerEnter(Collider other)
    {
                playerShip
                = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null && poweredUp == false)
        {
            poweredUp = true;
            TeleportHolder += TeleportUses;
        }
    }


    

    private void Update()
    {
        if(playerShip != null && poweredUp==true && TeleportHolder>=1)
        {
            playerShip?.SetTeleportReference(true);
            DisableObject();
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerShip?.SetTeleportBoost(true);

                playerShip?.TeleportPower();

                TeleportHolder -= TeleportUses;
                playerShip?.SetTeleportReference(false);
                DelayHelper.DelayAction(this, EnableObject, 5.0f);
                poweredUp = false;

                

            }
        }
    }

    public void DeactivatePowerup(PlayerShip playerShip)
    {
        playerShip?.SetTeleportBoost(false);
    }

    public void DisableObject()
    {
        colliderToDeactivate.enabled = false;
        visualsToDeactivate.SetActive(false);
    }

    public void EnableObject()
    {
        colliderToDeactivate.enabled = true;
        visualsToDeactivate.SetActive(true);
    }



















}
