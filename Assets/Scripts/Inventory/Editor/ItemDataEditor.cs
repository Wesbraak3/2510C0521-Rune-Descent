using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace ItemManagementSystem {
    [CustomEditor(typeof(ItemData))]
    public class ItemDataEditor : Editor {
        private ReorderableList statList;

        private void OnEnable() {
            statList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("stats"),
                true, true, true, true) {
                drawHeaderCallback = rect => {
                    EditorGUI.LabelField(rect, "Stats");
                }
            };

            statList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = statList.serializedProperty.GetArrayElementAtIndex(index);
                var typeProp = element.FindPropertyRelative("type");
                var valueProp = element.FindPropertyRelative("value");

                rect.y += 2;
                float halfWidth = rect.width / 2;
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, halfWidth - 5, EditorGUIUtility.singleLineHeight),
                    typeProp, GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x + halfWidth + 5, rect.y, halfWidth - 5, EditorGUIUtility.singleLineHeight),
                    valueProp, GUIContent.none);
            };
        }

        public override void OnInspectorGUI() {
            ItemData item = (ItemData)target;

            //Draw default
            EditorGUILayout.LabelField("Core Info", EditorStyles.boldLabel);

            // Show the name, but not editable (comes from filename)
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Name", item.name);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.LabelField("Description");
            item.itemDescription = EditorGUILayout.TextArea(item.itemDescription, GUILayout.Height(60));
            item.itemIcon = (Sprite)EditorGUILayout.ObjectField("Icon", item.itemIcon, typeof(Sprite), false);
            item.itemModel = (GameObject)EditorGUILayout.ObjectField("Model", item.itemModel, typeof(GameObject), false);

            EditorGUILayout.Space();
            item.randomize = EditorGUILayout.Toggle("Randomise", item.randomize);
            item.value = EditorGUILayout.IntField("Value", item.value); 
            item.maxStack = Mathf.Clamp(
                EditorGUILayout.IntField("Max Stack", item.maxStack),
                1, 999
                );

            // Category
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Category", EditorStyles.boldLabel);
            item.category = (ItemCategory)EditorGUILayout.EnumPopup("Category", item.category);

            // Subtypes (conditional)
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Subtype", EditorStyles.boldLabel);

            switch (item.category) {
                case ItemCategory.Weapon:
                    item.weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", item.weaponType);
                    break;
                case ItemCategory.Armor:
                    item.armorType = (ArmorType)EditorGUILayout.EnumPopup("Armor Type", item.armorType);
                    break;
                default:
                    EditorGUILayout.LabelField("No subtype applicable for this category.");
                    break;
            }

            // Stats
            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
            statList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            // Mark dirty
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }
}