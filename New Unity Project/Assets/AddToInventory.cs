using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    public Canvas inventory;
    public Image image;
    private bool full = false;
    private bool done = false;
    private int count = 0;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Clump" && !full)
        {
            Destroy(collider.gameObject);
            
            for (int i = 0; i < 20; i++)
            {
                image = inventory.transform.Find("Inventory/ItemsParent/InventorySlot (" + i.ToString() + ")").gameObject.transform.Find("ItemButton/Icon").gameObject.GetComponent<Image>();
                if (!image.IsActive() && !done)
                {
                    image.enabled = true;
                    done = true;
                    
                }
                if (image.IsActive())
                {
                    count += 1;
                }
                if (count == 19)
                {
                    full = true;
                }
            }
            done = false;
            count = 0;
        }
    }
}
