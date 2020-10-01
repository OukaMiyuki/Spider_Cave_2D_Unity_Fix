using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

    [SerializeField] private Button[] buttons;

    private int levelUnlocked;

    void Start() {
        levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);

        for (int i=0; i<buttons.Length; i++) {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelUnlocked; i++) {
            buttons[i].interactable = true;
        }
        print(PlayerPrefs.GetInt("levelUnlocked"));
    }

    public void LoadLevel(int levelIndex) {
        string levelName = "Level" + levelIndex;
        SceneFader.instance.FadeIn(levelName);
    }

    public void BackToMenuButton() {
        SceneFader.instance.FadeIn("MainMenu");
    }
}
