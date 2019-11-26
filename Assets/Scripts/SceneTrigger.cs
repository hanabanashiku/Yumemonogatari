using UnityEngine;
using Yumemonogatari.Entities;

namespace Yumemonogatari {
    [RequireComponent(typeof(Collider2D))]
    public class SceneTrigger : MonoBehaviour {
        public string scene;

        private void OnTriggerEnter(Collider c) {
            if(c.GetComponent<PlayerCharacter>() == null)
                return;
            StartCoroutine(SceneController.Instance.LoadScene(scene));
        }
    }
}