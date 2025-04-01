using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothesManager : MonoBehaviour
{
    [SerializeField] private List<ClothingStats> clothesPieces = new List<ClothingStats>();
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private ClothespinStats[] clothesPins;

    [InspectorButton("GetAllClothes")]
    public bool GetClothes;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckAllClothing();
        }

        clothesPins = FindObjectsByType<ClothespinStats>(FindObjectsSortMode.None);
    }

    public void CheckAllClothing()
    {
        SetPins(false);
        int total = 0;
        for (int i = 0; i < clothesPieces.Count; i++)
        {
            if(clothesPieces[i].CheckHangedStatus())
            {
                total++;
            }
        }
        Debug.Log("total: " + total);
        Debug.Log("clothes: " + clothesPieces.Count);
        if(total == clothesPieces.Count)
        {
            victoryScreen.SetActive(true);
        }
    }

    public void ResetAllClothing()
	{
        SetPins(true);
        for (int i = 0; i < clothesPieces.Count; i++)
        {
            clothesPieces[i].ResetClothes();
        }
    }

    private void SetPins(bool _setting)
    {
        for (int i = 0; i < clothesPins.Length; i++)
        {
            clothesPins[i].checkCollision = _setting;
        }
    }

    public void NextLevel(string _sceneName)
	{
        SceneManager.LoadScene(_sceneName);
    }

    public void QuitGame()
	{
        Application.Quit();
	}

    public void GetAllClothes()
    {
        clothesPieces.Clear();

        ClothingStats[] _clothingStats = FindObjectsByType<ClothingStats>(FindObjectsSortMode.None);

        for (int i = 0; i < _clothingStats.Length; i++)
        {
            clothesPieces.Add(_clothingStats[i]);
        }
    }
}
