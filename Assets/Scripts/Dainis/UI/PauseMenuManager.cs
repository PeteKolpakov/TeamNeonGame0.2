using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

public class PauseMenuManager : MonoBehaviour
{
    public bool _isPauseMenuOpen = false;
    public GameObject PauseMenu;

    [FMODUnity.EventRef] public string _pauseEnterSFX;
    [FMODUnity.EventRef] public string _pauseExitSFX;


    public void ResumeGame(){
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        _isPauseMenuOpen = false;
    }
    public void QuitGame(){
        Application.Quit();
    }

    public void MainMenu(){
        ScenesManager sceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScenesManager>();
        sceneManager.GoToMainMenu();
        ResumeGame();
    }

    public void Update() {
         if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_isPauseMenuOpen == true)
                {
                    PlaySFX(_pauseExitSFX);
                    Time.timeScale = 1;
                    PauseMenu.SetActive(false);
                    _isPauseMenuOpen = false;
                }
                else
                {
                    PlaySFX(_pauseEnterSFX);
                    Time.timeScale = 0;
                    PauseMenu.SetActive(true);
                    _isPauseMenuOpen = true;
                }
            }
    }
    public void PlaySFX(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }
}
