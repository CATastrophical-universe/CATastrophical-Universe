using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    [Header("Default Level")]
    [SerializeField] string defaultSceneName;

    public void LoadLevelButton(string level)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(level));
    }

    public void LoadSavedLevel()
    {
        string level = SaveLevel.Load();
        
        if (level == null) {
            level = defaultSceneName;
        }

        StartCoroutine(LoadLevelASync(level));
    }

    IEnumerator LoadLevelASync(string level)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);

            loadingSlider.value = progressValue;

            yield return null;
        }
    }
}
