using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {
    private Slider TimerSlider;
    private GameObject player;
    private float timeBurn = 1f;

    public float time = 10f;

    void Awake() {
        player = GameObject.Find("Player");
        TimerSlider = GameObject.Find("Time Slider").GetComponent<Slider>();

        TimerSlider.minValue = 0f;
        TimerSlider.maxValue = time;
        TimerSlider.value = TimerSlider.maxValue;
    }

    void Update() {
        if (!player) {
            return;
        }

        if (time > 0) {
            time -= timeBurn * Time.deltaTime;
            TimerSlider.value = time;
        } else {
            if (PlayerDeath.instance.GetDieOrAlive()) {
                RunAnimation.instance.RunningAnimation();
                PlayerDeath.instance.KillThePlayer();
                Invoke("LoadGameOverPanel", 1.0f);
            }
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }
}
