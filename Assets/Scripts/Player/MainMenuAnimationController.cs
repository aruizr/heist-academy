using System;
using System.Collections;
using System.Collections.Generic;
using Codetox.Variables;
using FSM;
using Managers;
using UnityEngine;
using Variables;

public class MainMenuAnimationController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3Variable currentVelocity;
    [SerializeField] private Vector2Variable currentDirection;
    [SerializeField] private FloatVariable movementSpeed;
    [SerializeField] private FloatVariable smoothTime;
    [SerializeField] private float animationCounter = 150f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private ValueReference<string> velocityParameter;
    [SerializeField] private GameObject canvaMenu;
    
    private Vector2 _currentVelocity;
    private bool activateOutAnimation = false;
    private float counterToMove = 20f;
    
    private void Awake()
    {
        currentVelocity.Value = Vector3.zero;
        currentDirection.Value = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (activateOutAnimation)
        {
            var finalVelocity = GetFinalVelocity();
            if(counterToMove <= 0f)
                controller.Move(finalVelocity * Time.deltaTime);
            animator.SetFloat(velocityParameter.Value, 1f);

            counterToMove--;
            animationCounter--;
        }

        if (animationCounter <= 0f)
        {
            gameManager.SwitchToScene("Museum");
        }
    }

    public void MakeMove()
    {
        canvaMenu.SetActive(false);
        activateOutAnimation = true;
    }

    public void SetWalkTrigger()
    {
        animator.SetTrigger("Walk");
    }
    
    private Vector3 GetFinalVelocity()
    {
        return new Vector3(-1f, 0, 0);
    }

    private Vector2 GetFinalHorizontalVelocity(Vector2 currentHorizontalVelocity, Vector2 targetHorizontalVelocity)
    {
        return Vector2.SmoothDamp(
            currentHorizontalVelocity, 
            targetHorizontalVelocity, 
            ref _currentVelocity,
            smoothTime.Value);
    }
    
    private Vector2 GetTargetHorizontalVelocity()
    {
        return currentDirection.Value * movementSpeed.Value;
    }

    private Vector2 GetCurrentHorizontalVelocity()
    {
        return new Vector2(currentVelocity.Value.x, currentVelocity.Value.z);
    }
}
