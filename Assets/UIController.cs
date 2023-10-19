using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClockController clockCont;
    //[SerializeField] private GameObject[] ;
    [SerializeField] private GameObject pauseIcon;
    [SerializeField] private GameObject playIcon;
    [SerializeField] private GameObject pannel;
    [SerializeField] private GameObject infoBoard;

    [SerializeField] private float rotateSpeed = 100f;
    private bool uiIsVisible = true;

    //private float timePaused = Time.unscaledTime;


    void Update() {
        if(uiIsVisible) {
            if(infoBoard.transform.localEulerAngles.y <= 0f) {
                infoBoard.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            } else {
                infoBoard.transform.localEulerAngles = infoBoard.transform.localEulerAngles - (rotateSpeed * Time.unscaledDeltaTime * new Vector3(0f, 1f, 0f));
            }
        } else {
            if(infoBoard.transform.localEulerAngles.y >= 90f) {
                infoBoard.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            } else {
                infoBoard.transform.localEulerAngles = infoBoard.transform.localEulerAngles + (rotateSpeed * Time.unscaledDeltaTime * new Vector3(0f, 1f, 0f));
            }
        }
    }

    public void HideUI() {
        uiIsVisible = false;
        //buttons.SetActive(false);
    }

    public void ShowUI() {
        uiIsVisible = true;
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
