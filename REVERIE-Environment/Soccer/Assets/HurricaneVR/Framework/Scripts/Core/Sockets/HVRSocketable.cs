using System;
using UnityEngine;

namespace HurricaneVR.Framework.Core.Sockets
{
    [RequireComponent(typeof(HVRGrabbable))]
    public class HVRSocketable : MonoBehaviour
    {
        public HVRGrabbable Grabbable { get; private set; }
        public Transform SocketOrientation;


        [Tooltip("If your grabbable model is not at 1,1,1 scale. ")]
        public Vector3 CounterScale = Vector3.one;

        [Header("Scaling")]
        [Tooltip("If true the the renderer bounds at the time of socketing will be used to scale the object in a socket that has scaling enabled.")]
        public bool UseRendererBounds = true;

        [Tooltip("If greater than 0 and UseRendererBounds is disabled, the size used when computing socket scale when socketing into a socket with scale enabled.")]
        public float ScaleSize = 0f;

        [Tooltip("Socket scale factor applied after socketing.")]
        public float SocketScale = 1f;

        [Tooltip("Override renderer bounds when socket is scaling")]
        public BoxCollider ScaleOverride;

        [Header("SFX")]
        public AudioClip SocketedClip;
        public AudioClip UnsocketedClip;

        [Tooltip("If populated this object cannot be socketed if any of these objects are held.")]
        public HVRGrabbable[] LinkedGrabbables;

        public bool AnyLinkedGrabbablesHeld
        {
            get
            {
                if (LinkedGrabbables == null || LinkedGrabbables.Length == 0)
                    return false;

                if (LinkedGrabbables[0].IsBeingHeld)
                    return true;

                for (int i = 1; i < LinkedGrabbables.Length; i++)
                {
                    if (LinkedGrabbables[i].IsBeingHeld)
                        return true;
                }

                return false;
            }
        }

        protected void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();
        }

        public virtual float GetSocketScaleSize()
        {
            Vector3 size;

            if (ScaleOverride)
            {
                size = ScaleOverride.size;
            }
            else
            {
                if (!UseRendererBounds && ScaleSize > 0f)
                {
                    return ScaleSize;
                }

                //making sure the AABB is aligned the same every time before pulling the renderer bounds

                var rot = transform.rotation;

                transform.rotation = Quaternion.identity;

                size = Grabbable.ModelBounds.size;

                transform.rotation = rot;
            }

            var axis = size.x;

            if (size.y > axis) axis = size.y;
            if (size.z > axis) axis = size.z;

            return axis;
        }
    }
}