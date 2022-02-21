using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialAnimated : MonoBehaviour
{
    [SerializeField]
    private Material m_mat;
    [SerializeField]
    private string valueKey;
    [SerializeField]
    private float value; 
    // Update is called once per frame
    void Update()
    {
        if(m_mat.HasFloat(valueKey)){
            m_mat.SetFloat(valueKey, value);
        }
    }
}
