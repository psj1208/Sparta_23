using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClawControl : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    [SerializeField] float CloseRot;
    [SerializeField] float OpenRot;

    // Start is called before the first frame update
    void Start()
    {
        leftHand.rotation = Quaternion.Euler(0, 0, -CloseRot);
        rightHand.rotation = Quaternion.Euler(0, 180, -CloseRot);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Open()
    {
        
    }

    private void Close()
    {

    }
}
