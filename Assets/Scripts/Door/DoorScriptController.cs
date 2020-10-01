using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScriptController : MonoBehaviour {

    public static DoorScriptController instance;

    private Animator animator;
    private BoxCollider2D box;

    public int collctiblesDiamondCount;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }

        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    IEnumerator OpenDoorFunction() {
        animator.Play("Open");
        yield return new WaitForSeconds(.7f);
        box.isTrigger = true;
    }

    public void DecrementCollectibleDiamonds() {
        collctiblesDiamondCount -= 1;

        if (collctiblesDiamondCount == 0) {
            StartCoroutine(OpenDoorFunction());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerWin.instance.Win();
        }
    }
}
