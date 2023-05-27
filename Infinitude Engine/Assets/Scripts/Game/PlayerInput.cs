using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] PlayerArrowObjects;
    public Animator[] PlayerArrowAnimator;
    private bool[] pressed = { false, false, false, false };
    private bool[] triggered = { false, false, false, false };
    private float[] distance = { 0f, 0f, 0f, 0f };
    private string[] currentState = { "LeftStatic", "DownStatic", "UpStatic", "RightStatic" };

    private void ChangeAnimationState(int id, string newState)
    {
        if (currentState[id] == newState) return;
        PlayerArrowAnimator[id].Play(newState);
        currentState[id] = newState;
    }

    private void Update()
    {
        if (Input.GetKeyDown(gameManager.Inputs[0]))
        {
            pressed[0] = true;
        }
        if (Input.GetKeyUp(gameManager.Inputs[0]))
        {
            pressed[0] = false;
        }
        if (Input.GetKeyDown(gameManager.Inputs[1]))
        {
            pressed[1] = true;
        }
        if (Input.GetKeyUp(gameManager.Inputs[1]))
        {
            pressed[1] = false;
        }
        if (Input.GetKeyDown(gameManager.Inputs[2]))
        {
            pressed[2] = true;
        }
        if (Input.GetKeyUp(gameManager.Inputs[2]))
        {
            pressed[2] = false;
        }
        if (Input.GetKeyDown(gameManager.Inputs[3]))
        {
            pressed[3] = true;
        }
        if (Input.GetKeyUp(gameManager.Inputs[3]))
        {
            pressed[3] = false;
        }

        if (pressed[0] && !triggered[0])
        {
            ChangeAnimationState(0, "LeftPress");
        }else if (!pressed[0]) ChangeAnimationState(0, "LeftStatic");

        if (pressed[1] && !triggered[1])
        {
            ChangeAnimationState(1, "DownPress");
        }
        else if (!pressed[1]) ChangeAnimationState(1, "DownStatic");

        if (pressed[2] && !triggered[2])
        {
            ChangeAnimationState(2, "UpPress");
        }
        else if (!pressed[2]) ChangeAnimationState(2, "UpStatic");

        if (pressed[3] && !triggered[3])
        {
            ChangeAnimationState(3, "RightPress");
        }
        else if (!pressed[3]) ChangeAnimationState(3, "RightStatic");
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
            if (pressed[0]) Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Down Note"))
        {
            distance[1] = Vector2.Distance(PlayerArrowObjects[1].transform.position, collision.transform.position);
            if (pressed[1]) Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Up Note"))
        {
            distance[2] = Vector2.Distance(PlayerArrowObjects[2].transform.position, collision.transform.position);
            if (pressed[2]) Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Right Note"))
        {
            distance[3] = Vector2.Distance(PlayerArrowObjects[3].transform.position, collision.transform.position);
            if (pressed[3]) Destroy(collision.gameObject);
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