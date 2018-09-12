using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace InsaneSystems.InputManager
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "InputStorage", menuName = "Insane Systems/Input Manager/Input Storage")]
    public class InputStorage : ScriptableObject
    {
        const string playerPrefsControlsName = "Controls";

        static InputStorage singleton;

        public static InputStorage Singleton
        {
            get
            {
				if (!singleton)
                {
                    LoadDefaultStorage();

                    if (!IsControlsExistsInPlayerPrefs()) // in editor it's don't needed to load from file
                        SaveToPlayerPrefs(singleton);
                    else
                        LoadFromPlayerPrefs(singleton);

					singleton.usingAxis = GetStorageFromResources().usingAxis; // we need always load usingAxis from editor save.
				}

                return singleton;
            }
            private set
            {
                singleton = value;
            }
        }

        [SerializeField] List<KeyAction> keys = new List<KeyAction>();
        [SerializeField] List<AxisAction> axis = new List<AxisAction>();

        [Header("Setup all using axis in Unity Input Manager too!")]
        [Tooltip("Add here all default Input Manager axis, which can be used by player controller. Recommended to add Mouse X, Y and Scroll Wheel axis and at least 1-10 Joystick Axis.")]
        [SerializeField] string[] usingAxis;

        public List<KeyAction> Keys
        {
            get { return keys; }
        }

        public List<AxisAction> Axis
        {
            get { return axis; }
        }

        public string[] UsingAxis
        {
            get { return usingAxis; } 
        }

        public KeyAction GetKeyByName(string name)
        {
            for (int i = 0; i < keys.Count; i++)
                if (keys[i].Name == name)
                    return keys[i];

            throw new System.NullReferenceException("No key " + name + " found!");
        }

        public AxisAction GetAxisByName(string name)
        {
            for (int i = 0; i < axis.Count; i++)
                if (axis[i].Name == name)
                    return axis[i];

            throw new System.NullReferenceException("No axis " + name + " found!");
        }

        public static void SaveToPlayerPrefs() { SaveToPlayerPrefs(singleton); }
        
        static void SaveToPlayerPrefs(InputStorage toSave)
        {
            string jsonString = JsonUtility.ToJson(toSave);
            PlayerPrefs.SetString(playerPrefsControlsName, jsonString);           
        }

        static void LoadFromPlayerPrefs(InputStorage loadTo)
        {
            string loadedJson = PlayerPrefs.GetString(playerPrefsControlsName);
            JsonUtility.FromJsonOverwrite(loadedJson, loadTo);

			CheckAndUpdateStorageDataIfChanged(loadTo);
		}

		// todo refactor work
		static void CheckAndUpdateStorageDataIfChanged(InputStorage checkingStorage)
		{ 
			InputStorage defaultStorage = GetStorageFromResources();

			for (int i = 0; i < checkingStorage.keys.Count; i++)
			{
				KeyAction currentKey = checkingStorage.keys[i];

				if (!IsKeyExistsInStorage(currentKey, defaultStorage))
					checkingStorage.keys.Remove(currentKey);
			}

			for (int i = 0; i < checkingStorage.axis.Count; i++)
			{
				AxisAction currentAxis = checkingStorage.axis[i];

				if (!IsAxisExistsInStorage(currentAxis, defaultStorage))
					checkingStorage.axis.Remove(currentAxis);
			}

			for (int i = 0; i < defaultStorage.keys.Count; i++)
			{
				KeyAction currentKey = defaultStorage.keys[i];

				if (!IsKeyExistsInStorage(currentKey, checkingStorage))
					checkingStorage.keys.Add(currentKey);
			}

			for (int i = 0; i < defaultStorage.axis.Count; i++)
			{
				AxisAction currentAxis = defaultStorage.axis[i];

				if (!IsAxisExistsInStorage(currentAxis, checkingStorage))
					checkingStorage.axis.Add(currentAxis);
			}
		}

		static bool IsAxisExistsInStorage(AxisAction axis, InputStorage storage)
		{
			for (int j = 0; j < storage.axis.Count; j++)
				if (storage.axis[j].Name == axis.Name)
					return true;

			return false;
		}

		static bool IsKeyExistsInStorage(KeyAction key, InputStorage storage)
		{
			for (int j = 0; j < storage.keys.Count; j++)
				if (storage.keys[j].Name == key.Name)
					return true;

			return false;
		}
		/* Old method
				static void CheckAndUpdateStorageData(InputStorage checkingStorage)
		{ 
			InputStorage defaultStorage = GetStorageFromResources();

			for (int i = 0; i < defaultStorage.keys.Count; i++)
			{
				KeyAction currentKey = defaultStorage.keys[i];
				bool isFound = false;

				for (int j = 0; j < checkingStorage.keys.Count; j++)
					if (checkingStorage.keys[j].Name == currentKey.Name)
						isFound = true;

				if (!isFound)
					checkingStorage.keys.Add(currentKey);
			}

			for (int i = 0; i < defaultStorage.axis.Count; i++)
			{
				AxisAction currentAxis = defaultStorage.axis[i];
				bool isFound = false;

				for (int j = 0; j < checkingStorage.axis.Count; j++)
					if (checkingStorage.axis[j].Name == currentAxis.Name)
						isFound = true;

				if (!isFound)
					checkingStorage.axis.Add(currentAxis);
			}
		}
		*/

		static bool IsControlsExistsInPlayerPrefs()
        {
            return PlayerPrefs.HasKey(playerPrefsControlsName);
        }
		
        public static void LoadDefaultStorage()
        {
            singleton = GetStorageFromResources();
        }

		public static InputStorage GetStorageFromResources()
		{
			return Instantiate(Resources.Load("InputStorage") as InputStorage);
		}

		public static void ClearSettingsSave()
		{
			PlayerPrefs.DeleteKey(playerPrefsControlsName);
		}
        // Сохранение и загрузка:
        // Если нет файла controls.xml, то создаём его на основе лежащего в игре InputStorage (сериализацией)
        // если есть, то десерализуем из него и создаём новый экземпляр InputStorage (?)
    }
}