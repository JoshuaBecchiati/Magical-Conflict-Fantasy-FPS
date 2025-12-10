using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI volumeValueText;
    public AudioSource backgroundMusic; // <--- aggiunto

    void Start()
    {
        volumeSlider.value = 1f;
        UpdateVolumeText();
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolumeText(); });
    }

    void UpdateVolumeText()
    {
        int percent = Mathf.RoundToInt(volumeSlider.value * 100);
        volumeValueText.text = percent.ToString();

        // Controlla il volume della musica
        if (backgroundMusic != null)
            backgroundMusic.volume = volumeSlider.value;
    }

}

