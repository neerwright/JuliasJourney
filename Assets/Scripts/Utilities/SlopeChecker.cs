using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{

    public static class SlopeChecker
    {
        public static (bool _slopeOnRight, bool _slopeOnLeft, float _slopeSideAngle, Vector2 _slopeNormalPerp) SlopeCheckHorizontal(RaycastHit2D slopeHitFront, RaycastHit2D slopeHitBack)
            {
                bool _slopeOnRight = false;
                bool _slopeOnLeft = false;
                float _slopeSideAngle = 0f;
                Vector2 _slopeNormalPerp = new Vector2(0,0);


                if(slopeHitFront)
                {
                    _slopeOnRight = true;
                    _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
                    _slopeNormalPerp = Vector2.Perpendicular(slopeHitFront.normal).normalized;
                }
                else if(slopeHitBack)
                {
                    _slopeOnLeft = true;
                    _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
                    _slopeNormalPerp = Vector2.Perpendicular(slopeHitBack.normal).normalized;
                }
                else
                {
                    _slopeSideAngle = 0.0f;
                    _slopeOnRight = false;
                    _slopeOnLeft = false;
                }

                return (_slopeOnRight, _slopeOnLeft, _slopeSideAngle, _slopeNormalPerp);
                
            }

            public static (bool _onSlopeBothRays ,bool _onSlopeOneRay ,float _slopeDownAngle, Vector2 _slopeNormalPerp) SlopeCheckVertical(RaycastHit2D hitLeft, RaycastHit2D  hitRight)
            {
                bool _onSlopeBothRays = false;
                bool _onSlopeVertical = false;
                float _slopeDownAngle = 0; 
                Vector2 _slopeNormalPerp = new Vector2(0,0);     
        
                if (hitLeft || hitRight)
                {
                    RaycastHit2D hit;
                    if(hitLeft && hitRight) // which one is on a slope?
                    {
                        float AngleLeft = Vector2.Angle(hitLeft.normal, Vector2.up);
                        float AngleRight = Vector2.Angle(hitRight.normal, Vector2.up);

                        if( AngleLeft >= AngleRight)
                        {
                            hit = hitLeft;    
                        }
                        else
                        {
                            hit = hitRight;
                        }

                        if (AngleLeft > 0 && AngleRight > 0)
                        {
                            _onSlopeBothRays = true;
                        }
                    } 
                    else
                    {
                        hit = (hitLeft)? hitLeft : hitRight;
                    }
                    
                    _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

                    if (Mathf.Abs(_slopeDownAngle) > Mathf.Epsilon )
                    {
                        _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
                        _onSlopeVertical = true;
                    }
                    else  
                    {
                        _onSlopeVertical = false;
                        _onSlopeBothRays = false;
                    }    
                }
                else
                {
                    _onSlopeVertical = false;
                }

                return (_onSlopeBothRays, _onSlopeVertical, _slopeDownAngle, _slopeNormalPerp);
            }
    }
}