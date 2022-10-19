using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerĞ : MonoBehaviour
{
    [SerializeField] public ScriptableĞ So;

    public string Name;
    public float speed;
    public Color color;

    public void Set()
    {
        Name = So.Name;
        speed = So.Speed;
        color = So.color;
        GetComponent<MeshRenderer>().material.color = color;
    }
}