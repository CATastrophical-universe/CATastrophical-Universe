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
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Pos_x", playerPosition.x);
        PlayerPrefs.SetFloat("Pos_y", playerPosition.y);

        PlayerPrefs.Save();
    }

    public void LoadGame() {
        if (!PlayerPrefs.HasKey("Level")) {
            return;
        }

        string level = PlayerPrefs.GetString("Level");

        WaitForLevelToLoad(level);
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
