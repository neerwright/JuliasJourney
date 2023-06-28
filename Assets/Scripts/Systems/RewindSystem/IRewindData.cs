using UnityEngine;
using System;

interface IRewindData
{
  void Rewind(RecordedData data);
  
}

public struct RecordedData
        {
            public Vector2 pos;
            public Vector2 vel;

            public double time;
            public AnimationClip clip;
        }
