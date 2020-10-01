using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalker : MonoBehaviour {

    [SerializeField] private float speed = 1f;
    [SerializeField] Transform startPosition, endPosition;
    private bool collision;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
        ChangeDirection();
    }

    private void Move() {
        rb.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }

    private void ChangeDirection() {
        collision = Physics2D.Linecast(startPosition.position, endPosition.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.DrawLine(startPosition.position, endPosition.position, Color.green);

        if (!collision) {
            Vector3 temp = transform.localScale;
            if (temp.x == 1f) {
                temp.x = -1f;
            } else {
                temp.x = 1f;
            }
            transform.localScale = temp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerDeath.instance.KillThePlayer();
            RunAnimation.instance.RunningAnimation();
            Invoke("LoadGameOverPanel", 1.0f);
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }
}
