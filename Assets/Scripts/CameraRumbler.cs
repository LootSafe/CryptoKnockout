using UnityEngine;

public class CameraRumbler : MonoBehaviour
{
    enum SHAKESTATE { SHAKING, NOTSHAKING };

    SHAKESTATE shakeState;
    Vector3 originalPosition;

    float timeShaking;
    float shakeDuration;
    float shakeIntensity;
    int intensityMultiplier;

    public bool debugging = false;

    /* Methods */

    void Start()
    {
        shakeState = SHAKESTATE.NOTSHAKING;

        /* Configurables */

        shakeDuration = 0.06f;
        shakeIntensity = 0.5f;
        timeShaking = 0.0f;
        intensityMultiplier = 1;
    }

    void Update()
    {
        /* Shake */

        if (timeShaking > 0)
        {
            Camera.main.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeDuration * (shakeIntensity * intensityMultiplier);

            timeShaking -= Time.deltaTime * 0.1f;
        }
        else if (timeShaking <= 0 && shakeState == SHAKESTATE.SHAKING)
        {
            shakeState = SHAKESTATE.NOTSHAKING;
            intensityMultiplier = 1;
        }

        /* Debug */

        if (debugging)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                standardAttack();
            }
        }
    }

    public void standardAttack()
    {
        if (shakeState == SHAKESTATE.SHAKING)
        {
            intensityMultiplier++;
            Camera.main.transform.localPosition = originalPosition;
        }
        else
        {
            originalPosition = Camera.main.transform.localPosition;
        }

        shakeState = SHAKESTATE.SHAKING;
        timeShaking = shakeDuration;
    }
}