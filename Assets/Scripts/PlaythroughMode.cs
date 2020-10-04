using System.Collections;
using UnityEngine;

public class PlaythroughMode : MonoBehaviour
{
    public void BeginMode()
    {
        timer.SetTime(1 * 60);
        timer.StartTime();
        crewCapsule.BeginPlaythrough();
        livesDisplay.Show();
        asteroidSpawner.StartSpawningForPlaythroughMode(gameTimeSec);
        gunsController.GunsActive = true;
        sfxPlayer.AllowCollisionSounds = true;
    }

    public void StopMode()
    {
        timer.StopTimer();
        crewCapsule.StopPlaythrough();
        asteroidSpawner.StartSpawningForIdleMode();
        gunsController.GunsActive = false;
    }

    public void HideHud()
    {
        timer.HideTimer();
        livesDisplay.Hide();
    }

    [SerializeField]
    GameOverMode gameOverMode = default;

    [SerializeField]
    GunsController gunsController = default;

    [SerializeField]
    AsteroidSpawner asteroidSpawner = default;

    [SerializeField]
    CrewCapsule crewCapsule = default;

    [SerializeField]
    Timer timer = default;

    [SerializeField]
    LivesDisplay livesDisplay = default;

    [SerializeField]
    SfxPlayer sfxPlayer = default;

    const int gameTimeSec = 1 * 60;

    void Awake()
    {
        crewCapsule.Died += Died;
        crewCapsule.LostLife += LostLife;
        timer.TimePassed += Won;
    }

    void Won()
    {
        StopMode();
        StartCoroutine(WonCoroutine());
    }

    void LostLife(CollisionObjectType obj)
    {
        StartCoroutine(LostLifeSlowdownCoroutine());
    }

    void Died()
    {
        StopMode();
        StartCoroutine(DiedCoroutine());
    }

    IEnumerator WonCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        HideHud();
        gameOverMode.BeginMode(won: true);
    }

    IEnumerator DiedCoroutine()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(2f);
        HideHud();
        gameOverMode.BeginMode(won: false);
        Time.timeScale = 1f;
    }

    IEnumerator LostLifeSlowdownCoroutine()
    {
        const float slowdownTimeScale = 0.3f;
        const float slowdownDuration = 1f;
        float slowDownFor = 0f;
        float lastTime = Time.unscaledTime;

        Time.timeScale = slowdownTimeScale;

        yield return new WaitForSecondsRealtime(0.5f);

        while (slowDownFor < slowdownDuration)
        {
            var p = Mathf.Clamp(slowDownFor, 0, slowdownDuration) / slowdownDuration;
            Time.timeScale = slowdownTimeScale + (1f - slowdownTimeScale) * p;
            yield return new WaitForSecondsRealtime(0.05f);
            slowDownFor += Time.unscaledTime - lastTime;
            lastTime = Time.unscaledTime;
        }

        Time.timeScale = 1;
    }
}
