using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI curFloorText;

    [SerializeField]
    private TextMeshProUGUI totalFloorText;

    public void SetCurFloor(int curFloor)
    {
        curFloorText.text = curFloor.ToString();
    }

    public void SetTotalFloor(int totalFloor)
    {
        totalFloorText.text = totalFloor.ToString();
    }
}
