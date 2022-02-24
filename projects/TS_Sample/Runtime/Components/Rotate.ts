import CS from 'csharp';

export class Behaviour {

    unity: CS.MonoBehaviour;

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

export class Rotate extends Behaviour {

    update() {
        if(!this.unity) return;
        const speed = 30;
        const r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed);
        this.unity.transform.Rotate(r);

        const pos = this.unity.transform.position;
        pos.y += Math.sin(CS.UnityEngine.Time.time) * .003;
        this.unity.transform.position = pos;
    }
} 
