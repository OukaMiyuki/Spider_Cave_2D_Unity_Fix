using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallGround : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector2 initialPosition;
    bool platformMovingBack;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    void Update() {
        
        if (platformMovingBack) {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, 20f * Time.deltaTime);
        }

        if (transform.position.y == initialPosition.y) {
            platformMovingBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player" && !platformMovingBack) {
            Invoke("DropPlatform", .8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Lava" || collision.tag == "Ground") {
            gameObject.SetActive(false);
        }
    }

    private void DropPlatform() {
        rb.isKinematic = false;
        Invoke("GetPlatformBack", 3f);
    }

    private void GetPlatformBack() {
        gameObject.SetActive(true);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        platformMovingBack = true;

    }
}
