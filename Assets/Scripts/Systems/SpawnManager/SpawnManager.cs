using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.SceneManagement;
using environment;

public class SpawnManager : MonoBehaviour
{
    [Header("Asset References")]
	//[SerializeField] private InputReader _inputReader = default;
	[SerializeField] private GameObject _playerPrefab = default;

	private Vector2 _spawnLocations;
	private Transform _defaultSpawnPoint;
    private GameObject _playerInstance;
	private void Awake()
	{
		_defaultSpawnPoint = transform.GetChild(0);
        _spawnLocations = _defaultSpawnPoint.position;
	}

    public void SpawnPlayer()
	{
        var gameplayScene = SceneManager.GetSceneByName("Gameplay");
        if (gameplayScene.IsValid())
        {
            SceneManager.SetActiveScene(gameplayScene);
            _playerInstance = Instantiate(_playerPrefab, new Vector3(81.30f,74.4f,-0.3f), Quaternion.identity);
            Debug.Log("spawn Player");
            Debug.Log(_playerInstance.transform.position);

            //Camera
            ProCamera2D.Instance.AddCameraTarget(_playerInstance.transform, 1f, 1f, 0f);
            
        } 
		
        
		//TODO: Probably move this to the GameManager once it's up and running
		//_inputReader.EnableGameplayInput();
	}

    public void SpawnEnvironmentObjects()
	{
        Debug.Log("finding Obj");
        var ObjectsToSpawn = FindObjectsOfType<EnvironmentObject>();

        foreach(var envObj in ObjectsToSpawn)
        {
            Debug.Log("Insta");
            var objScript = envObj.GetComponent<EnvironmentObject>();
            var objInstance = Instantiate(objScript.prefab, objScript.locationData, Quaternion.identity);
            objInstance.GetComponent<IEnvironmentalObject>().Initialize(_playerInstance);

            envObj.gameObject.SetActive(false);
        }
    }
}
