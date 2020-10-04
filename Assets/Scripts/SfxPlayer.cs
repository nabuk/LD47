using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public bool AllowCollisionSounds { get; set; } = true;

    public void PlayRockCollision(float volume)
    {
        if (AllowCollisionSounds)
            audioSource.PlayOneShot(rockCollisionSound, volume);
    }

    public void PlayLaserHit(float volume)
    {
        if (AllowCollisionSounds)
            audioSource.PlayOneShot(laserHitSound, volume);
    }

    public void PlayLaserShot()
    {
        audioSource.PlayOneShot(laserShotSound, 0.75f);
    }

    public void PlaySelect()
    {
        audioSource.PlayOneShot(selectSound);
    }

    [SerializeField]
    AudioClip selectSound = default;

    [SerializeField]
    AudioClip laserShotSound = default;

    [SerializeField]
    AudioClip laserHitSound = default;

    [SerializeField]
    AudioClip rockCollisionSound = default;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
