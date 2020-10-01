using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {
    void Start() {
        var height = Camera.main.orthographicSize * 2;
        var width = height * Screen.width / Screen.height;

        if (gameObject.name == "BackgoundImage") {
            transform.localScale = new Vector3(width, height, 0); // scale the background based on the heigh and width
        }
    }

}
