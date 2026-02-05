using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Core {
    /// <summary>
    /// Handles scene transitions, including async loading, fade effects, and persistence.
    /// </summary>
    public class SceneLoader : MonoBehaviour {
        [Header("Loading Screen")]
        [SerializeField] private CanvasGroup loadingScreen;
        [SerializeField] private float fadeDuration = 0.5f;

        [Header("Scene")]
        [SerializeField] private Scene scene;

        private void Awake() {
        }

        /// <summary>
        /// Loads a new scene by name asynchronously with a fade transition.
        /// </summary>
        public void LoadScene(Scene scene) {
            StartCoroutine(LoadSceneAsync(scene));
        }

        /// <summary>
        /// Reloads the current scene (useful for dungeon resets or player death).
        /// </summary>
        public void ReloadCurrentScene() {
            Scene currentScene = SceneManager.GetActiveScene();
            LoadScene(currentScene);
        }

        private IEnumerator LoadSceneAsync(Scene scene) {
            // Fade in loading screen
            yield return StartCoroutine(FadeLoadingScreen(1f));

            // Use build index or scene name
            int sceneIndex = scene.buildIndex; // safer than custom Indexof(scene)
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoad.allowSceneActivation = false;

            // Wait until scene is ready (0.9f means "loaded but not activated")
            while (asyncLoad.progress < 0.9f) {
                // Optionally: update loading bar (asyncLoad.progress / 0.9f)
                yield return null;
            }

            // Optional: short buffer for smooth transition
            yield return new WaitForSeconds(0.3f);

            // Activate scene
            asyncLoad.allowSceneActivation = true;

            // Wait for completion
            while (!asyncLoad.isDone)
                yield return null;

            // Fade out loading screen
            yield return StartCoroutine(FadeLoadingScreen(0f));
        }

        /// <summary>
        /// Smoothly fades the loading screen in or out.
        /// </summary>
        private IEnumerator FadeLoadingScreen(float targetAlpha) {
            if (loadingScreen == null) yield break;

            float startAlpha = loadingScreen.alpha;
            float time = 0f;

            while (time < fadeDuration) {
                time += Time.unscaledDeltaTime;
                loadingScreen.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
                yield return null;
            }

            loadingScreen.alpha = targetAlpha;
            loadingScreen.blocksRaycasts = targetAlpha > 0.5f;
        }
    }
}
