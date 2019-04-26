using UnityEngine;
using System.Collections.Generic;

public interface IInputManager
{
    void ChangeKeyCode(string inputName, KeyCode newKeyCode);
    Dictionary<string, KeyCode> GetInputs();

}

public class InputManagerProxy : UniqueProxy<IInputManager>
{
}