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
    AsteroidSpawner asteroidSpawner = default;

    [SerializeField]
    Timer timer = default;

    [SerializeField]
    GameObject pauseText = default;

    [SerializeField]
    TMP_Text instructionsText = default;

    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        var gameTimeSec = 1 * 60;
        timer.SetTime(1 * 60);
        timer.StartTime();
        asteroidSpawner.StartSpawning(gameTimeSec);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseToggle();
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    asteroidSpawner.SpawnSafe();
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
