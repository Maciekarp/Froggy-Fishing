using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClockController clockCont;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject pauseIcon;
    [SerializeField] private GameObject playIcon;
    [SerializeField] private GameObject pannel;
    
    //private float timePaused = Time.unscaledTime;

    public void HideUI() {
        buttons.SetActive(false);
    }

    public void StartClock() {
        clockCont.gameObject.SetActive(true);
        clockCont.timeLeft = 60;
    }

    public void Pause() {
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
            pannel.SetActive(true);
            Time.timeScale = 0f;
    }
    public void Play() {
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
            pannel.SetActive(false);
            Time.timeScale = 1f;
    }
    void OnApplicationFocus() {
        Play();
    }

    void OnApplicationPause() {
        Pause();
    }

    // toggles the game being paused 
    public void TogglePause() {
        if(Time.timeScale == 0f) {
            Play();
        } else {
            Pause();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    void Start() {
        Play();
    }

}
