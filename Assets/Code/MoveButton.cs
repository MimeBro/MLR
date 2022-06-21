using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    private RectTransform _rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
