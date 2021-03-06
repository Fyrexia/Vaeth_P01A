﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HazardVolume : MonoBehaviour
{
    [SerializeField] Text YouLoseText = null;
    



    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();

        if (playerShip !=null)
        {
            YouLoseText.enabled = true;
            playerShip.Kill();
            DelayHelper.DelayAction(this, GameRestart, 2.0f);
        }



    }


    public void GameRestart()
    {
        GameInput.ReloadLevel();
    }




}
