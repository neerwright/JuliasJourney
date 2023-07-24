using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Renderer _bridgeSpriteRenderer;
    [SerializeField] private Renderer _frontSpriteRenderer;
    [SerializeField] private Collider2D _bridgeCollider;

    private const float ALPHA_INCREMENT = 25.0f;
    private float _fadeIn = 0.1f;

    private bool isInvis = true;
    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        

        //Color tmp = _bridgeSpriteRenderer.color;
        //tmp.a = 0f;
        //_bridgeSpriteRenderer.color = tmp;
        //_frontSpriteRenderer.color = tmp;

        _bridgeCollider.enabled = (false);
        _bridgeSpriteRenderer.enabled = false;
        _frontSpriteRenderer.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && isInvis)
        {
            if(_bridgeSpriteRenderer != null)
            {
                isInvis = false;
                _bridgeCollider.enabled = (true);
        _bridgeSpriteRenderer.enabled = true;
        _frontSpriteRenderer.enabled = true;
                //StartCoroutine("fadeIn");
            }
        }
        
        
    }
/*
    private IEnumerator fadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        _bridgeCollider.enabled = (true);
        _bridgeSpriteRenderer.enabled = true;
        _frontSpriteRenderer.enabled = true;

        Color tmp = _bridgeSpriteRenderer.color;
        
        while(tmp.a < 1f)
        {
                tmp = _bridgeSpriteRenderer.color;
                tmp.a += ALPHA_INCREMENT * Time.deltaTime;
                _bridgeSpriteRenderer.color = tmp;
                _frontSpriteRenderer.color = tmp;
                yield return new WaitForSeconds(_fadeIn);
        }
        
        
    }
    */
    
}