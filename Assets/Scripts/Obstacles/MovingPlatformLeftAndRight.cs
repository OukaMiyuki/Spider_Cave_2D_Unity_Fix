using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformLeftAndRight : MonoBehaviour {

    private bool onLeft = true;
    private bool onRight = false;
    private bool movingTheplatform = false;

    private void Update() {
        if (movingTheplatform) {
            if (onLeft && !onRight) {
                MovePlatformToRight();
            } else if (!onLeft && onRight) {
                MovePlatformToLeft();
            }
        }
    }

    private void MovePlatformToRight() {
        if (transform.position.x >= -2.19f) {
            movingTheplatform = false;
            onLeft = false;
            onRight = true;
        } else {
            transform.Translate(Vector3.right * 5 * Time.deltaTime);
        }
    }

    private void MovePlatformToLeft() {
        if (transform.position.x <= -14.18f) {
            movingTheplatform = false;
            onLeft = true;
            onRight = false;
        } else {
            transform.Translate(Vector3.left * 5 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            movingTheplatform = true;
        }
    }
}
