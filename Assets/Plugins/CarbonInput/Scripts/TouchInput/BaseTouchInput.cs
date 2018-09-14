using UnityEngine;

namespace CarbonInput {
    /// <summary>
    /// Base class for all touch controls.
    /// </summary>
    public class BaseTouchInput : MonoBehaviour {
        /// <summary>
        /// The index of the player this control belongs to. If set to Any, it will use the first free player.
        /// </summary>
        [Tooltip("The index of the player this control belongs to. If set to Any, it will use the first free player.")]
        public PlayerIndex Index;
        /// <summary>
        /// Mapping of this control.
        /// </summary>
        protected TouchMapping Mapping;

        /// <summary>
        /// Initialize this input by injecting a <see cref="TouchMapping"/> into <see cref="GamePad.PlayerMappings"/>.
        /// </summary>
        protected void InitMapping() {
            if(Index == PlayerIndex.Any) {
                ControllerInstance[] mappings = GamePad.GetPlayerMappings();
                for(int i = 1; i < CarbonController.PlayerIndices; i++) {
                    if(mappings[i].Controller.Replacable || mappings[i].Controller is TouchMapping) {
                        UseMapping(i);
                        return;
                    }
                }
                // all mappings already in use
            } else {
                UseMapping((int)Index);
            }
        }

        /// <summary>
        /// Changes index <paramref name="idx"/> of the <see cref="GamePad.PlayerMappings"/> to a <see cref="TouchMapping"/>.
        /// </summary>
        /// <param name="idx"></param>
        private void UseMapping(int idx) {
            ControllerInstance[] mappings = GamePad.GetPlayerMappings();
            // if there is already a TouchMapping, use it.
            if(mappings[idx] != null && mappings[idx].Controller is TouchMapping)
                Mapping = (TouchMapping)mappings[idx].Controller;
            else {//otherwise overwrite the old value
                Mapping = ScriptableObject.CreateInstance<TouchMapping>();
                mappings[idx] = new ControllerInstance(Mapping, 0);
            }
            // if we set PlayerIndex.One, we must also set PlayerIndex.Any, because AnyBehaviour.UseMappingOne needs this
            if(idx == 1) mappings[0] = mappings[1];
        }
    }
}
