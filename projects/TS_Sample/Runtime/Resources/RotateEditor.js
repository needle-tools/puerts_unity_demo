"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RotateEditor = exports.Editor = void 0;
const csharp_1 = require("csharp");
class Editor {
    unity;
}
exports.Editor = Editor;
class RotateEditor extends Editor {
    onInspectorGUI() {
        csharp_1.default.UnityEditor.EditorGUILayout.LabelField("JS Inspector", csharp_1.default.UnityEditor.EditorStyles.boldLabel);
        const target = this.unity?.target;
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
exports.RotateEditor = RotateEditor;
//# sourceMappingURL=RotateEditor.js.map