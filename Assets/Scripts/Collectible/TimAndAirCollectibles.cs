using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimAndAirCollectibles : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (gameObject.name == "Air Collectibles") {
                GameObject.Find("Gameplay Controller").GetComponent<AirTimer>().air += 5;
            } else if(gameObject.tag == "TimeCollectibles") {
                GameObject.Find("Gameplay Controller").GetComponent<LevelTimer>().time += 10;
            }
            Destroy(gameObject);
        }
    }
}
