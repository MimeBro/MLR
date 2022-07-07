using UnityEngine;
public class PlayerChaser : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = PlayerController.Instance.unit.currentPanel.transform.position;
    }
}
