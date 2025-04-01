using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform currentObject;
    [SerializeField] private LayerMask lineLayers;
    [SerializeField] private LayerMask objectLayers;
    [SerializeField] private List<GameObject> clothesGrid = new List<GameObject>();
    [SerializeField] private List<GameObject> pinGrid = new List<GameObject>();
    [SerializeField] private List<Collider> clothesColliders = new List<Collider>();
    [SerializeField] private Material activeShader;
    [SerializeField] private Material inActiveShader;

    private GameObject currentTextObject;

    [InspectorButton("GetAllClothesColliders")]
    public bool GetClothesColliders;


    private ClothingStats tempCloth;

    private void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = camera.ScreenPointToRay(mouse);
        RaycastHit hit;

        if (!currentObject)
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Clothes"))
                {
                    if (currentTextObject)
                    {
                        currentTextObject.SetActive(false);
                        currentTextObject = null;
                    }
                    currentTextObject = hit.collider.gameObject.transform.root.GetChild(1).gameObject;
                    currentTextObject.SetActive(true);



                    if(!tempCloth)
                    {
                        tempCloth = hit.collider.GetComponent<ClothingStats>();
                        tempCloth.SetShaders(activeShader);
                    }
                    else if(tempCloth && tempCloth.gameObject != hit.collider.gameObject)
                    {
                        tempCloth.SetShaders(inActiveShader);
                        tempCloth = null;
                    }
                }
                else
                {
                    if (currentTextObject)
                    {
                        currentTextObject.SetActive(false);
                    }

                    if (tempCloth)
                    {
                        tempCloth.SetShaders(inActiveShader);
                        tempCloth = null;
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, objectLayers))
                {
                    currentObject = hit.collider.transform;
                    //currentObject.eulerAngles = Vector3.zero;
                    if (currentObject.CompareTag("ClothesPin"))
                    {
                        SetClothesGrid(false);
                        SetPinGrid(true);
                        SetClothesColliders(false);
                        currentObject.transform.position = Vector3.zero;
                    }
                    else if (currentObject.CompareTag("Clothes"))
                    {
                        if (currentObject.transform.parent != null)
                        {
                            currentObject = currentObject.transform.root;
                        }
                        SetClothesGrid(true);
                        SetPinGrid(false);
                        SetClothesColliders(false);
                        currentObject.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }
        else if (currentObject)
        {
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, lineLayers))
            {
                if (hit.transform.CompareTag("LineCollision"))
                {
                    currentObject.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
                    currentObject.eulerAngles = new Vector3(currentObject.eulerAngles.x, hit.transform.eulerAngles.y, currentObject.eulerAngles.z);
                }
                else if (hit.transform.CompareTag("BackgroundCollision"))
                {
                    currentObject.position = hit.point;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                currentObject = null;
                SetPinGrid(false);
                SetClothesColliders(true);
                SetClothesGrid(false);
            }
        }
    }

    private void SetClothesGrid(bool _state)
    {
        for (int i = 0; i < clothesGrid.Count; i++)
        {
            clothesGrid[i].SetActive(_state);
        }
    }

    private void SetPinGrid(bool _state)
    {
        for (int i = 0; i < pinGrid.Count; i++)
        {
            pinGrid[i].SetActive(_state);
        }
    }

    private void SetClothesColliders(bool _state_)
    {
        for (int i = 0; i < clothesColliders.Count; i++)
        {
            clothesColliders[i].enabled = _state_;
        }
    }

    public void GetAllClothesColliders()
    {
        clothesColliders.Clear();

        ClothingStats[] _clothingStats = FindObjectsByType<ClothingStats>(FindObjectsSortMode.None);
        //ClothingStats[] _clothingStats = FindObjectsOfTypeAll(typeof(ClothingStats));
        Debug.Log(_clothingStats.Length);

        for (int i = 0; i < _clothingStats.Length; i++)
        {
            clothesColliders.Add(_clothingStats[i].GetComponent<Collider>());
        }
    }
}
