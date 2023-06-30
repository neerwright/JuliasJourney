using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables;

public class ArmSetup : MonoBehaviour
{

    public GameObjectVariableSO spriteArm;
	public GameObjectVariableSO frontArm;

    private const string SPRITE_ARM_TAG = "ArmsGlide";
    private const string FRONT_ARM_TAG = "PlayerFrontArm";
    // Start is called before the first frame update
    void Start()
    {
        GameObject sArm = GameObject.FindWithTag(SPRITE_ARM_TAG);
        GameObject fArm = GameObject.FindWithTag(FRONT_ARM_TAG);
        spriteArm.GObject = sArm;
        frontArm.GObject = fArm;

        sArm.SetActive(false);

    }

    void Update()
    {
    }
}
