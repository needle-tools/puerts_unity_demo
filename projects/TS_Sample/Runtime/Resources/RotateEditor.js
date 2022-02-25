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
        csharp_1.default.UnityEditor.EditorGUILayout.LabelField("JS It Compiles", csharp_1.default.UnityEditor.EditorStyles.boldLabel);
        const target = this.unity?.target;
        if (target) {
            const sp = this.unity?.serializedObject?.FindProperty("speed");
            if (sp) {
                csharp_1.default.UnityEditor.EditorGUILayout.PropertyField(sp);
                this.unity?.serializedObject?.ApplyModifiedProperties();
            }
            const rc = this.unity?.serializedObject?.FindProperty("randomColor");
            if (rc) {
                csharp_1.default.UnityEditor.EditorGUILayout.PropertyField(rc);
                this.unity?.serializedObject?.ApplyModifiedProperties();
            }
            if (!target.randomColor) {
                const col = this.unity?.serializedObject?.FindProperty("color");
                if (col) {
                    csharp_1.default.UnityEditor.EditorGUILayout.PropertyField(col);
                    this.unity?.serializedObject?.ApplyModifiedProperties();
                }
            }
        }
    }
}
exports.RotateEditor = RotateEditor;
//# sourceMappingURL=RotateEditor.js.map