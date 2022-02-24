"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Rotate = exports.Behaviour = void 0;
const csharp_1 = require("csharp");
const puerts_1 = require("puerts");
class Behaviour {
    unity;
    awake() {
        console.log("AWAKE");
    }
    onEnable() {
        console.log("ENABLED");
    }
    onDisable() {
        console.log("DISABLE");
    }
    onValidate() {
    }
    start() {
        console.log("START");
    }
    onDestroy() {
        console.log('onDestroy');
    }
}
exports.Behaviour = Behaviour;
class Rotate extends Behaviour {
    self;
    awake() {
        this.self = this.unity;
    }
    onValidate() {
        if (!this.self?.randomColor)
            this.trySetColor();
    }
    start() {
        console.log("START");
        // https://github.com/chexiongsheng/puerts_unity_demo/blob/master/TsProj/UIEvent.ts
        const type = (0, puerts_1.$typeof)(csharp_1.default.UnityEngine.Transform);
        console.log(type);
        const t = this.unity.GetComponent(type);
        console.log("TRANSFORM", t);
        this.trySetColor();
    }
    update() {
        if (!this.unity)
            return;
        const speed = this.self.speed;
        const r = csharp_1.default.UnityEngine.Vector3.op_Multiply(csharp_1.default.UnityEngine.Vector3.up, csharp_1.default.UnityEngine.Time.deltaTime * speed);
        this.unity.transform.Rotate(r);
        const pos = this.unity.transform.position;
        pos.y += Math.sin(csharp_1.default.UnityEngine.Time.time) * .001;
        this.unity.transform.position = pos;
        if (csharp_1.default.UnityEngine.Time.frameCount % 60 === 0 && this.self.randomColor)
            this.trySetColor();
    }
    trySetColor() {
        if (!this.unity)
            return;
        const renderer = this.unity.GetComponent((0, puerts_1.$typeof)(csharp_1.default.UnityEngine.MeshRenderer));
        if (renderer) {
            const mat = renderer.sharedMaterial;
            const col = this.self.randomColor
                ? new csharp_1.default.UnityEngine.Color(Math.random(), Math.random(), Math.random(), 1)
                : this.self.color;
            // console.log(mat, col);
            mat.SetColor("_Color", col);
        }
    }
}
exports.Rotate = Rotate;
//# sourceMappingURL=Rotate.js.map