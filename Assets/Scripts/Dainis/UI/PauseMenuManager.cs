using Assets.Scripts.GameManager;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public bool IsPauseMenuOpen = false;
    public GameObject PauseMenu;

    [FMODUnity.EventRef] public string PauseEnterSFX;
    [FMODUnity.EventRef] public string PauseExitSFX;


    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        IsPauseMenuOpen = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu(){
        ScenesManager sceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScenesManager>();
        sceneManager.GoToMainMenu();
        ResumeGame();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPauseMenuOpen == true)
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
            else
            {
                PlaySFX(PauseEnterSFX);
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                IsPauseMenuOpen = true;
            }
        }
    }
    public void PlaySFX(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }
}
