using UnityEngine;
using UnityEngine.SceneManagement;
using Yumemonogatari.Interactions;
namespace Yumemonogatari.UI {
    public class MainMenuController : MonoBehaviour {
        public void Quit() {
            Application.Quit();
        }

        public void NewGame() {
            SceneManager.LoadScene("Shouheikou");
            var canvas = AssetBundles.Ui.LoadAsset<GameObject>("HUDCanvas");
            canvas = Instantiate(canvas);
            DontDestroyOnLoad(canvas);
            var manager = AssetBundles.Ui.LoadAsset<GameObject>("GameManager");
            manager = Instantiate(manager);
            DontDestroyOnLoad(manager);
            InteractionManager.Instance.LoadLevel(1);
            var player = AssetBundles.Spawns.LoadAsset<GameObject>("Player");
            player = Instantiate(player);
            DontDestroyOnLoad(player);
        }

        public void LoadGame() {
            var canvas = AssetBundles.Ui.LoadAsset<GameObject>("Load Menu");
            Instantiate(canvas);
        }
    }
}