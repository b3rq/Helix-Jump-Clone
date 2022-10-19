using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSO : MonoBehaviour
{
    public Controller– controller;

    public void SelectCharacter(Scriptable– So)
    {
        controller.So = So;
        controller.Set();
    }
}