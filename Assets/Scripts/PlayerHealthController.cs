using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public int health = 5;

    public int mainMenuIndex = 0;


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
        gameObject.GetComponent<CustomizablePlayerControllerScript>().StuckByGettingDamage();

        health -= damage;
        overlay.SetHealth(health);
        if (health <= 0)
        {
            Die();
            return;
        }

        gameObject.GetComponent<CheckPointManager>().LoadLastPoint();
    }
        
    private void Die()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }
}
