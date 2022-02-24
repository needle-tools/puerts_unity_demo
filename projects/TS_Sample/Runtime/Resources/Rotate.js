"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Rotate = void 0;
const speed = 30;
const csharp_1 = require("csharp");
class Rotate {
    bindTo;
    constructor(bindTo) {
        console.log('HELLO BINDING');
        this.bindTo = bindTo;
    }
    awake() {
        console.log("AWAKE");
    }
    onEnable() {
        console.log("ENABLED HELLO!!!!");
    }
    onDisable() {
        console.log("DISABLE");
    }
    start() {
        console.log("START");
    }
    update() {
        let r = csharp_1.default.UnityEngine.Vector3.op_Multiply(csharp_1.default.UnityEngine.Vector3.up, csharp_1.default.UnityEngine.Time.deltaTime * speed * 1);
        this.bindTo.transform.Rotate(r);
    }
    onDestroy() {
        console.log('onDestroy');
    }
}
exports.Rotate = Rotate;
//# sourceMappingURL=Rotate.js.map