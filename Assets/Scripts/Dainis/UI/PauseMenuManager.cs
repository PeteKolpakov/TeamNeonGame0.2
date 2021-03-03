using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

public class PauseMenuManager : MonoBehaviour
{
    public bool _isPauseMenuOpen = false;
    public GameObject PauseMenu;

    public void ResumeGame(){
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        _isPauseMenuOpen = false;
    }
    public void QuitGame(){
        Application.Quit();
    }

    public void DebugMessage(){
        Debug.Log("Trying to click the button");
    }

    public void MainMenu(){
        ScenesManager sceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScenesManager>();
        sceneManager.GoToMainMenu();
        ResumeGame();
    }

    private void Update() {
         if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_isPauseMenuOpen == true)
                {
                    Time.timeScale = 1;
                    PauseMenu.SetActive(false);
                    _isPauseMenuOpen = false;
                }
                else
                {
                    Time.timeScale = 0;
                    PauseMenu.SetActive(true);
                    _isPauseMenuOpen = true;
                }
            }
    }
}
