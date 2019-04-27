using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFirstSelected : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(SelectButtonRoutine());
    }

    IEnumerator SelectButtonRoutine()
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}