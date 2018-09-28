using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonExtension : MonoBehaviour, ISelectHandler, IPointerEnterHandler {

    public Oscillator oscillator;
    public EventSystem e;
    Button b;
    Vector2 originalPosition;

    public void OnPointerEnter(PointerEventData eventData)
    {
        oscillator.enabled = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        oscillator.enabled = false;
    }

    public void OnMouseOver()
    {
        Debug.Log("Button Selected");
    }

    public void OnMouseEnter()
    {
        
    }

    public void OnMouseExit()
    {
        oscillator.enabled = false;
    }

    // Use this for initialization
    void Start () {
        oscillator = GetComponent<Oscillator>();
        b = GetComponent<Button>();
        originalPosition = b.GetComponent<RectTransform>().position;
	}
	
	// Update is called once per frame
	void Update () {
		if(e.currentSelectedGameObject == gameObject)
        {
            oscillator.enabled = true;
        }
        else
        {
            oscillator.enabled = false;
            GetComponent<RectTransform>().position = originalPosition;
        }
	}
}
