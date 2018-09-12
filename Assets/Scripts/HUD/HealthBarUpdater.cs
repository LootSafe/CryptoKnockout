using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour {

    
    public float hueIntensity = 0.7f;
    public int playerNumber = 0;

    public RectTransform healthShowBar;
    public float healthAnimateSpeed;
    Color originalHealthColor;
    public RectTransform healthFillContainer;

    public RectTransform damageIndicator;
    public float damageAnimateSpeed;
    public float damageBarDelay = 0.1f;

    public RectTransform specialBar;
    public float specialAnimateSpeed;
    public RectTransform specialContainer;
    public GameObject specialParticles;

    public GameObject damageBarParticles;

    public Image portrait;
    public Text characterName;
    public Text healthTextDisplay;

    Game game;
    Player player;
    float lastHit;

	
	void Start () {

        originalHealthColor = healthShowBar.GetComponentInParent<Image>().color;
        game = Game.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        if (!game) return;
        if (!player)
        {
            //May need Reworking for "Hotswap" Multiplayer if ever more than 2p
            player = game.GetPlayer(playerNumber);
            return;
        }
        portrait.sprite = player.characterPortrait;
        characterName.text = player.name.ToUpper();
        UpdateHealthBar();
        UpdateDamageBar();
        updateSpecialBar();
        if (healthTextDisplay) UpdateHealthText();
    }

    private void UpdateHealthBar()
    {
        Vector2 currentSize = healthShowBar.sizeDelta;

        float healthPercent = player.GetHealth() / player.GetMaxHealth();
        float currentWidth = currentSize.x;
        float targetWidth = healthFillContainer.sizeDelta.x * healthPercent;
        float newWidth = currentWidth;
        Color newColor = originalHealthColor;

        if (currentWidth != targetWidth)
        {
            if (Mathf.Abs(targetWidth - currentWidth) < healthAnimateSpeed)
            {
                newWidth = targetWidth;
            }
            else
            {
                newWidth = currentWidth + healthAnimateSpeed * (Mathf.Abs(targetWidth - currentWidth) / (targetWidth - currentWidth));
            }
            healthShowBar.sizeDelta = new Vector2(newWidth, currentSize.y);

            /*Color Update*/
            /*
            if (targetWidth < currentWidth)
            {
                float saturation = 1 - ((targetWidth - currentWidth) / (targetWidth - currentWidth) * hueIntensity);
                newColor = new Color(1, saturation, saturation, originalHealthColor.a);
                healthShowBar.GetComponentInParent<Image>().color = newColor;
            }
            else
            {
                healthShowBar.GetComponentInParent<Image>().color = originalHealthColor;
            }
            */

        }
    }

    private void UpdateHealthText()
    {
        healthTextDisplay.text = "" + player.GetHealth();
    }

    /*Update Damage Bar*/
    private void UpdateDamageBar()
    {
        Vector2 currentSize = damageIndicator.sizeDelta;

        float healthPercent = player.GetHealth() / player.GetMaxHealth();
        float currentWidth = currentSize.x;
        float targetWidth = healthFillContainer.sizeDelta.x * healthPercent;
        float newWidth = currentWidth;

        if (currentWidth != targetWidth)
        {
            damageBarParticles.SetActive(true);
            //Check if health has gone up - hard reset to new position
            if (targetWidth > currentWidth)
            {
                newWidth = targetWidth;
                damageIndicator.sizeDelta = new Vector2(newWidth, currentSize.y);
            }
            else
            {
                if (Time.time >= player.GetLastHit() + damageBarDelay)
                {
                    if (Mathf.Abs(targetWidth - currentWidth) < damageAnimateSpeed)
                    {
                        newWidth = targetWidth;
                    }
                    else
                    {
                        newWidth = currentWidth + damageAnimateSpeed * (Mathf.Abs(targetWidth - currentWidth) / (targetWidth - currentWidth));
                    }
                    damageIndicator.sizeDelta = new Vector2(newWidth, currentSize.y);
                }
            }
        }
        else
        {
            damageBarParticles.SetActive(false);
        }
    }

    private void updateSpecialBar()
    {
        Vector2 currentSize = specialBar.sizeDelta;

        float specialPercent = player.GetSpecial() / player.GetMaxSpecial();
        float currentWidth = currentSize.x;
        float targetWidth = specialContainer.sizeDelta.x * specialPercent;
        float newWidth = currentWidth;

        if (currentWidth != targetWidth)
        {
            if (Mathf.Abs(targetWidth - currentWidth) < specialAnimateSpeed || Mathf.Abs(targetWidth - currentWidth) > specialContainer.sizeDelta.x * .5)
            {
                newWidth = targetWidth;
            }
            else
            {
                newWidth = currentWidth + specialAnimateSpeed * (Mathf.Abs(targetWidth - currentWidth) / (targetWidth - currentWidth));
            }
            specialBar.sizeDelta = new Vector2(newWidth, currentSize.y);
            
        }

        if(specialPercent > .99)
        {
            specialParticles.SetActive(true);
        }
        else
        {
            specialParticles.SetActive(false);
        }
    }
}
