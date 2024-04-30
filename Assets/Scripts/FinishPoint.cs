using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinishPoint : MonoBehaviour
{
    public string NextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}