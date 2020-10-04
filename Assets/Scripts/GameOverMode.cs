using UnityEngine;

public class GameOverMode : MonoBehaviour
{
    public void BeginMode(bool won)
    {
        if (won)
        {
            this.wonScreen.SetActive(true);
            continueLabelToActivateAfterCooldown = wonScreenContinueText;
        }
        else
        {
            this.lostScreen.SetActive(true);
            continueLabelToActivateAfterCooldown = lostScreenContinueText;
        }

        continueLabelToActivateAfterCooldown.SetActive(false);
        cooldownRemaining = 1.5f;
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
    GameObject wonScreenContinueText = default;

    [SerializeField]
    GameObject lostScreen = default;

    [SerializeField]
    GameObject lostScreenContinueText = default;

    [SerializeField]
    SfxPlayer sfxPlayer = default;

    bool isOn = false;
    float cooldownRemaining;
    GameObject continueLabelToActivateAfterCooldown;

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
                if (!continueLabelToActivateAfterCooldown.activeSelf)
                    continueLabelToActivateAfterCooldown.SetActive(true);

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
