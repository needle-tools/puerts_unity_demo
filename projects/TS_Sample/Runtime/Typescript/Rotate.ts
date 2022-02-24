const speed = 30;
import CS from 'csharp';

export class Rotate {

    private bindTo: CS.MonoBehaviour;

    constructor(bindTo: CS.MonoBehaviour) {
        console.log('HELLO BINDING');
        this.bindTo = bindTo;
    }
    awake(){
        console.log("AWAKE"); 
    }
    onEnable(){
        console.log("ENABLED HELLO!!!!");
    }
    onDisable(){
        console.log("DISABLE");
    }
    start(){
        console.log("START");
    }

    update() {
        let r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed * 1);
        this.bindTo.transform.Rotate(r);
    }

    onDestroy() {
        console.log('onDestroy');
    }
} 
