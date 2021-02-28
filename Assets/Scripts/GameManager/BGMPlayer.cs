using UnityEngine;

namespace Assets.Scripts.GameManager
{
    class BGMPlayer : MonoBehaviour
    {
        private static FMOD.Studio.EventInstance _music;

        [SerializeField][FMODUnity.EventRef] private string _track;
        [SerializeField][Range(0f, 1f)]      private float _defaultXFade = 0f;

        private void Start()
        {
            _music = FMODUnity.RuntimeManager.CreateInstance(_track);
            _music.setParameterByName("XFade", _defaultXFade);
            _music.start();
            _music.release();
        }


        private void Update()
        {
            // TODO remove this function
            SetBGMXFade(_defaultXFade);
        }

        private void OnDestroy()
        {
            _music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        public void SetBGMXFade(float value)
        {
            _music.setParameterByName("XFade", value);
        }

    }
}
