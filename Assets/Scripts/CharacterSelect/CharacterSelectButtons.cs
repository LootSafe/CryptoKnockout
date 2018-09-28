using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectButtons : MonoBehaviour {
    Button button;
    public Sprite avatar_left;
    public Sprite avatar_right;
    public Characters character;

   
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public GameObject selectionIndicator;
    public bool interactable;
    public ButtonAppearance appearance;

    private int selectionCount = 0;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (selectionCount > 0)
        {
            selectionIndicator.SetActive(true);
        }
        else
        {
            selectionIndicator.SetActive(false);
        }
    }

    public void Select()
    {
        selectionCount++;
    }

    public void Deselect()
    {
        selectionCount--;
    }

    [System.Serializable]
    public class ButtonAppearance
    {
        public Image targetGraphic;
        public Color active;
        public Color normal;
        public Color disabled;
    }


}
