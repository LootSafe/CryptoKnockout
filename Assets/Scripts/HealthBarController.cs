using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    public GameObject healthBar;
    public GameObject damageBar;
    public GameObject specialBar;
    public Text healthDisplay;
    public int playerNumber;

    private Game game;
    private float lastUpdatetime;
    private float lastUpdateHealth;

    enum HEALTHSTATE { ANIMATING, NOTANIMATING };
    enum SPECIALSTATE { ANIMATING, NOTANIMATING };

    Vector3 startPosition_healthbarImage;
    Vector3 startPosition_damageImage;
    Vector3 startPosition_specialbarImage;

    RectTransform healthbarRect;
    RectTransform damagebarRect;
    RectTransform specialbarRect;

    float animateSpeed = 2f;
    float damageBarPos = 0f;
    float xPos = 0f;
    float special = 0;

    HEALTHSTATE currentHealthstate;

    void Start()
    {
        
        game = Game.GetInstance();
        healthbarRect = healthBar.transform.GetComponent<RectTransform>();
        damagebarRect = damageBar.transform.GetComponent<RectTransform>();
        specialbarRect = specialBar.transform.GetComponent<RectTransform>();

        startPosition_healthbarImage = healthBar.transform.localPosition;
        startPosition_damageImage = damageBar.transform.localPosition;

        startPosition_specialbarImage = specialbarRect.GetChild(0).transform.GetComponent<RectTransform>().localPosition;

        print(startPosition_specialbarImage.x);

        currentHealthstate = HEALTHSTATE.NOTANIMATING;

        ResetBars();
    }

    void FixedUpdate()
    {
        
    }

    public void TakeDamageMeter(float damagePercentOutof100)
    {
        /* Health Bar Logic */

        float healthBarSize = healthbarRect.sizeDelta.x;
        float currentXPosition = healthbarRect.localPosition.x;

        xPos = healthBarSize / 100 * damagePercentOutof100;

        currentXPosition -= xPos;
        healthBar.transform.localPosition = new Vector2(currentXPosition, healthBar.transform.localPosition.y);

        /*  Damage Bar Logic */

        if (currentHealthstate == HEALTHSTATE.ANIMATING)
        {
            xPos += damagebarRect.sizeDelta.x;
        }
        else
        {
            currentHealthstate = HEALTHSTATE.ANIMATING;
        }

        damageBarPos = healthBar.transform.localPosition.x + startPosition_healthbarImage.x + (xPos / 2);
        damagebarRect.sizeDelta = new Vector2(xPos, damagebarRect.sizeDelta.y);
        damageBar.transform.localPosition = new Vector3(damageBarPos, damageBar.transform.localPosition.y);
    }

    public void IncreaseSpecialMeter(int fillPercentage)
    {
        if (special + fillPercentage <= 100)
        {
            special += fillPercentage;
            float increasePercentage = (specialbarRect.sizeDelta.x / 100 * special);
            specialbarRect.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta = new Vector3(increasePercentage, specialbarRect.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta.y);
            specialbarRect.GetChild(0).transform.localPosition = new Vector3(increasePercentage / 2, specialbarRect.GetChild(0).transform.localPosition.y);
        }
    }

    public void ClearSpecialMeter()
    {
        if (special != 0)
        {
            special = 0;
            specialbarRect.GetChild(0).transform.localPosition = new Vector3(startPosition_specialbarImage.x, specialbarRect.GetChild(0).transform.localPosition.y);
            specialbarRect.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, specialbarRect.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void ResetBars()
    {
        healthBar.transform.localPosition = startPosition_healthbarImage;
        damagebarRect.sizeDelta = new Vector2(0, damagebarRect.sizeDelta.y);
        ClearSpecialMeter();
    }

    public void Update()
    {
        //healthDisplay.text = "Player UNV";
        if (!game) healthDisplay.text  = "Game Was Not Initialized";
        Player player = game.GetPlayer(playerNumber);
        //Debug.Log("Trying to Get Player Number " + playerNumber);
        if (player)
        {
            //Current Health
            Debug.Log("Last Update " + lastUpdateHealth);
            if(player.GetHealth() != lastUpdateHealth)
            {
                float range = player.GetHealth() - lastUpdateHealth;
                //If health decreased
                    //Rudementary -- To Be replaced by damage calculator and "hit Streak variables"
                    TakeDamageMeter(range / player.GetMaxHealth());
                    Debug.Log("Range: " + range + " Calc - " + range / player.GetMaxHealth());
                    lastUpdateHealth = player.GetHealth();

            }
            
            Debug.Log(player.GetHealth());
            healthDisplay.text = "" + player.GetHealth();

        } else
        {
            healthDisplay.text = "Player Unavailable";
        }

        //Animate
        /* Animate Damage Bar */

        float nextWidth, difference;
        float currentWidth = damagebarRect.sizeDelta.x;

        if (currentWidth != 0)
        {
            if (Mathf.Abs(0 - currentWidth) < animateSpeed)
            {
                nextWidth = 0;
                currentHealthstate = HEALTHSTATE.NOTANIMATING;
            }
            else
            {
                nextWidth = currentWidth + animateSpeed * (Mathf.Abs(0 - currentWidth) / (0 - currentWidth));
                difference = currentWidth - nextWidth;
                difference = damageBar.transform.localPosition.x - (difference / 2);

                damageBar.transform.localPosition = new Vector3(difference, damageBar.transform.localPosition.y);
                damagebarRect.sizeDelta = new Vector2(nextWidth, damagebarRect.sizeDelta.y);
            }
        }
    }
   

    
}
