"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Rotate = void 0;
const speed = 30;
const csharp_1 = require("csharp");
class Rotate {
    bindTo;
    // constructor(){
    //     console.log('HELLO');
    // }
    constructor(bindTo) {
        console.log('HELLO BINDING');
        this.bindTo = bindTo;
        // this.bindTo.JsUpdate = () => this.onUpdate();
        // this.bindTo.JsOnDestroy = () => this.onDestroy();
        // bindTo.StartCoroutine(bindTo.Coroutine());
    }
    awake() {
        console.log("AWAKE");
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
    update() {
        // console.log("UPDATE js");
        let r = csharp_1.default.UnityEngine.Vector3.op_Multiply(csharp_1.default.UnityEngine.Vector3.up, csharp_1.default.UnityEngine.Time.deltaTime * speed);
        this.bindTo.transform.Rotate(r);
    }
    onDestroy() {
        console.log('onDestroy');
    }
}
exports.Rotate = Rotate;
// export function init(bindTo : CS.MonoBehaviour) {
//     new Rotate(bindTo);
// }
//# sourceMappingURL=Rotate.js.map