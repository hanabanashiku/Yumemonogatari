using UnityEngine;

public class LoadingCircle : MonoBehaviour {
    private RectTransform _rect;
    private const float Speed = 200f;
    
    private void Awake() {
        _rect = GetComponent<RectTransform>();
    }

    private void Update() {
        _rect.Rotate(0f, 0f, Speed * Time.deltaTime);
    }
}
