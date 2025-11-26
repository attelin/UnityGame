using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _characterBody;
    [SerializeField] private float _movementSpeed;   
    private Rigidbody2D _rb;


    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandlePlayerMovement();
    }


    private void HandlePlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized * _movementSpeed;
        _rb.velocity = movement;

        bool characterIsWalking = movement.magnitude > 0f;
        _animator.SetBool("isWalking", characterIsWalking);

        bool flipSprite = movement.x < 0f;
        _characterBody.flipX = flipSprite;
    }
}
