using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = PlayerController.Instance.unit.currentPanel.transform.position;
    }
}
