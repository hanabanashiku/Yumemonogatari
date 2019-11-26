using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yumemonogatari {
    public class SceneController : MonoBehaviour {
        public static SceneController Instance { get; private set; }

        private List<Scene> _scenes;

        private void Awake() {
            if(Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _scenes = new List<Scene> {SceneManager.GetActiveScene()};
        }

        public IEnumerator LoadScene(string scene) {
            var s = SceneManager.GetSceneByName(scene);
            var op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            _scenes.Add(s);
            CleanScenes();
            while(!op.isDone)
                yield return null;
        }

        private IEnumerator UnloadScene(string scene) {
            _scenes.RemoveAll(x => x.name == scene);
            var op = SceneManager.UnloadSceneAsync(scene);
            while(!op.isDone)
                yield return null;
        }

        private void CleanScenes() {
            while(_scenes.Count > 3)
                StartCoroutine(UnloadScene(_scenes[0].name));
        }
    }
}