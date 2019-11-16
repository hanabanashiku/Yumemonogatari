using System;
using UnityEngine.SceneManagement;

namespace Yumemonogatari.Interactions {
    [Serializable]
    public class OnSceneLoadedTrigger : Trigger {

        public override TriggerTypes Type => TriggerTypes.OnSceneLoaded;

        public string SceneName;

        public override bool ConditionIsMet() 
            => SceneManager.GetActiveScene().name.Equals(SceneName);
    }
}