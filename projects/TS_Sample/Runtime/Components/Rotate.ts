import CS from 'csharp';
import { $typeof } from 'puerts'

export class Behaviour {

    unity: CS.UnityEngine.MonoBehaviour;

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

export class Rotate extends Behaviour {

    self: CS.PuertsTest.Rotate;

    awake(): void {
        this.self = this.unity as CS.PuertsTest.Rotate;
    }

    onValidate(): void {
        if(!this.self?.randomColor) this.trySetColor();
    }

    start(): void {
        console.log("START");
        // https://github.com/chexiongsheng/puerts_unity_demo/blob/master/TsProj/UIEvent.ts
        const type = $typeof(CS.UnityEngine.Transform);
        console.log(type);
        const t = this.unity.GetComponent(type);
        console.log("TRANSFORM", t);
        this.trySetColor();
    }

    update() {
        if (!this.unity) return;
        const speed = this.self.speed;
        const r = CS.UnityEngine.Vector3.op_Multiply(CS.UnityEngine.Vector3.up, CS.UnityEngine.Time.deltaTime * speed);
        this.unity.transform.Rotate(r);

        const pos = this.unity.transform.position;
        pos.y += Math.sin(CS.UnityEngine.Time.time) * .001;
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
