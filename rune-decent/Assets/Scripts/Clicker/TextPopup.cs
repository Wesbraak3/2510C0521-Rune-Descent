using System.Collections;
using TMPro;
using UnityEngine;


public class TextPopup : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private TextMeshProUGUI textMesh;
    private Color textColor;


    public static void CreateTextPopup(Vector3 position, string text, Color color, Vector3 offset = new()) {
        // Load prefab (must exist in Resources/UI/TextPopup.prefab)
        Transform textPopupTransform = Instantiate(Resources.Load<Transform>("UI/TextPopup"), position + offset, Quaternion.identity);

        // Get TextPopup component and show popup
        TextPopup textPopup = textPopupTransform.GetComponent<TextPopup>();
        textPopup.ShowPopup(text, color);
    }
    private void Awake() {
    }

    public void ShowPopup(string text, Color color) {
        textMesh.text = text;
        textMesh.color = color;
        textColor = color;

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        while (textColor.a > 0f) {
            // Move upward
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

            // Fade out
            textColor.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = textColor;

            yield return null;
        }

        Destroy(gameObject); // Cleanup after fading
    }
}
