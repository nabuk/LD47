using UnityEngine;

public class PlaythroughMode : MonoBehaviour
{
    public void BeginMode()
    {
        timer.SetTime(1 * 60);
        timer.StartTime();
        crewCapsule.BeginPlaythrough();
        asteroidSpawner.StartSpawningForPlaythroughMode(gameTimeSec);
        gunsController.GunsActive = true;
    }

    [SerializeField]
    GunsController gunsController = default;

    [SerializeField]
    AsteroidSpawner asteroidSpawner = default;

    [SerializeField]
    CrewCapsule crewCapsule = default;

    [SerializeField]
    Timer timer = default;

    const int gameTimeSec = 1 * 60;

    void Awake()
    {
        crewCapsule.Died += Died;
        crewCapsule.LostLife += LostLife;
        timer.TimePassed += Won;
    }

    void Won()
    {
        // capsule should be invincible

        throw new System.NotImplementedException();
    }

    void LostLife(CollisionObjectType obj)
    {
        // TODO: some effect

        throw new System.NotImplementedException();
    }

    void Died()
    {
        // TODO: you lost message

        throw new System.NotImplementedException();
    }
}
