using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 1f;

    void Update()
    {
        float alpha = Mathf.Abs(Mathf.Sin(Time.time * speed));
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }
}

