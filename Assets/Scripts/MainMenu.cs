using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void SaveGame() {
        throw new System.NotImplementedException("PlayerPrefs removed");
    }

    public void LoadGame() {
        throw new System.NotImplementedException("PlayerPrefs removed");
    }

    void SetSavedPlayerState() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log(player);
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("Pos_x"), PlayerPrefs.GetFloat("Pos_y"));
    }

    IEnumerator WaitForLevelToLoad(string level) {
        while (SceneManager.GetActiveScene().name != level) {
            yield return null;
        }

        SetSavedPlayerState();
    }
}
