using UnityEngine;

public class HealthBarSystem : MonoBehaviour {

    HealthBarController playerone;
    HealthBarController playertwo;

    void Awake()
    {
        playerone = transform.GetChild(0).GetComponent<HealthBarController>();
        playertwo = transform.GetChild(1).GetComponent<HealthBarController>();
    }

    /* Reset the bars for the beginning of a round */

    public void roundBeginning_ResetHealthbars()
    {
        playerone.ResetBars();
        playertwo.ResetBars();
    }

    /* Player One */

    public void playerone_PercentageDamage(int damagePercentage)
    {
        playerone.TakeDamageMeter(damagePercentage);
    }

    public void playerone_IncreaseSpecialMeter(int addPercentageOfHundred)
    {
        playerone_IncreaseSpecialMeter(addPercentageOfHundred);
    }

    /* Player Two */

    public void playertwo_PercentageDamage(int damagePercentage)
    {
        playertwo.TakeDamageMeter(damagePercentage);
    }

    public void playertwo_IncreaseSpecialMeter(int addPercentageOfHundred)
    {
        playertwo_IncreaseSpecialMeter(addPercentageOfHundred);
    }

}
