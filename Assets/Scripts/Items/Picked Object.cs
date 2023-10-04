using System;
using Player;
using UnityEngine;

namespace Items
{
    public class PickedObject : MonoBehaviour, IInteractable
    { 
        [SerializeField] private Item inventoryItem;

        public Item InventoryItem { get => inventoryItem; set => inventoryItem = value; }
        private void OnEnable()
        {
            PickItem += PlayerInventory.Inventory.ReceiveItem;
        }
        private void OnDisable()
        {
            PickItem -= PlayerInventory.Inventory.ReceiveItem;
        }
    
        private event Action<Item> PickItem;

        private void OnItemPicked(Item item)
        {
            PickItem?.Invoke(item);
        }

        public void Interact()
        {
            OnItemPicked(inventoryItem);

            Destroy(gameObject);
        }
    }
}
