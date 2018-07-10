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

    public RectTransform damageIndicator;
    public float damageAnimateSpeed;
    Vector2 damageIndicatorSize;

    public RectTransform specialBar;
    Vector2 specialBarSize;
    public float specialAnimateSpeed;

    public Text healthTextDisplay;

    Game game;
    Player player;
    float containerWidth;
    float lastHit;

	
	void Start () {
        containerWidth = GetComponent<RectTransform>().sizeDelta.x;
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

        UpdateHealthBar();
        UpdateDamageBar();
        if (healthTextDisplay) UpdateHealthText();
    }

    private void UpdateHealthBar()
    {
        Vector2 currentSize = healthShowBar.sizeDelta;

        float healthPercent = player.GetHealth() / player.GetMaxHealth();
        float currentWidth = currentSize.x;
        float targetWidth = containerWidth * healthPercent;
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

        }
    }

    private void UpdateHealthText()
    {
        healthTextDisplay.text = "" + player.GetHealth();
    }

    private void UpdateDamageBar()
    {
        Vector2 currentSize = damageIndicator.sizeDelta;

        float healthPercent = player.GetHealth() / player.GetMaxHealth();
        float currentWidth = currentSize.x;
        float targetWidth = containerWidth * healthPercent;
        float newWidth = currentWidth;

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

        }
    }
}
