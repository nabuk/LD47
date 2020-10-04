using UnityEngine;

public class GameOverMode : MonoBehaviour
{
    public void BeginMode(bool won)
    {
        if (won)
        {
            this.wonScreen.SetActive(true);
        }
        else
        {
            this.lostScreen.SetActive(true);
        }

        
        isOn = true;
        sfxPlayer.AllowCollisionSounds = false;
    }

    public void StopMode()
    {
        this.wonScreen.SetActive(false);
        this.lostScreen.SetActive(false);
        isOn = false;
    }

    [SerializeField]
    PlaythroughMode playthroughMode = default;

    [SerializeField]
    GameObject wonScreen = default;

    [SerializeField]
    GameObject lostScreen = default;

    [SerializeField]
    SfxPlayer sfxPlayer = default;

    bool isOn = false;

    void Update()
    {
        if (isOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                sfxPlayer.PlaySelect();
                StopMode();
                playthroughMode.BeginMode();
            }
        }
    }
}
