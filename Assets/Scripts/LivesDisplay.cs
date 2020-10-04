using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    public void Show()
    {
        displayVisible = true;
    }

    public void Hide()
    {
        displayVisible = false;
    }

    [SerializeField]
    Image[] lives = default;

    [SerializeField]
    CrewCapsule crewCapsule = default;
    
    bool displayVisible;

    void Update()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            bool isVisible = crewCapsule.LivesLeft >= 3 - i;
            isVisible = isVisible && displayVisible;
            lives[i].enabled = isVisible;
        }
    }
}
