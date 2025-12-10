using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Header("Weather Systems")]
    public GameObject rainSystem;
    public GameObject snowSystem;

    [Header("Timing Settings")]
    public float minChangeTime = 20f;
    public float maxChangeTime = 40f;

    private float timer;
    private float nextChangeTime;
    private bool isRaining;

    void Start()
    {
        // Inizia con pioggia o neve casuale
        isRaining = Random.value > 0.5f;
        UpdateWeather();

        // Imposta il tempo al prossimo cambio
        SetNextChangeTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextChangeTime)
        {
            // Cambia il meteo
            isRaining = !isRaining;
            UpdateWeather();

            // Reset timer
            timer = 0f;
            SetNextChangeTime();
        }
    }

    void UpdateWeather()
    {
        if (rainSystem != null) rainSystem.SetActive(isRaining);
        if (snowSystem != null) snowSystem.SetActive(!isRaining);
    }

    void SetNextChangeTime()
    {
        nextChangeTime = Random.Range(minChangeTime, maxChangeTime);
    }
}

