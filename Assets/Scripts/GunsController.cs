using UnityEngine;

public class GunsController : MonoBehaviour
{
    float maxAngVelocity = 0.8f * 2f * Mathf.PI;
    float angAcc = 1f * 2f * Mathf.PI;
    float angDec = -2f * 2f * Mathf.PI;

    float cwVel = 0, ccwVel = 0;

    void Update()
    {
        var timeScale = Time.deltaTime;
        bool cw, ccw;
        
        cw = Input.GetKey(KeyCode.D);
        ccw = Input.GetKey(KeyCode.A);

        var cwVelDelta = cw ? angAcc * timeScale : angDec * timeScale;
        var ccwVelDelta = ccw ? angAcc * timeScale : angDec * timeScale;

        cwVel = Mathf.Clamp(cwVel + cwVelDelta, 0f, maxAngVelocity);
        ccwVel = Mathf.Clamp(ccwVel + ccwVelDelta, 0f, maxAngVelocity);

        var meanVelocity = -cwVel + ccwVel;
        var rotationDelta = meanVelocity * timeScale;

        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationDelta * Mathf.Rad2Deg);
    }
}
