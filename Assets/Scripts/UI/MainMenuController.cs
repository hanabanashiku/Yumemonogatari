using UnityEngine;
using UnityEngine.SceneManagement;
using Yumemonogatari.Entities;
using Yumemonogatari.Interactions;
using Yumemonogatari.Items;

namespace Yumemonogatari.UI {
    public class MainMenuController : MonoBehaviour {
        public void Quit() {
            Application.Quit();
        }

        public void NewGame() {
            SceneManager.LoadScene("Shouheikou");
            GameManager.SetUpScene();
            InteractionManager.Instance.LoadLevel(1);
            var player = FindObjectOfType<PlayerCharacter>();
            Debug.Assert(player != null);
            
            // default items
            var shinai = AssetBundles.Item.LoadAsset<MeleeWeapon>("shinai");
            player.inventory.Add(shinai);
        }

        public void LoadGame() {
            var canvas = AssetBundles.Ui.LoadAsset<GameObject>("Load Menu");
            Instantiate(canvas);
        }
    }
}