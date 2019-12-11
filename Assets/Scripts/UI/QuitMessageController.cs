using UnityEngine;
using UnityEngine.SceneManagement;
using Yumemonogatari.Entities;

namespace Yumemonogatari.UI {
    public class QuitMessageController : MonoBehaviour {
        
        public void ConfirmQuit() {
            var player = FindObjectOfType<PlayerCharacter>();
            Destroy(player.gameObject);
            Destroy(GameManager.Instance.hud);
            var gameManager = FindObjectOfType<GameManager>();
            Destroy(gameManager.gameObject);
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void CancelQuit() {
            Destroy(gameObject);
        }
    }
}