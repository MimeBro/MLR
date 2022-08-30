using UnityEngine;
public class PlayerChaser : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = PlayerController.Instance.oldUnit.currentPanel.transform.position;
    }
}
