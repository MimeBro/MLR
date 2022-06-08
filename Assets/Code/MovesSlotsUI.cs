using UnityEngine;

public class MovesSlotsUI : MonoBehaviour
{
    public MoveButtonUI[] moveButtons;
    
    private void Start()
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            moveButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerController.Instance.playerMovesList.Count; i++)
        {
            moveButtons[i].gameObject.SetActive(true);
            moveButtons[i].SetMove(PlayerController.Instance.playerMovesList[i]);
        }
    }
}
