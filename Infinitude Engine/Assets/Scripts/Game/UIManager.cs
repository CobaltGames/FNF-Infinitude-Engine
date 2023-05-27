using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public RectTransform Chart;
    private void FixedUpdate()
    {
        Chart.Translate(new Vector3(0, 1.2f * gameManager.scrollSpeed * Time.deltaTime, 0));
    }
}