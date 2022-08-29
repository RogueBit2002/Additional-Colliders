using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.AdditionalColliders
{
    /*
    [AddComponentMenu("Additional Colliders/Rounded Plane Collider (WIP)")]
    public class RoundedPlaneCollider : AdditionalCollider
    {
        public enum Normal : int
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        [SerializeField]
        private Vector2 _size = Vector2.one;

        [SerializeField]
        private float _radius = 0.15f;


        [SerializeField]
        private Normal _normal = Normal.Y;

        CapsuleCollider[] capsules = new CapsuleCollider[4];
        BoxCollider box;

        protected override void Awake()
        {
            base.Awake();

            GenerateColliders();

            InvalidateMaterial();
        }

        protected override void Invalidate()
        {
            base.Invalidate();

            InvalidateShape();
        }

        private void InvalidateShape()
        {


            void InvalidateBox()
            {
                void Configure(BoxCollider box, Vector3 size)
                {
                    box.center = Vector3.zero;
                    box.size = size;
                }

                float offset = 2 * _radius;

                switch(_normal)
                {
                    case Normal.X:
                        Configure(box, new Vector3(offset, _size.y - offset, _size.x - offset));
                        break;
                    case Normal.Y:
                        Configure(box, new Vector3(_size.x - offset, offset, _size.y - offset));
                        break;
                    case Normal.Z:
                        Configure(box, new Vector3(_size.x - offset, _size.y - offset, offset));
                        break;
                }

                
            }

            void InvalidateCapsules()
            {

            }

            InvalidateBox();
            InvalidateCapsules();
        }

        private void GenerateColliders()
        {
            if (box != null)
                DestroyImmediate(box);

            box = host.AddComponent<BoxCollider>();

            for(int i = 0; i < capsules.Length; i++)
            {
                if(capsules[i] != null)
                    DestroyImmediate(capsules[i]);

                capsules[i] = host.AddComponent<CapsuleCollider>();
            }

            InvalidateShape();
        }
    }*/
}
