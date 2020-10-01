using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.VR;

public class MovingGroundDown : MonoBehaviour {

    private bool onTop = true;
    private bool onBottom = false;
    private bool movingTheplatform = false;

    private void Update() {
        if (movingTheplatform) {
            if (onTop && !onBottom) {
                MovingDownThePlatform();
            } else if(!onTop && onBottom){
                MovingUpThePlatform();
            }
        }
    }

    private void MovingDownThePlatform() {
        if (transform.position.y <= -3.80f) {
            movingTheplatform = false;
            onTop = false;
            onBottom = true;

        }
        else {
            transform.Translate(Vector3.down * 10 * Time.deltaTime);
        }
    }

    private void MovingUpThePlatform() {
        if (transform.position.y >= 6.08f) {
            movingTheplatform = false;
            onTop = true;
            onBottom = false;
        } else {
            transform.Translate(Vector3.up * 10 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            movingTheplatform = true;
        }
    }
}
