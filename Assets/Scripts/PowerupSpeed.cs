using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpeed : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] float speedIncreaseAmount = 20;
    
    [SerializeField] float powerupAddtime = 5;

    [Header("Setup")]
    [SerializeField] GameObject visualsToDeactivate = null;

    Collider colliderToDeactivate = null;
    //bool poweredUp = false;
    float powerupDuration = 0;

    private void Awake()
    {
        colliderToDeactivate = GetComponent<Collider>();
        EnableObject();
    }



    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
                = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip != null)
        {
            StartCoroutine(PowerupSequence(playerShip));
        }
        
    }


    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        //poweredUp = true;

        ActivatePowerup(playerShip);
        powerupDuration += powerupAddtime;
        DisableObject();

        yield return new WaitForSeconds(powerupDuration);

        DeactivatePowerup(playerShip);
        EnableObject();

        //poweredUp = false;
        powerupDuration = 0;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if(playerShip != null)
        {
            playerShip.SetSpeed(speedIncreaseAmount);
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        playerShip?.SetSpeed(-speedIncreaseAmount);
        playerShip?.SetBoosters(false);
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
