using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro countText;
    [SerializeField] private int pinAmount;

    private void Start()
    {
        countText.text = pinAmount + "x";
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ClothesPin"))
        {
            pinAmount--;
            countText.text = pinAmount + "x";
        }
    }
}
