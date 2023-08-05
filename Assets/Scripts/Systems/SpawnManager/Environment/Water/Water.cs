using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float scrollSpeed;

    private SpriteRenderer waterRenderer;

    //[SerializeField] private Material waterMaterial;
    [SerializeField] private Sprite[] waterTextures;

    private float _timer = 0f;
    private int _index = 0;
    private const float START_ANIMATION_THREASHOLD = 1.2f;
    private const float CHANGE_TEX_SPEED = 0.9f;
    private float _animationTimer = 0f;
    private bool forward = true;

    void Start()
    {
        waterRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("UpdateWater", 0, CHANGE_TEX_SPEED);
    }


    void UpdateWater()
    {
        //waterRenderer.material.mainTextureOffset += new Vector2(scrollSpeed , 0f);
        
        if(forward)
        {
            
            _index++;
            waterRenderer.sprite = waterTextures[_index];
            if(_index >= waterTextures.Length -1)
            {
                _index = waterTextures.Length -1;
                forward = false;
            }
        }
        else
        {
            
            
            
            _index--;
            waterRenderer.sprite = waterTextures[_index];

            if(_index <= 0)
            {
                _index = 0;
                forward = true;
                
            }
        }
                       
    }
}
