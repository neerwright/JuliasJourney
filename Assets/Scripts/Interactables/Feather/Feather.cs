using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour , IUsableItem
{
    // Start is called before the first frame update
    public void Use()
    {
        Debug.Log("used");
    }
}
