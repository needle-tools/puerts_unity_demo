import CS from 'csharp';
import { $typeof } from 'puerts'

export class Behaviour {

    unity: CS.UnityEngine.MonoBehaviour;

    awake?(): void;
    onEnable?():void;
    onDisable?():void;
    onValidate?():void;
    start?():void;
    onDestroy?():void;
    earlyUpdate?():void;
    update?():void;
    lateUpdate?():void;
}

export class Rotate extends Behaviour {

    self: CS.PuertsTest.Rotate;
    // color : CS.UnityEngine.Color;

    awake(): void {
        this.self = this.unity as CS.PuertsTest.Rotate;
    }

    onValidate(): void {
        if(!this.self?.randomColor) this.trySetColor();
    }

    start(): void {
        console.log("START");
        this.trySetColor();
    }

    update() {
        if (!this.unity) return;
        const speed = this.self.speed * 3;
        const r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed);
        this.unity.transform.Rotate(r);

        const pos = this.unity.transform.position;
        pos.y += Math.sin(CS.UnityEngine.Time.time) * .0001;  
        this.unity.transform.position = pos;

        if (CS.UnityEngine.Time.frameCount % 60 === 0 && this.self.randomColor) this.trySetColor();
    }

    private trySetColor() {
        if (!this.unity) return;
        const renderer = this.unity.GetComponent($typeof(CS.UnityEngine.MeshRenderer)) as CS.UnityEngine.MeshRenderer;
        if (renderer) {
            const mat = renderer.sharedMaterial;
            const col = this.self.randomColor
                ? new CS.UnityEngine.Color(Math.random(), Math.random(), Math.random(), 1)
                : this.self.color;
            // console.log(mat, col);
            mat.SetColor("_Color", col);
        }
    }
} 
