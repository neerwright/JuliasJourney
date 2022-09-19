using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


[CreateAssetMenu(menuName = "State Machine/Conditions/RandomCondition")]
public class RandomConditionSO : StateConditionSO<RandomCondition>
{
	public int threshold = 50;
}

public class RandomCondition : Condition
{
	private new RandomConditionSO _originSO => (RandomConditionSO)base.OriginSO; // The SO this Condition spawned from

	public override void Awake(StateMachine stateMachine)
	{
        
	}

    protected override bool Statement()
	{
        bool targetVisible = Decide(_originSO.threshold );
        Thread.Sleep(100);
        return targetVisible;
    }

    private bool Decide(int i)
    {
        if (Random.Range(1, 100) > i)
        {
            Debug.Log("decision made");
            return true;
        }
        return false;
    }
}
