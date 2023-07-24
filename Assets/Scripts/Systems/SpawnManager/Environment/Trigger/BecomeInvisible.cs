using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeInvisible : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private const float ALPHA_DECREMENT = 25.0f;
    private float _fadeOut = 0.1f;

    private bool isInvis = false;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer= GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && !isInvis)
        {
            if(_spriteRenderer != null)
            {
                isInvis = true;
                StartCoroutine("fadeOut");
            }
        }
        
        
    }

    private IEnumerator fadeOut()
    {
        Color tmp = _spriteRenderer.color;
        while(tmp.a > 0.1f)
        {
                tmp = _spriteRenderer.color;
                tmp.a -= ALPHA_DECREMENT * Time.deltaTime;
                _spriteRenderer.color = tmp;
                yield return new WaitForSeconds(_fadeOut);
        }
        _spriteRenderer.enabled = false;
        
        
    }
    
}
