using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour {

    public static PlayerWin instance;
    private bool isPlayerWin = false;
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void Win() {
        isPlayerWin = true;
        RunAnimation.instance.RunningAnimation();
        Invoke("ShowWinScreen", 5.0f);
    }

    private void ShowWinScreen(){
        GameplayController.instance.PlayerWin();
    }

    public bool GetWin() {
        return isPlayerWin;
    }
}
