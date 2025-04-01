using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro countText;
    public int pinAmount;

    private void Start()
    {
        if(pinAmount > 0)
        {
            countText.text = pinAmount + "x";
        }
        else
        {
            countText.gameObject.SetActive(false);
        }
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
