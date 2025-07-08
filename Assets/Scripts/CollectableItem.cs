using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private Animator animator;

    public string collectTriggerName = "Collect";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CollectItem()
    {
        animator.SetTrigger(collectTriggerName);
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
