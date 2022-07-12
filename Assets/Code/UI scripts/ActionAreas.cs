using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionAreas : MonoBehaviour
{
    public GameObject movesArea;
    public GameObject teamArea;
    public GameObject inventoryArea;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void StartEverythingOff(GameObject area)
    {
        var images = area.GetComponentsInChildren<Image>();
        var texts = area.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var image in images)
        {
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), 0);
        }

        foreach (var text in texts)
        {
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), 0).OnComplete(
                () =>
                {
                    area.SetActive(false);
                });
        }
    }

    public void TurnEverythingOff(GameObject area)
    {
        var images = area.GetComponentsInChildren<Image>();
        var texts = area.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var image in images)
        {
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), 0.35f);
        }

        foreach (var text in texts)
        {
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), 0.35f);
        }
    }

    public void TurnEverythingOn(GameObject area)
    {
        area.gameObject.SetActive(true);
        var images = area.GetComponentsInChildren<Image>();
        var texts = area.GetComponentsInChildren<TextMeshProUGUI>();
        
        foreach (var image in images)
        {
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 1), 0.35f);
        }

        foreach (var text in texts)
        {
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 1), 0.35f);
        }
    }
}
