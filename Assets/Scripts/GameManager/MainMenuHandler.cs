using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuHandler : MonoBehaviour
{
    private Button _playButton;
    [SerializeField]
    private GameObject _speedrunMode;
    public GameObject PlayButtonGameObject;
    [SerializeField]
    private Animator _transition;
    private float _transitionDuration = 1f;
    private StatsTracker _stats;
    private TimerUI _timer;
    private bool _test;

    private void Start() {
        _playButton = PlayButtonGameObject.GetComponent<Button>();
        _stats = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatsTracker>();
        _timer = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimerUI>();
        Toggle speedrun = _speedrunMode.GetComponent<Toggle>();

        // This is fine :))))))
        speedrun.onValueChanged.AddListener(delegate{_stats.SpeedrunModeActivation(speedrun);});

        speedrun.isOn = false;
        _stats.CanStartTheTimer = true;

        _timer.StopAndResetTimer();
    }

    public void PlayButtonInteractableSwitch(){
        StartCoroutine(InteractableSwitch());
    }

    private IEnumerator InteractableSwitch(){
        _playButton.interactable = false;
        yield return new WaitForSeconds(1f);
        _playButton.interactable = true;

    }

    // We Ctrl+C / Ctrl+V this from the ScenesManager script.
    // GameManager, that ScenesManager script is atatched to, doesn't
    // get destroyed on load. But the rest of the buttons on scene - do.
    // So once we go to the main menu from the pause menu, we can't
    // start a new game, because GameManager gets de-referenced
    // on the button on-click event. I don't know how to cash it in
    // properly.

     public void GoToNextLevel()
        {
            //After Rest Stage
            StartCoroutine(LoadLevel(1));
        }

     public IEnumerator LoadLevel(int lvlIndex)
        {
            if(_transition != null){
                _transition.SetTrigger("Start");
            }

            yield return new WaitForSeconds(_transitionDuration);

            SceneManager.LoadScene(lvlIndex);
            if(_transition != null)
            {
                _transition.SetTrigger("End");
            }
        }        
}