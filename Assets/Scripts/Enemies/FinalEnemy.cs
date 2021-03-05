using Assets.Scripts.GameManager;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class FinalEnemy : MonoBehaviour
    {
        private ManagerOfScenes _sceneManager;

        private void Awake()
        {
            _sceneManager = GameObject.Find("GameManager").GetComponent<ManagerOfScenes>();
        }
        private void OnDestroy()
        {
            _sceneManager.GoToNextLevel();
        }

    }
}
