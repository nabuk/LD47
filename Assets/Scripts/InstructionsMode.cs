using UnityEngine;

public class InstructionsMode : MonoBehaviour
{
    public void BeginMode()
    {
        this.instructionsScreen.SetActive(true);
        isOn = true;
        sfxPlayer.AllowCollisionSounds = false;
        cooldownRemaining = 1.5f;
        continueText.SetActive(false);
        this.playthroughMode.HideHud();
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
    GameObject continueText = default;

    [SerializeField]
    SfxPlayer sfxPlayer = default;


    bool isOn = false;
    float cooldownRemaining;

    void Update()
    {
        if (isOn)
        {
            if (cooldownRemaining > 0)
            {
                cooldownRemaining -= Time.deltaTime;
            }
            else
            {
                if (!continueText.activeSelf)
                    continueText.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    sfxPlayer.PlaySelect();
                    StopMode();
                    playthroughMode.BeginMode();
                }
            }
        }
    }
}
