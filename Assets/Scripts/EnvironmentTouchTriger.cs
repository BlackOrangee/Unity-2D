using UnityEngine;

public class EnvironmentTouchTriger : MonoBehaviour
{
    private Animator animator;

    public string triggerName = "Touch";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger(triggerName);
        }
    }
}
