using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject pauseText = default;

    [SerializeField]
    TMP_Text instructionsText = default;

    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowInstructionsLetterByLetter());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseToggle();
        }
    }

    public void PauseToggle()
    {
        pause = !pause;

        pauseText.SetActive(pause);
        Time.timeScale = pause ? 0f : 1f;
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
