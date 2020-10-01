using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformWithTrigger : MonoBehaviour {
    [SerializeField] private float upLimit = 10f;
    [SerializeField] private float downLimitt = -3f;
    [SerializeField] private float acceleration = 1f; //used for increase the speed
    [SerializeField] private float movespeed = 0.5f;
    [SerializeField] private Vector3 userDirection = Vector3.up;
    [SerializeField] private Vector3 userDirectionDown = Vector3.down;
    [SerializeField] private float fallSpeed = 10f;
    [SerializeField] private bool enableElevatorUp = false;
    [SerializeField] private bool alreadyOnTop = false;
    [SerializeField] private bool enableElevatorDown = false;

    private void Start() {
        //This will wait 20 seconds to execute for the first time and later will be executed every 20 seconds
        if (enableElevatorUp) {
            InvokeRepeating("IncreaseSpeed", 0.5f, 0.5f);
        }
    }
    private void Update() {
        if (enableElevatorUp && !enableElevatorDown) {
            MoveUp();
        } else {
            if (alreadyOnTop) {
                MoveDown();
            }
        }
    }

    private void MoveUp() {
        if (transform.position.y > upLimit) {
            enableElevatorUp = false;
            enableElevatorDown = true;
            alreadyOnTop = true;
        } else {
            transform.Translate(userDirection * movespeed * Time.deltaTime);
        }
    }

    private void MoveDown() {
        if (alreadyOnTop && enableElevatorDown) {
            if (transform.position.y <= downLimitt) {
                movespeed = 0.5f;
                alreadyOnTop = false;
                enableElevatorDown = false;
            } else {
                transform.Translate(userDirectionDown * fallSpeed * Time.deltaTime);
            }
        }
    }

    private void IncreaseSpeed() {
        movespeed += acceleration;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        enableElevatorUp = true;
    }
}
