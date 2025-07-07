using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel_Trigger : MonoBehaviour
{

    public int levelToLoad = -1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(levelToLoad >= 0 ? levelToLoad : SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
