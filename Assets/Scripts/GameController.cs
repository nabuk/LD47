using UnityEngine;

public class GameController : MonoBehaviour
{
    InstructionsMode instructionsMode;

    void Awake()
    {
        instructionsMode = GetComponent<InstructionsMode>();
    }

    void Start()
    {
        instructionsMode.BeginMode();
    }
}
