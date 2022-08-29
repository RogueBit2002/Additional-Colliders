using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LaurensKruis.AdditionalColliders;

namespace LaurensKruis.AdditionalColliders.Editor
{
    [CustomEditor(typeof(AdditionalCollider), true)]
    public class AdditionalColliderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawProperties();
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawProperties()
        {
            SerializedProperty property = serializedObject.GetIterator();

            if (!property.NextVisible(true))
                return;

            do
            {
                if (property.propertyPath.Equals("m_Script"))
                    continue;

                EditorGUILayout.PropertyField(property);
            } while (property.NextVisible(false));

        }
    }
}
