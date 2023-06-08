using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputOld : MonoBehaviour
{
    public GameObject[] PlayerArrowObjects;
    public Animator[] PlayerArrowAnimator;
    private bool[] pressed = { false, false, false, false };
    private bool[] confirmPressed = { false, false, false, false };
    private bool[] triggered = { false, false, false, false };
    private bool[] hit = { false, false, false, false };
    private float[] distance = { 0f, 0f, 0f, 0f };
    private string[] currentState = { "LeftStatic", "DownStatic", "UpStatic", "RightStatic" };
    private void ChangeAnimationState(int id, string newState)
    {
        if (currentState[id] == newState) return;
        PlayerArrowAnimator[id].Play(newState);
        currentState[id] = newState;
    }

    private void CalculateRatings(float distance)
    {

    }

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(GameManager.Instance.Inputs[i]))
            {
                pressed[i] = true;
            }
            if (Input.GetKeyUp(GameManager.Instance.Inputs[i]))
            {
                pressed[i] = false;
                confirmPressed[i] = false;
                hit[i] = false;
            }
        }
        
        if (pressed[0] && !triggered[0] && !confirmPressed[0])
        {
            ChangeAnimationState(0, "LeftPress");
        }else if (!pressed[0]) ChangeAnimationState(0, "LeftStatic");

        if (pressed[1] && !triggered[1] && !confirmPressed[1])
        {
            ChangeAnimationState(1, "DownPress");
        }else if (!pressed[1]) ChangeAnimationState(1, "DownStatic");

        if (pressed[2] && !triggered[2] && !confirmPressed[2])
        {
            ChangeAnimationState(2, "UpPress");
        }else if (!pressed[2]) ChangeAnimationState(2, "UpStatic");

        if (pressed[3] && !triggered[3] && !confirmPressed[3])
        {
            ChangeAnimationState(3, "RightPress");
        }else if (!pressed[3]) ChangeAnimationState(3, "RightStatic");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left Note")) triggered[0] = true;
        if (collision.CompareTag("Down Note")) triggered[1] = true;
        if (collision.CompareTag("Up Note")) triggered[2] = true;
        if (collision.CompareTag("Right Note")) triggered[3] = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Left Note"))
        {
            distance[0] = Vector2.Distance(PlayerArrowObjects[0].transform.position, collision.transform.position);
            if (pressed[0] && !hit[0])
            {
                CalculateRatings(distance[0]);
                hit[0] = true;
                confirmPressed[0] = true;
                ChangeAnimationState(0, "LeftConfirm");
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Down Note"))
        {
            distance[1] = Vector2.Distance(PlayerArrowObjects[1].transform.position, collision.transform.position);
            if (pressed[1] && !hit[1])
            {
                CalculateRatings(distance[1]);
                hit[1] = true;
                confirmPressed[1] = true;
                ChangeAnimationState(1, "DownConfirm");
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Up Note"))
        {
            distance[2] = Vector2.Distance(PlayerArrowObjects[2].transform.position, collision.transform.position);
            if (pressed[2] && !hit[2])
            {
                CalculateRatings(distance[2]);
                hit[2] = true;
                confirmPressed[2] = true;
                ChangeAnimationState(2, "UpConfirm");
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Right Note"))
        {
            distance[3] = Vector2.Distance(PlayerArrowObjects[3].transform.position, collision.transform.position);
            if (pressed[3] && !hit[3])
            {
                CalculateRatings(distance[3]);
                hit[3] = true;
                confirmPressed[3] = true;
                ChangeAnimationState(3, "RightConfirm");
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left Note")) triggered[0] = false;
        if (collision.CompareTag("Down Note")) triggered[1] = false;
        if (collision.CompareTag("Up Note")) triggered[2] = false;
        if (collision.CompareTag("Right Note")) triggered[3] = false;
    }
}