using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action TimePassed = delegate { };

    public void SetTime(int seconds)
    {
        text.gameObject.SetActive(true);
        this.seconds = seconds;
        elapsed = 0;
        UpdateText();
    }

    public void StartTime()
    {
        text.gameObject.SetActive(true);
        measureElapsed = true;
    }

    public void StopTimer()
    {
        measureElapsed = false;
    }

    public void HideTimer()
    {
        text.gameObject.SetActive(false);
    }

    [SerializeField]
    TMP_Text text = default;

    int seconds = 0;
    float elapsed = 0;
    bool measureElapsed = false;

    void Update()
    {
        if (measureElapsed)
        {
            elapsed += Time.deltaTime;
            UpdateText();

            if (elapsed >= seconds)
            {
                StopTimer();
                TimePassed();
            }
        }
    }

    void UpdateText()
    {
        var totalSecondsLeft = Mathf.CeilToInt(seconds - elapsed);
        var s = totalSecondsLeft % 60;
        var m = totalSecondsLeft / 60;

        var strTime = $"{m}:{s:D2}";
        text.text = strTime;
    }
}
