const speed = 30;
import CS from 'csharp';

export class Rotate {

    private bindTo: CS.MonoBehaviour;

    constructor(bindTo: CS.MonoBehaviour) {
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
        let r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed * 3);
        this.bindTo.transform.Rotate(r);

        const pos = this.bindTo.transform.position;
        pos.y += Math.sin(CS.UnityEngine.Time.time) * .003;
        this.bindTo.transform.position = pos;
    }

    onDestroy() {
        console.log('onDestroy');
    }
} 
