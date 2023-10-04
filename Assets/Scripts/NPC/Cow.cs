using Items;
using Player;
using UnityEngine;

namespace NPC
{
    public class Cow : Npc, IInteractable
    {
        [SerializeField] private Item emptyMilk;
        [SerializeField] private Item filledMilk;

        private void OnEnable()
        {
            TakeItem += PlayerInventory.Inventory.RemoveItem;
            GiveItem += PlayerInventory.Inventory.ReceiveItem;
        }

        private void OnDisable()
        {
            TakeItem -= PlayerInventory.Inventory.RemoveItem;
            GiveItem -= PlayerInventory.Inventory.ReceiveItem;
        }

        public void Interact()
        {
            if (CheckBottle())
            {
                ChangeMilk();
                ShowChatBubble("Вот твое млеко");
            }
            else
            {
                ShowChatBubble("Ты пришел без бутылочки((");
            }
        }
        
        private void ChangeMilk()
        {
            OnTakeItem(emptyMilk);
            OnGiveItem(filledMilk);
        }

        private bool CheckBottle()
        {
            return PlayerInventory.Inventory.ActiveItem==emptyMilk;
        }
    }
}