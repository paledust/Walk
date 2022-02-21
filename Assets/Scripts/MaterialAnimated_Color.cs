using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialAnimated_Color : MonoBehaviour
{
    [SerializeField]
    private Material m_mat;
    [SerializeField]
    private string valueKey;
    [SerializeField]
    private Color value; 
    // Update is called once per frame
    void Update()
    {
        if(m_mat.HasColor(valueKey)){
            m_mat.SetColor(valueKey, value);
        }
    }
}
