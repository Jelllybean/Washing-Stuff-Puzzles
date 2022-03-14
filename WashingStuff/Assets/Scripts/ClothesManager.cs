using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] private List<ClothingStats> clothesPieces = new List<ClothingStats>();
    [SerializeField] private GameObject victoryScreen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckAllClothing();
        }
    }

    public void CheckAllClothing()
    {
        int total = 0;
        for (int i = 0; i < clothesPieces.Count; i++)
        {
            if(clothesPieces[i].CheckHangedStatus())
            {
                total++;
            }
        }
        if(total == clothesPieces.Count)
        {
            victoryScreen.SetActive(true);
        }
    }

    public void ResetAllClothing()
	{
        for (int i = 0; i < clothesPieces.Count; i++)
        {
            clothesPieces[i].ResetClothes();
        }
    }
}
