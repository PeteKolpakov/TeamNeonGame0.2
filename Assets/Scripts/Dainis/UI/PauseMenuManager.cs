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

    public void MainMenu()
    {
        ManagerOfScenes sceneManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerOfScenes>();
        sceneManager.GoToMainMenu();
        ResumeGame();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPauseMenuOpen == true)
            {
                PlaySFX(PauseExitSFX);
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                IsPauseMenuOpen = false;
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
