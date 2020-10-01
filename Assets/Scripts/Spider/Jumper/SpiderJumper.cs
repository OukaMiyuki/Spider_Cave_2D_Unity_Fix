using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumper : MonoBehaviour {

    [SerializeField] private float forceY = 300f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start() {
        StartCoroutine(AttackThePayer());
    }

    IEnumerator AttackThePayer() {
        yield return new WaitForSeconds(Random.Range(2, 4));

        forceY = Random.Range(400f, 450f);

        rb.AddForce(new Vector2(0, forceY));
        animator.SetBool("Attack", true);

        yield return new WaitForSeconds(.7f);
        StartCoroutine(AttackThePayer());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ground" || collision.tag == "Lava") {
            animator.SetBool("Attack", false);
        }

        if (collision.tag == "Player") {
            PlayerDeath.instance.KillThePlayer();
            RunAnimation.instance.RunningAnimation();
            Invoke("LoadGameOverPanel", 1.0f);
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }
}
