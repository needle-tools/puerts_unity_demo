import CS from 'csharp';
import { Rotate } from './Rotate';

export class Editor {
    unity: CS.UnityEditor.Editor;
}

export class RotateEditor extends Editor {

    onInspectorGUI() {
        CS.UnityEditor.EditorGUILayout.LabelField("JS Inspector", CS.UnityEditor.EditorStyles.boldLabel);
        const target = this.unity?.target as CS.PuertsTest.Rotate;
        if (target) {
            // const sp = this.unity?.serializedObject?.FindProperty("speed");
            // if (sp) {
            //     CS.UnityEditor.EditorGUILayout.PropertyField(sp);
            //     this.unity?.serializedObject?.ApplyModifiedProperties();
            // }
            
            // const col = this.unity?.serializedObject?.FindProperty("color");
            // if (col) {
            //     CS.UnityEditor.EditorGUILayout.PropertyField(col);
            //     this.unity?.serializedObject?.ApplyModifiedProperties();
            // }
        }
    }
}