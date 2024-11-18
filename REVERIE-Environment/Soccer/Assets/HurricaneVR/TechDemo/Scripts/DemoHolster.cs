using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoHolster : HVRSocket
    {
        protected override Quaternion GetRotationOffset(HVRGrabbable grabbable)
        {
            var orientation = grabbable.GetComponent<DemoHolsterOrientation>();
            if (orientation && orientation.Orientation)
                return orientation.Orientation.localRotation;
            return base.GetRotationOffset(grabbable);
        }

        protected override Vector3 GetPositionOffset(HVRGrabbable grabbable)
        {
            var orientation = grabbable.GetComponent<DemoHolsterOrientation>();
            if (orientation && orientation.Orientation)
                return orientation.Orientation.localPosition;
            return base.GetPositionOffset(grabbable);
        }
    }
}