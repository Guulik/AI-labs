﻿using System;
using System.Collections;
using System.Linq;
using Items;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace NPC
{
    public class Npc : Interactable
    {
        [SerializeField] protected bool isAbleToMove = false;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float movingTime = 1f;
        [SerializeField] private float stopTime = 1f;
        [Space(20)] 

        private Vector2 circleRadius = Vector2.zero;
        private Coroutine _coroutine;
        private Rigidbody2D _rb2d;
        
        private InteractUI _interactionMarker;
        private Animator _animator;
        private SpriteRenderer _sprite;
        protected PlayerInput _playerInput;
        

        private void Awake()
        {
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.gravityScale = 0f;
            _interactionMarker = GetComponentInChildren<InteractUI>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }
        

        private void Update()
        {
            if (IsPlayerNearby())
            {
                _interactionMarker.Show();
                isAbleToMove = false;
                _canInteract = true;
            }
            else
            {
                _interactionMarker.Hide();
                isAbleToMove = true;
                _canInteract = false;
            }

            
            if (isAbleToMove)
                _coroutine ??= StartCoroutine(Move());
        }

        #region Moving

        private void SetVelocity(Vector2 circleRadius)
        {
            _rb2d.velocity = new Vector2(circleRadius.x, circleRadius.y);
        }

        private void moveRadiusCalculation()
        {
            circleRadius = Random.insideUnitCircle * moveSpeed;
            _sprite.flipX = circleRadius.x < 0;
        }

        private IEnumerator Move()
        {
            _animator.SetBool("isWalking",true);
            moveRadiusCalculation();
            SetVelocity(circleRadius);
            yield return new WaitForSeconds(movingTime);
            _animator.SetBool("isWalking",false);
            SetVelocity(Vector2.zero);
            yield return new WaitForSeconds(stopTime);
            _coroutine = null;
        }
        
        #endregion

        private bool IsPlayerNearby()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (var collider in colliders)
            {
                if (collider.gameObject == _player)
                {
                    return true;
                }
            }
            return false;
        }
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