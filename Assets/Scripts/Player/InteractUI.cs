using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    [SerializeField] private PlayerInteract _playerInteract;

    void Update()
    {
        if (_playerInteract.getInteractable() != null) Show();
        else Hide();
    }
    
    void Show()
    {
        interactUI.SetActive(true);
    }

    void Hide()
    {
        interactUI.SetActive(false);
    }
}
