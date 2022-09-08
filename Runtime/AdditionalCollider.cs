using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaurensKruis.AdditionalColliders
{
    public abstract class AdditionalCollider : MonoBehaviour
    {
        [SerializeField]
        private bool _isTrigger;
        public bool isTrigger { get { return isTrigger; } set { _isTrigger = value; InvalidateIsTrigger(); } }

        [SerializeField]
        private PhysicMaterial _material;
        public PhysicMaterial material { get { return _material; } set { _material = value; InvalidateMaterial(); } }

        [SerializeField]
        private Vector3 _center;
        public Vector3 center { get { return _center; } set { _center = value; InvalidateCenter(); } }

        private IEnumerable<Collider> colliders;

        protected GameObject host { get; private set; }

        protected virtual void Awake()
        {
            host = new GameObject($"{gameObject.name} - Collider Host");
            host.transform.parent = transform;

            colliders = Generate();
            
            InvalidateMaterial();
            InvalidateIsTrigger();
            InvalidateCenter();
        }

        protected virtual void OnEnable()
        {
            foreach(Collider collider in colliders)
                collider.enabled = true;
        }

        protected virtual void OnDisable()
        {
            foreach (Collider collider in colliders)
                collider.enabled = false;
        }

        protected virtual void OnValidate()
        {
            if (!Application.isPlaying)
                return;

            if (host == null)
                return;
            
            Invalidate();
        }


        protected virtual void Invalidate()
        {
            InvalidateCenter();
            InvalidateMaterial();
            InvalidateIsTrigger();
        }

        protected void InvalidateCenter()
        {
            if (!Application.isPlaying)
                return;

            host.transform.localPosition = _center;
            host.transform.localRotation = Quaternion.identity;
        }

        protected void InvalidateMaterial()
        {
            if (!Application.isPlaying)
                return;

            foreach (Collider collider in colliders)
                collider.material = _material;
        }

        protected void InvalidateIsTrigger()
        {
            if (!Application.isPlaying)
                return;

            foreach(Collider collider in colliders)
                collider.isTrigger = _isTrigger;
        }

        protected abstract IEnumerable<Collider> Generate();
    }
}
