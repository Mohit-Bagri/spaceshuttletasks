using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(InsideTrash);
    }

    // Specify the return type (void) for the method
    public void InsideTrash(GameObject go)
    {
        go.SetActive(false);
    }
}
