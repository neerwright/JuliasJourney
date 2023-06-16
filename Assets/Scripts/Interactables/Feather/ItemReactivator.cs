using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReactivator : MonoBehaviour , IUsableItem
{
    private bool _CO_playing = false;
    Collider2D m_Collider;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //Fetch the GameObject's Collider (make sure it has a Collider component)
        m_Collider = GetComponent<Collider2D>();
    }
    
    
    public void Use()
    {
        
        if(!_CO_playing)
            StartCoroutine(ReActivate());
    }

    IEnumerator ReActivate()
    {
        _CO_playing = true;
        m_Collider.enabled =false;
        rend.enabled = false;
        yield return new WaitForSeconds(4.1f);
        m_Collider.enabled =true;
        rend.enabled = true;
        _CO_playing = false;
    }
}
