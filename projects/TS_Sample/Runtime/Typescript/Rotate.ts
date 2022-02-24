const speed = 2;
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

    update() {
        console.log("UPDATE js");
        let r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed * 100);
        this.bindTo.transform.Rotate(r);
    }

    onDestroy() {
        console.log('onDestroy...');
    }
} 

// export function init(bindTo : CS.MonoBehaviour) {
//     new Rotate(bindTo);
// }
