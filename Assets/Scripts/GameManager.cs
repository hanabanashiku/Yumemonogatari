using UnityEngine;

public class GameManager : MonoBehaviour {
    
    /// <summary>
    /// The current running game manager
    /// </summary>
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenuPrefab;
    public GameObject hud;

    private void Awake() {
        Instance = this;
    }

    public void Pause() {
        Instantiate(pauseMenuPrefab);
        hud.SetActive(false);
    }
}