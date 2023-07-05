using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC 
{
    public class BubbleCreator : MonoBehaviour
    {
        [SerializeField] GameObject _bubblePrefab;
        
        public void CreateBubble(Transform parent, Vector3 localPosition, string text)
        {
            GameObject chatBubble = Instantiate(_bubblePrefab, parent );
            Transform chatBubbleTransform = chatBubble.transform;
            chatBubbleTransform.localPosition = localPosition;
            chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);

            //Destroy(chatBubble, 4f);
        }

        private void Start()
        {
            CreateBubble(gameObject.transform, Vector3.zero, "Hellooooo");
        }

    } 
}

