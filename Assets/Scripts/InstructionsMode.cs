using UnityEngine;

public class InstructionsMode : MonoBehaviour
{
    public void BeginMode()
    {
        this.instructionsScreen.SetActive(true);
        isOn = true;
        sfxPlayer.AllowCollisionSounds = false;
    }

    public void StopMode()
    {
        this.instructionsScreen.SetActive(false);
        isOn = false;
    }

    [SerializeField]
    PlaythroughMode playthroughMode = default;

    [SerializeField]
    GameObject instructionsScreen = default;

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
