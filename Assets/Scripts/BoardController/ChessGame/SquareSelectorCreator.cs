using System.Collections.Generic;
using UnityEngine;

public class SquareSelectorCreator : MonoBehaviour
{
    [SerializeField] private Material _freeSquareMaterial;
    [SerializeField] private Material _enemySquareMaterial;
    [SerializeField] private GameObject _selectorPrefab;

    private List<GameObject> _instantiatedSelectors = new List<GameObject>();

    public void ShowSelection(Dictionary<Vector3, bool> squareData, Transform parent)
    {
        ClearSelection();
        foreach (var data in squareData)
        {
            GameObject selector = Instantiate(_selectorPrefab, parent);

            selector.transform.localPosition = data.Key;
            // selector.transform.localScale = Vector3.Scale(selector.transform.localScale, scaleBoard);

            _instantiatedSelectors.Add(selector);
            foreach (var setter in selector.GetComponentsInChildren<MaterialSetter2D>())
            {
                setter.SetSingleMaterial(data.Value ? _freeSquareMaterial : _enemySquareMaterial);
            }
        }
    }

    public void ClearSelection()
    {
        for (int i = 0; i < _instantiatedSelectors.Count; i++)
        {
            Destroy(_instantiatedSelectors[i]);
        }
    }
}