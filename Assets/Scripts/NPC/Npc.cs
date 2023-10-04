using System;
using Items;
using UnityEngine;

namespace NPC
{
    public class Npc : MonoBehaviour
    {
        protected void ShowChatBubble(string text)
        {
            if (ChatBubbleHandler.BubbleInstance)
            {
                ChatBubbleHandler.BubbleInstance.OnShowUp(gameObject.transform, text);
            }
            else
                Debug.Log("Bubble НЕ существует");
        }

        
        protected event Action<Item> GiveItem;
        protected void OnGiveItem(Item item)
        {
            GiveItem?.Invoke(item);
        }
        
        protected event Action<Item> TakeItem;
        protected void OnTakeItem(Item item)
        {
            TakeItem?.Invoke(item);
        }
    }
}