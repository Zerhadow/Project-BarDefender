using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 120;
    private TextMeshProUGUI _text = null;
    [SerializeField] public string sceneName;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            _text.text = "Time Remaining: " + Mathf.FloorToInt(timeRemaining);
        }
        else
        {
            Debug.Log("Time's Up!");
            SceneManager.LoadScene(sceneName);
        }
    }
}
