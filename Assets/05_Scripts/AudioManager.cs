using UnityEngine;
using UnityEngine.UI;

namespace audiomanager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource FXSource;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider FXVolumeSlider;

        [Header("FX Clips")]
        [SerializeField] private AudioClip keyboardClick;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (musicVolumeSlider != null && musicSource != null)
            {
                musicVolumeSlider.onValueChanged.AddListener(OnMusicSliderChanged);
                musicVolumeSlider.value = musicSource.volume;
            }
            if (FXVolumeSlider != null && FXSource != null)
            {
                FXVolumeSlider.onValueChanged.AddListener(OnFXSliderChanged);
                FXVolumeSlider.value = FXSource.volume;
            }
        }

        public void SetVolume(AudioSource source, float value)
        {
            if (source != null)
            {
                source.volume = value;                
            }            
        }
        public void PlayKeyboardClick()
        {
            if (keyboardClick == null || FXSource == null || !FXSource.isActiveAndEnabled || !FXSource.gameObject.activeInHierarchy)
                return;             
                FXSource.PlayOneShot(keyboardClick);
            
        }
        private void OnMusicSliderChanged(float value)
        {
            SetVolume(musicSource, value);
        }

        private void OnFXSliderChanged(float value)
        {
            SetVolume(FXSource, value);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
