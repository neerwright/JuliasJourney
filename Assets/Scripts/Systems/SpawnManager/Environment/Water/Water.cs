using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float speed;

    [SerializeField] private Renderer waterRenderer;

    [SerializeField] private Material[] waterMaterial;

    private float _timer = 0f;
    private int _index = 0;
    private const float START_ANIMATION_THREASHOLD = 1.2f;
    private const float SPEED = 0.2f;
    private float _animationTimer = 0f;
    private bool forward = true;

    void Start()
    {
        
    }


    void Update()
    {
        if(waterRenderer != null)
        {
            waterRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);

            _timer += Time.deltaTime;

            if(_timer > START_ANIMATION_THREASHOLD)
            {
                _animationTimer += Time.deltaTime;
                
                if(_animationTimer > SPEED)
                {
                    _animationTimer = 0;

                    if(forward)
                    {
                        waterRenderer.material = waterMaterial[_index];
                        _index++;
                        if(_index >= waterMaterial.Length)
                        {
                            _index = waterMaterial.Length -1;
                            forward = false;
                        }
                    }
                    else
                    {
                        waterRenderer.material = waterMaterial[_index];
                        _index--;

                        if(_index > 0)
                        {
                            _index = 0;
                            forward = true;
                            _timer = 0;
                        }
                    }
                }
                
            }
            
        

        }
            

            
    }
}
