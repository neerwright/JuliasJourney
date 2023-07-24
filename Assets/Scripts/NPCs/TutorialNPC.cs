using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public enum TurorialType
    {
        AtoJump,
        YtoReset,
        BtoRewind
    }

    public class TutorialNPC : MonoBehaviour
    {
        [SerializeField] GameObject _bubblePrefab;
        [SerializeField] TurorialType _tutorialType = TurorialType.AtoJump;
        
        private bool _isDisplayingText = false;

        private const float OFFSET_X = -3f;
        private const float OFFSET_Y = 2f;


        public void CreateBubble(Transform parent, Vector3 localPosition, string text)
        {
            GameObject chatBubble = Instantiate(_bubblePrefab, parent );
            Transform chatBubbleTransform = chatBubble.transform;
            
            chatBubbleTransform.localPosition = localPosition;
            chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);
            StartCoroutine(DestroyABubbleAfterSeconds(4f, chatBubble));
            
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(!_isDisplayingText)
            {
                _isDisplayingText = true;
                Vector2 spawnPoint = Vector3.zero;
                spawnPoint.x += OFFSET_X;
                spawnPoint.y += OFFSET_Y;
                string text =  _decideText();
                
                
                CreateBubble(gameObject.transform , spawnPoint , text);
            }
                
        }

        private string _decideText()
        {
            string text = "";
            if(_tutorialType == TurorialType.AtoJump)
            {
                text = "Press        /SPACE on a         to double jump";
            }
            return text;
        }
        private IEnumerator DestroyABubbleAfterSeconds(float sec , GameObject chatBubble)
        {
            yield return new WaitForSeconds(sec);
            Destroy(chatBubble, 4f);
            _isDisplayingText = false;
        }
    }
}
