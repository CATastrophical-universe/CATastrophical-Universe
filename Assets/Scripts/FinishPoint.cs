using UnityEngine;
using UnityEngine.SceneManagement; 

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private string NextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            SceneManager.LoadSceneAsync(NextScene);
        }
    }
}