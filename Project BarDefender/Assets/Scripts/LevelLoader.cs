using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] string _sceneName;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _loadingBar;
    [SerializeField] TextMeshProUGUI _progressText;

    private void Start()
    {
        StartCoroutine(LoadAsynchronously(_sceneName));
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            _loadingBar.value = progress;
            _progressText.text = (progress * 100f).ToString() + "%";
            yield return null;
        }
    }


}
