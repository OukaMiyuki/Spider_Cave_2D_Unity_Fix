using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerController : MonoBehaviour {

    [SerializeField] float force = 500f;

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    IEnumerator AnimateBouncer() {
        animator.Play("Up");
        yield return new WaitForSeconds(.5f);
        animator.Play("Down");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerMoveController>().BouncePlayer(force);
            StartCoroutine(AnimateBouncer());
        }
    }
}
