using UnityEngine;

public class PlayerItemsController : MonoBehaviour
{
    public Overlay overlay;
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

            overlay.SetItems(itemAmount); 
            collision.gameObject.GetComponent<CollectableItem>().CollectItem();
        }
    }
}
