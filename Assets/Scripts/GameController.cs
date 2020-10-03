using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void PauseToggle()
    {
        pause = !pause;

        pauseText.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
    }

    [SerializeField]
    GameObject pauseText = default;

    [SerializeField]
    TMP_Text instructionsText = default;

    PlaythroughMode playthroughMode;

    bool pause = false;

    void Awake()
    {
        playthroughMode = GetComponent<PlaythroughMode>();
    }

    void Start()
    {
        playthroughMode.Start();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    PauseToggle();
        //}
    }

    IEnumerator ShowInstructionsLetterByLetter()
    {
        int length = instructionsText.text.Length;

        for (int i = 0; i < length; i++)
        {
            var c = instructionsText.text[i];
            instructionsText.maxVisibleCharacters = i;
            if (c != ' ')
                yield return new WaitForSeconds(0.15f);
            else
                yield return new WaitForSeconds(0.1f);
        }

        instructionsText.maxVisibleCharacters = length;
    }
}
