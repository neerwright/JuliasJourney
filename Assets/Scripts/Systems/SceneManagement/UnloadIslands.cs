using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Scriptables;
using System;
using System.Collections.Generic;

namespace SceneManagement
{
    
    public class UnloadIslands : MonoBehaviour
    {

        private const string PLAYER_TAG = "Player";

        public void OnTriggerEnter2D(Collider2D Collider)
                {
                    if(Collider.gameObject.tag == PLAYER_TAG)
                    {    
                        
                        Scene Scene1 = SceneManager.GetSceneByName("Island4");
                        if (Scene1.isLoaded)
                            SceneManager.UnloadSceneAsync(Scene1);
                    }

                }
    }
}