using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSO : MonoBehaviour
{
    public ControllerĐ controller;

    public void SelectCharacter(ScriptableĐ So)
    {
        controller.So = So;
        controller.Set();
    }
}