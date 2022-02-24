const speed = 30;
import CS from 'csharp';

export class Rotate {

    private bindTo: CS.MonoBehaviour;

    // constructor(){
    //     console.log('HELLO');
    // }

    constructor(bindTo: CS.MonoBehaviour) {
        console.log('HELLO BINDING');
        this.bindTo = bindTo;
        // this.bindTo.JsUpdate = () => this.onUpdate();
        // this.bindTo.JsOnDestroy = () => this.onDestroy();
        // bindTo.StartCoroutine(bindTo.Coroutine());
    }
    awake(){
        console.log("AWAKE");
    }
    onEnable(){
        console.log("ENABLED");
    }
    onDisable(){
        console.log("DISABLE");
    }
    start(){
        console.log("START");
    }

    update() {
        // console.log("UPDATE js");
        let r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed);
        this.bindTo.transform.Rotate(r);
    }

    onDestroy() {
        console.log('onDestroy');
    }
} 

// export function init(bindTo : CS.MonoBehaviour) {
//     new Rotate(bindTo);
// }
