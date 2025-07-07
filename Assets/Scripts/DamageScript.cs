using UnityEngine;
public class DamageScript : MonoBehaviour
{
    public int damage = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
        }
    }
}
