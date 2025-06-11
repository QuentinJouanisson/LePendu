using UnityEngine;
using UnityEngine.UI;

namespace audiomanager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private Slider musicVolumeSlider;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (musicVolumeSlider != null && musicSource != null)
            {
                musicVolumeSlider.onValueChanged.AddListener(SetVolume);
                musicVolumeSlider.value = musicSource.volume;
            }
        }

        public void SetVolume(float value)
        {
            if (musicSource != null)
            {
                musicSource.volume = value;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
