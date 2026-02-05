using NUnit.Framework.Internal;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ItemManagementSystem {
    [CustomEditor(typeof(ItemData))]
    public class ItemDataEditor : Editor {
        private ReorderableList statList;

        private SerializedProperty statsProp;
        private SerializedProperty categoryProp;
        private SerializedProperty weaponTypeProp;
        private SerializedProperty armorTypeProp;
        private SerializedProperty maxStackProp;
        private SerializedProperty randomizeStatsProp;
        private SerializedProperty descriptionProp;
        private SerializedProperty gameObjectProp;
        private SerializedProperty valueProp;

        private void OnEnable() {
            statsProp = serializedObject.FindProperty("stats");
            categoryProp = serializedObject.FindProperty("category");
            weaponTypeProp = serializedObject.FindProperty("weaponType");
            armorTypeProp = serializedObject.FindProperty("armorType");
            maxStackProp = serializedObject.FindProperty("maxStack");
            randomizeStatsProp = serializedObject.FindProperty("randomizeStats");
            descriptionProp = serializedObject.FindProperty("itemDescription");
            gameObjectProp = serializedObject.FindProperty("itemModel");
            valueProp = serializedObject.FindProperty("value");

            statList = new ReorderableList(serializedObject, statsProp, true, true, true, true) {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Stats"),
                drawElementCallback = (rect, index, isActive, isFocused) => {
                    var element = statsProp.GetArrayElementAtIndex(index);
                    var typeProp = element.FindPropertyRelative("type");
                    var valueProp = element.FindPropertyRelative("value");

                    float half = rect.width / 2f - 5f;
                    rect.y += 2;
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, half, EditorGUIUtility.singleLineHeight),
                        typeProp, GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x + half + 10, rect.y, half, EditorGUIUtility.singleLineHeight),
                        valueProp, GUIContent.none);
                }
            };
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            var item = (ItemData)target;

            // Read-only name from asset filename
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Name", item.name);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(descriptionProp);

            EditorGUILayout.Space();

            // icon field
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Icon", GUILayout.Width(50));

            GUILayout.FlexibleSpace();

            item.itemIcon = (Sprite)EditorGUILayout.ObjectField(
                item.itemIcon,
                typeof(Sprite),
                false,
                GUILayout.Height(EditorGUIUtility.singleLineHeight * 4),
                GUILayout.Width(EditorGUIUtility.singleLineHeight * 4)
            );
            EditorGUILayout.EndHorizontal();

            // game model
            EditorGUILayout.PropertyField(gameObjectProp);

            EditorGUILayout.Space();

            // value of the item
            EditorGUILayout.PropertyField(valueProp);
            // Stack limit enforcement
            maxStackProp.intValue = Mathf.Clamp(
                EditorGUILayout.IntField("Max Stack", maxStackProp.intValue), 1, 999);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Category", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(categoryProp);

            // Show subtype only when needed
            var cat = (ItemCategory)categoryProp.enumValueIndex;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Subtype", EditorStyles.boldLabel);
            switch (cat) {
                case ItemCategory.Weapon:
                    EditorGUILayout.PropertyField(weaponTypeProp, new GUIContent("Subtype"));
                    EditorGUILayout.PropertyField(randomizeStatsProp);
                    break;
                case ItemCategory.Armor:
                    EditorGUILayout.PropertyField(armorTypeProp, new GUIContent("Subtype"));
                    EditorGUILayout.PropertyField(randomizeStatsProp);
                    break;
                default:
                    EditorGUILayout.HelpBox("No subtype applicable for this category.", MessageType.Info);
                    break;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
            statList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}