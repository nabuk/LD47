using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TMP_Text text = default;

    int seconds = 0;
    float elapsed = 0;
    bool measureElapsed = false;

    public void SetTime(int seconds)
    {
        this.seconds = seconds;
        elapsed = 0;
        UpdateText();
    }

    public void StartTime()
    {
        measureElapsed = true;
    }

    public void StopTimer()
    {
        measureElapsed = false;
    }

    void Update()
    {
        if (measureElapsed)
        {
            elapsed += Time.deltaTime;
            UpdateText();

            if (elapsed >= seconds)
            {
                StopTimer();

                //TODO: win condition
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
