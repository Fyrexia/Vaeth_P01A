using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinVolume : MonoBehaviour
{
    [SerializeField] Text WinText= null;
    [SerializeField] AudioClip WinNoise = null;

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
            AudioHelper.PlayClip2D(WinNoise, .3f);
            playerShip.Won = true;
            WinText.enabled = true;
            playerShip.Kill();
            
        }



    }


   



}
