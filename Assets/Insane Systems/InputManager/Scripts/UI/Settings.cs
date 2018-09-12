using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    public class Settings : MonoBehaviour
    {
        public static Settings singleton { get; private set; }

        [SerializeField] RectTransform parentPanel;

        [SerializeField] GameObject keyEntryTemplate;
        [SerializeField] GameObject axisEntryTemplate;
        [SerializeField] GameObject headTextTemplate;

        [Header("Other Parameters")]
        [SerializeField] string keysHeaderName = "Setup Keys";
        [SerializeField] string axisHeaderName = "Setup Axis";

        [Header("Scene objects")]
        [SerializeField] GameObject helpText;
		
		List<GameObject> spawnedUIElements = new List<GameObject>();

        private void Awake()
        {
            singleton = this;
        }

        private void Start()
        {
            RefreshUI();

            SetHelpTextShown(false);
        }

        void GenerateUIForKeys()
        {
            List<KeyAction> keys = InputStorage.Singleton.Keys;

            if (keys.Count == 0)
                return;

            AddHeader(keysHeaderName, 10);

            for (int i = 0; i < keys.Count; i++)
            {
                KeyAction currentKey = keys[i];

                GameObject spawnedKeyEntry = Instantiate(keyEntryTemplate, parentPanel);
                KeyEntry keyEntry = spawnedKeyEntry.GetComponent<KeyEntry>();

                keyEntry.SetupByKeyAction(currentKey);
				
                spawnedUIElements.Add(spawnedKeyEntry);
            }
        }

        void GenerateUIForAxis()
        {
            List<AxisAction> axis = InputStorage.Singleton.Axis;

            if (axis.Count == 0)
                return;

            AddHeader(axisHeaderName, 30);

            for (int i = 0; i < axis.Count; i++)
            {
                AxisAction currentAxis = axis[i];
                GameObject spawnedAxisEntry = Instantiate(axisEntryTemplate, parentPanel);
                AxisEntry axisEntry = spawnedAxisEntry.GetComponent<AxisEntry>();

                axisEntry.SetupByAxisAction(currentAxis);

				spawnedUIElements.Add(spawnedAxisEntry);
            }
        }

        public void EventChangeKeyButtonPressed()
        {
            SetHelpTextShown(true);
        }

        public static int GetAxisIdByName(string name)
        {
            string[] usingAxis = InputStorage.Singleton.UsingAxis;

            for (int i = 0; i < usingAxis.Length; i++)
                if (usingAxis[i] == name)
                    return i;

            return -1;
        }
		
        void RefreshUI()
        {
            for (int i = 0; i < spawnedUIElements.Count; i++)
                Destroy(spawnedUIElements[i]);

			// todo DO smth with currentElementYPosition and parentPanel.sizeDelta = new Vector2(parentPanel.sizeDelta.x, Mathf.Abs(currentElementYPosition) + 120);
            GenerateUIForKeys();
            GenerateUIForAxis();
        }

        public void SaveSettings()
        {
            InputStorage.SaveToPlayerPrefs();
        }
		
		public void ResetSettings()
        {
            InputStorage.LoadDefaultStorage();

            RefreshUI();
        }

        public void SetHelpTextShown(bool value)
        {
            helpText.SetActive(value);
        }

        void AddHeader(string text, float addBottomPadding = 0)
        {
            GameObject spawnedHeader = Instantiate(headTextTemplate, parentPanel);
            Text spawnedHeaderText = spawnedHeader.GetComponent<Text>();

            spawnedHeaderText.text = text;
        }
    }
}