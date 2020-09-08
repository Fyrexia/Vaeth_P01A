using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] Text WinText= null;

    public void Awake()
    {
        WinText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip !=null && playerShip.count == 0)
        {
            playerShip.Won = true;
            WinText.enabled = true;
            playerShip.Kill();
            
        }



    }


   



}
