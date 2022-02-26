import CS from 'csharp';
import { Rotate } from './Rotate';

export abstract class Editor {
    unity: CS.UnityEditor.Editor;

    awake?(): void;
    onEnable?(): void;
    onDisable?(): void;
    onInspectorGUI?(): void;

}

export class RotateEditor extends Editor {


    onInspectorGUI(): void {
        CS.UnityEditor.EditorGUILayout.LabelField("Inspector from JS 123", CS.UnityEditor.EditorStyles.boldLabel);
        const target = this.unity?.target as CS.PuertsTest.Rotate;
        if (target) {
            const sp = this.unity?.serializedObject?.FindProperty("speed");
            if (sp) {
                CS.UnityEditor.EditorGUILayout.PropertyField(sp);
                this.unity?.serializedObject?.ApplyModifiedProperties();
            }

            const rc = this.unity?.serializedObject?.FindProperty("randomColor");
            if (rc) {
                CS.UnityEditor.EditorGUILayout.PropertyField(rc);
                this.unity?.serializedObject?.ApplyModifiedProperties();
            }

            if(!target.randomColor){
                const col = this.unity?.serializedObject?.FindProperty("color");
                if (col) {
                    CS.UnityEditor.EditorGUILayout.PropertyField(col);
                    this.unity?.serializedObject?.ApplyModifiedProperties();
                }
            }
            
            const other = this.unity?.serializedObject?.FindProperty("other");
            if (other) {
                CS.UnityEditor.EditorGUILayout.PropertyField(other);
                this.unity?.serializedObject?.ApplyModifiedProperties();
            }
        }
    }
}