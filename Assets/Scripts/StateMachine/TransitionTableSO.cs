using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTransitionTable", menuName = "State Machines/Transition Table")]
public class TransitionTableSO : ScriptableObject
{
	[SerializeField] private TransitionItem[] _transitions = default;