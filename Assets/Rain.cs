using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainPS;

    //private bool _followPlayer = false;

    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        _rainPS.Stop();
        Player = GameObject.FindWithTag("Player");
    }

    private void FollowPlayer()
    {
        Debug.Log("Follow");
        float y =_rainPS.transform.position.y;
        float x =Player.transform.position.x;
        _rainPS.transform.position = new Vector2(x, y);
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("Trigggg");
            _rainPS.Play();
            //_followPlayer = true;
            InvokeRepeating("FollowPlayer", 0.0f, 0.3f);
        }
        
    }
}
