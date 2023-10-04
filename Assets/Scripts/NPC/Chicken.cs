using System;
using System.Linq;
using Items;
using Player;
using UnityEngine;

namespace NPC
{
    public class Chicken : Npc, IInteractable
    {
        [SerializeField] private GameObject reward;
        [SerializeField] private Item requiredPass;

        private void OnEnable()
        {
            TakeItem += PlayerInventory.Inventory.RemoveItem;
        }

        private void OnDisable()
        {
            TakeItem -= PlayerInventory.Inventory.RemoveItem;
        }

        public void Interact()
        {
            if (CheckPass())
                GiveEgg();
            else 
                ShowChatBubble("Принеси пожалуйста Млеко");
        }
        
        private bool CheckPass()
        {
                return PlayerInventory.Inventory.ActiveItem == requiredPass 
                       && PlayerInventory.Inventory.ActiveItem.ToPass().isCorrect;
        }

        private void TakeMilk()
        {
            OnTakeItem(requiredPass);
        }
        private void GiveEgg()
        {
            if (CheckPass())
            {
                TakeMilk();
                
                ShowChatBubble("Держи яйко");
                
                GameObject rewardObject = Instantiate(reward, gameObject.transform);
                rewardObject.transform.localPosition = new Vector3(2f, 0.5f, 0f);
            }
        }
    }
}

