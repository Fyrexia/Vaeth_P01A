﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float turnSpeed = 3f;

    Rigidbody rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
