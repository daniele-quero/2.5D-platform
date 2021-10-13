using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UI3DElement : MonoBehaviour
{
    [SerializeField]
    private GameObject _original;
    [SerializeField]
    private float _scale = 20f;

    public bool lookAtCamera = true;
    void Start()
    {
        InstantiateIcon();
        CleanComponents();
        if (lookAtCamera)
            transform.LookAt(Camera.main.transform);
    }

    private void InstantiateIcon()
    {
        GameObject obj;
        if (_original != null)
        {
            obj = Instantiate(_original, Vector3.zero, Quaternion.identity);
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one * _scale;
        }
    }

    private void CleanComponents()
    {
        var components = GetComponentsInChildren<Component>();
        foreach (var c in components)
            if (!(c is Transform) && !(c is RectTransform)
                && !(c is MeshFilter) && !(c is MeshRenderer)
                && !(c is SkinnedMeshRenderer)
                && !(c is Animator) && c != this)
            {
                Component.Destroy(c);
            }
    }


}
