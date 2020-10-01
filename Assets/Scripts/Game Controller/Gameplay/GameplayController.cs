using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Text panelText;
    [SerializeField] private Button resumeRestartGameButton;
    [SerializeField] private Text resumeRestartTextButton;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseButton.interactable = false;
        panelText.fontSize = 47;
        panelText.text = "Paused";
        resumeRestartTextButton.text = "Resume";
        pausePanel.SetActive(true);
        resumeRestartGameButton.onClick.RemoveAllListeners();
        resumeRestartGameButton.onClick.AddListener(
            () => ResumeGame()
        );
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        pauseButton.interactable = true;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        //SceneManager.LoadScene("Level1");
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void PlayerDied() {
        Time.timeScale = 0f;
        pauseButton.interactable = false;
        panelText.fontSize = 47;
        panelText.text = "Game Over!";
        resumeRestartTextButton.text = "Restart";
        pausePanel.SetActive(true);
        resumeRestartGameButton.onClick.RemoveAllListeners();
        resumeRestartGameButton.onClick.AddListener(
            () => RestartGame()
        );
    }

    public void PlayerWin() {
        Time.timeScale = 0f;
        pauseButton.interactable = false;
        pausePanel.SetActive(true);
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int checkLastScene = PlayerPrefs.GetInt("levelUnlocked");

        if (checkLastScene == 2) {
            panelText.fontSize = 30;
            panelText.text = "You Have Finished The Game";
            resumeRestartTextButton.text = "Level Menu";
            resumeRestartGameButton.onClick.RemoveAllListeners();
            resumeRestartGameButton.onClick.AddListener(
                () => LoadLevelMenu()
            );
        }
        else {
            panelText.fontSize = 47;
            panelText.text = "Level Finished!";
            resumeRestartTextButton.text = "Next Level";
            if (currentLevel >= PlayerPrefs.GetInt("levelUnlocked")) {
                PlayerPrefs.SetInt("levelUnlocked", currentLevel);
            }
            Debug.Log("Level " + PlayerPrefs.GetInt("levelUnlocked") + "Unlocked");
            resumeRestartGameButton.onClick.RemoveAllListeners();
            resumeRestartGameButton.onClick.AddListener(
                () => LoadNextLevel()
            );
        }
    }

    private void LoadNextLevel() {
        Time.timeScale = 1f;
        print("Level Muat");
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        string levelName = "Level" + currentLevel;
        SceneFader.instance.FadeIn(levelName);
    }

    private void LoadLevelMenu() {
        Time.timeScale = 1f;
        SceneFader.instance.FadeIn("LevelMenu");
    }

    public void GoToMenu() {
        Time.timeScale = 1f;
        SceneFader.instance.FadeIn("MainMenu");
    }
}
