using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaurensKruis.AdditionalColliders
{
    [AddComponentMenu("Additional Colliders/Rounded Box Collider")]

    public class RoundedBoxCollider : AdditionalCollider
    {
        [SerializeField]
        private Vector3 _size = Vector3.one;
        public Vector3 size { get { return _size; } set { _size = value; InvalidateShape(); } }

        [SerializeField]
        [Min(0)]
        private float _radius = 0.15f;
        public float radius { get { return _radius; } set { _radius = value; InvalidateShape(); } }

        private BoxCollider[] boxes = new BoxCollider[3];
        private CapsuleCollider[] capsules = new CapsuleCollider[12];

        protected override void Invalidate()
        {
            base.Invalidate();

            InvalidateShape();
        }

        protected void InvalidateShape()
        {
            if (!Application.isPlaying)
                return;

            void InvalidateBoxes()
            {
                void Configure(BoxCollider box, Vector3 size)
                {
                    box.center = Vector3.zero;
                    box.size = size;
                }

                float offset = _radius * 2;
                Configure(boxes[0], new Vector3(_size.x, _size.y - offset, _size.z - offset));
                Configure(boxes[1], new Vector3(_size.x - offset, _size.y, _size.z - offset));
                Configure(boxes[2], new Vector3(_size.x - offset, _size.y - offset, _size.z));
            }

            void InvalidateCapsules()
            {
                void Configure(CapsuleCollider capsule, Vector3 center, float height, float radius, int direction)
                {
                    capsule.radius = radius;
                    capsule.center = center;
                    capsule.height = height;
                    capsule.direction = direction;
                }

                Configure(capsules[0], new Vector3(0, -_size.y / 2 + _radius, -_size.z / 2 + _radius), _size.x, _radius, 0);
                Configure(capsules[1], new Vector3(0, _size.y / 2 - _radius, -_size.z / 2 + _radius), _size.x, _radius, 0);
                Configure(capsules[2], new Vector3(0, _size.y / 2 - _radius, _size.z / 2 - _radius), _size.x, _radius, 0);
                Configure(capsules[3], new Vector3(0, -_size.y / 2 + _radius, _size.z / 2 - _radius), _size.x, _radius, 0);

                Configure(capsules[4], new Vector3(-_size.x / 2 + _radius, 0, -_size.z / 2 + _radius), _size.y, _radius, 1);
                Configure(capsules[5], new Vector3(-_size.x / 2 + _radius, 0, _size.z / 2 - _radius), _size.y, _radius, 1);
                Configure(capsules[6], new Vector3(_size.x / 2 - _radius, 0, _size.z / 2 - _radius), _size.y, _radius, 1);
                Configure(capsules[7], new Vector3(_size.x / 2 - _radius, 0, _size.z / 2 - _radius), _size.y, _radius, 1);
                
                Configure(capsules[8], new Vector3(-_size.x / 2 + _radius, -_size.y / 2 + _radius, 0), _size.z, _radius, 2);
                Configure(capsules[9], new Vector3(-_size.x / 2 + _radius, _size.y / 2 - _radius, 0), _size.z, _radius, 2);
                Configure(capsules[10], new Vector3(_size.x / 2 - _radius, _size.y / 2 - _radius, 0), _size.z, _radius, 2);
                Configure(capsules[11], new Vector3(_size.x / 2 - _radius, -_size.y / 2 + _radius, 0), _size.z, _radius, 2);

            }

            InvalidateBoxes();
            InvalidateCapsules();
        }

        protected override IEnumerable<Collider> Generate()
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] != null)
                    DestroyImmediate(boxes[i]);
                
                boxes[i] = host.gameObject.AddComponent<BoxCollider>();
            }

            for(int i = 0; i < capsules.Length; i++)
            {
                if (capsules[i] != null)
                    DestroyImmediate(capsules[i]);

                capsules[i] = host.gameObject.AddComponent<CapsuleCollider>();
            }

            InvalidateShape();

            return boxes.Cast<Collider>().Concat(capsules);
        }


        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = new Color32(145, 244, 145, 192);

            float xHW = _size.x / 2f;
            float yHW = _size.y / 2f;
            float zHW = _size.z / 2f;

            void DrawWireSpheres()
            {
                //-1 -1 -1
                Gizmos.DrawWireSphere(center + new Vector3(
                    -(xHW - _radius), -(yHW - _radius), -(zHW - _radius)), _radius);

                //1 -1 -1
                Gizmos.DrawWireSphere(center + new Vector3(
                    (xHW - _radius), -(yHW - _radius), -(zHW - _radius)), _radius);

                //1 1 -1
                Gizmos.DrawWireSphere(center + new Vector3(
                    (xHW - _radius), (yHW - _radius), -(zHW - _radius)), _radius);

                //-1 1 -1
                Gizmos.DrawWireSphere(center + new Vector3(
                    -(xHW - _radius), (yHW - _radius), -(zHW - _radius)), _radius);


                //-1 -1 1
                Gizmos.DrawWireSphere(center + new Vector3(
                    -(xHW - _radius), -(yHW - _radius), (zHW - _radius)), _radius);

                //1 -1 1
                Gizmos.DrawWireSphere(center + new Vector3(
                    (xHW - _radius), -(yHW - _radius), (zHW - _radius)), _radius);

                //1 1 1
                Gizmos.DrawWireSphere(center + new Vector3(
                    (xHW - _radius), (yHW - _radius), (zHW - _radius)), _radius);

                //-1 1 1
                Gizmos.DrawWireSphere(center + new Vector3(
                    -(xHW - _radius), (yHW - _radius), (zHW - _radius)), _radius);
            }
            void DrawWireCubes()
            {
                //X
                Gizmos.DrawWireCube(center, new Vector3(xHW * 2, 2 * (yHW - _radius), 2 * (zHW - _radius)));
                //Y
                Gizmos.DrawWireCube(center, new Vector3(2 * (xHW - _radius), 2 * yHW, 2 * (zHW - _radius)));
                //Z
                Gizmos.DrawWireCube(center, new Vector3(2 * (xHW - _radius), 2 * (yHW - _radius), 2 * zHW));
            }

            DrawWireSpheres();
            DrawWireCubes();

            Gizmos.matrix = Matrix4x4.identity;
        }

    }
}
