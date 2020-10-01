using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {
    public static SceneFader instance;

    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Animator animator;
    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeIn(string levelName) {
        StartCoroutine(FadeInAnimation(levelName));
    }

    IEnumerator FadeInAnimation(string levelName) {
        fadeCanvas.SetActive(true);
        animator.Play("FadeIn");
        yield return new WaitForSeconds(.8f);
        SceneManager.LoadSceneAsync(levelName);
        FadeOut();
    }

    public void FadeOut() {
        StartCoroutine(FadeOutAnimation());
    }
    
    IEnumerator FadeOutAnimation() {
        animator.Play("FadeOut");
        yield return new WaitForSeconds(.9f);
        fadeCanvas.SetActive(false);
    }
}
