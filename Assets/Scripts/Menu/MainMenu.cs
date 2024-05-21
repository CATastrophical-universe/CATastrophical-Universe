using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string targetScene;

    void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SaveGame() {
        SaveLevel.Save(SceneManager.GetActiveScene().name);
        GameObject.FindGameObjectWithTag("SaveLoad").GetComponent<SavingLoading>().Save();
    }

    public void LoadGame() {
        targetScene = SaveLevel.Load();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (targetScene == null || scene.name != targetScene) {
            return;
        }

        GameObject.FindGameObjectWithTag("SaveLoad").GetComponent<SavingLoading>().Load();
    }
}
