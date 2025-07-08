using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    public int itemAmount = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            itemAmount++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<CollectableItem>() != null)
        {
            itemAmount++;

            collision.gameObject.GetComponent<CollectableItem>().CollectItem();
        }
    }
}
