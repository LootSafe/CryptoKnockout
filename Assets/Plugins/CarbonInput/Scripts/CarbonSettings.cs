using UnityEngine;

namespace CarbonInput {
    /// <summary>
    /// Used to store global settings for CarbonInput.
    /// </summary>
    [CreateAssetMenu(fileName = "CarbonInput", menuName = "Carbon Input/Settings", order = 100)]
    public class CarbonSettings : ScriptableObject {
        /// <summary>
        /// Defines the behaviour of PlayerIndex.Any
        /// </summary>
        [Tooltip("Defines the behaviour of PlayerIndex.Any")]
        public AnyBehaviour Behaviour = AnyBehaviour.CheckAll;
        /// <summary>
        /// Defines if any <see cref="CAxis"/> must be inverted.
        /// </summary>
        [SerializeField]
        // ReSharper disable once InconsistentNaming
        private bool[] InvertedAxis = new bool[CarbonController.AxisCount];

        /// <summary>
        /// Gets or sets the given axis to be inverted or not.
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public bool this[CAxis axis] {
            get { return InvertedAxis[(int)axis]; }
            set { InvertedAxis[(int)axis] = value; }
        }

        /// <summary>
        /// Will try to load the CarbonInput asset. If the asset is not found, it wil return a new CarbonSettings object.
        /// </summary>
        /// <returns></returns>
        public static CarbonSettings Default() {
            CarbonSettings settings = Resources.Load<CarbonSettings>("CarbonInput");
            if(settings != null) return settings;
            return CreateInstance<CarbonSettings>();
        }
    }
}
