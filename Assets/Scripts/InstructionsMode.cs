using UnityEngine;

public class InstructionsMode : MonoBehaviour
{
    public void BeginMode()
    {
        this.instructionsScreen.SetActive(true);
        isOn = true;
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

    bool isOn = false;

    void Update()
    {
        if (isOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopMode();
                playthroughMode.BeginMode();
            }
        }
    }
}
