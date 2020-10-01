using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirTimer : MonoBehaviour {

    private Slider AirSlider;
    private GameObject player;
    private float airBurn = 1f;

    public float air = 10f;

    void Awake() {
        player = GameObject.Find("Player");
        AirSlider = GameObject.Find("Air Slider").GetComponent<Slider>();

        AirSlider.minValue = 0f;
        AirSlider.maxValue = air;
        AirSlider.value = AirSlider.maxValue;
    }

    void Update() {
        if (!player) {
            return;
        }

        if (air > 0) {
            air -= airBurn * Time.deltaTime;
            AirSlider.value = air;
        } else {
            if (PlayerDeath.instance.GetDieOrAlive()) {
                RunAnimation.instance.RunningAnimation();
                PlayerDeath.instance.KillThePlayer();
                print(PlayerDeath.instance.GetDieOrAlive());
                Invoke("LoadGameOverPanel", 1.0f);
            }
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }
}
