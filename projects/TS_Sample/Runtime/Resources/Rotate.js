"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Rotate = exports.Behaviour = void 0;
const csharp_1 = require("csharp");
class Behaviour {
    unity;
    awake() {
        console.log("AWAKE1");
    }
    onEnable() {
        console.log("ENABLED");
    }
    onDisable() {
        console.log("DISABLE");
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
    update() {
        if (!this.unity)
            return;
        const speed = 30;
        const r = csharp_1.default.UnityEngine.Vector3.op_Multiply(csharp_1.default.UnityEngine.Vector3.up, csharp_1.default.UnityEngine.Time.deltaTime * speed);
        this.unity.transform.Rotate(r);
        const pos = this.unity.transform.position;
        pos.y += Math.sin(csharp_1.default.UnityEngine.Time.time) * .003;
        this.unity.transform.position = pos;
    }
}
exports.Rotate = Rotate;
//# sourceMappingURL=Rotate.js.map