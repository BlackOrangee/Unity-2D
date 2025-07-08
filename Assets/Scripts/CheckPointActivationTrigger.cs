using UnityEngine;

public class CheckPointActivationTrigger : MonoBehaviour
{
    private Animator animator;

    public string activationTriggerName = "ActivationTrigger";

    public string resetTriggerName = "ResetTrigger";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ResetTrigger()
    {
        animator.SetTrigger(resetTriggerName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CheckPointManager>().SetCheckPoint(transform);

            animator.SetTrigger(activationTriggerName);
        }
    }
}
