using UnityEngine;

public class PlaythroughMode : MonoBehaviour
{
    public void BeginMode()
    {
        timer.SetTime(1 * 5);
        timer.StartTime();
        crewCapsule.BeginPlaythrough();
        asteroidSpawner.StartSpawningForPlaythroughMode(gameTimeSec);
        gunsController.GunsActive = true;
        sfxPlayer.AllowCollisionSounds = true;
    }

    public void StopMode()
    {
        timer.StopTimer();
        timer.HideTimer();
        crewCapsule.StopPlaythrough();
        asteroidSpawner.StartSpawningForIdleMode();
        gunsController.GunsActive = false;
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
        gameOverMode.BeginMode(won: true);
    }

    void LostLife(CollisionObjectType obj)
    {
        // some effect ?
    }

    void Died()
    {
        StopMode();
        gameOverMode.BeginMode(won: false);
    }
}
