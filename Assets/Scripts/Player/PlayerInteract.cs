using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    float interactRange = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }
        
    }

    public IInteractable getInteractable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable npcInteractable))
            {
                return npcInteractable;
            }
        }

        return null;
    }
}
