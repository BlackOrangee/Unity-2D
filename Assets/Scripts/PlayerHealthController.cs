using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public int health = 5;

    public int mainMenuIndex = 0;

    public Transform spawnPoint;

    public Overlay overlay;

    void Start()
    {
        overlay.Initialize(health);
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        overlay.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }

        transform.position = spawnPoint.position;
    }
        
    private void Die()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
}
