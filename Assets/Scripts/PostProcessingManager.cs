using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private GameObject _rewindPostProcessing;
    // Start is called before the first frame update
    void Start()
    {
        _rewindPostProcessing.SetActive(false);
    }

    public void TurnOnBlackAndWhitePostProcessing()
    {
        _rewindPostProcessing.SetActive(true);
    }

    public void TurnoffBlackAndWhitePostProcessing()
    {
        _rewindPostProcessing.SetActive(false);
    }
}
