   
declare module 'csharp' {
    import * as CSharp from 'csharp';
    export default CSharp;
}
declare module 'csharp' {
    interface $Ref<T> {
        value: T
    }
    namespace System {
        interface Array$1<T> extends System.Array {
            get_Item(index: number):T;
            set_Item(index: number, value: T):void;
        }
    }
    interface $Task<T> {}
    namespace UnityEngine {
        /** Provides an interface to get time information from Unity. */
        class Time extends System.Object
        {
        /** The time at the beginning of this frame (Read Only). */
            public static get time(): number;
            /** The double precision time at the beginning of this frame (Read Only). This is the time in seconds since the start of the game. */
            public static get timeAsDouble(): number;
            /** The time since this frame started (Read Only). This is the time in seconds since the last non-additive scene has finished loading. */
            public static get timeSinceLevelLoad(): number;
            /** The double precision time since this frame started (Read Only). This is the time in seconds since the last non-additive scene has finished loading. */
            public static get timeSinceLevelLoadAsDouble(): number;
            /** The interval in seconds from the last frame to the current one (Read Only). */
            public static get deltaTime(): number;
            /** The time since the last MonoBehaviour.FixedUpdate started (Read Only). This is the time in seconds since the start of the game. */
            public static get fixedTime(): number;
            /** The double precision time since the last MonoBehaviour.FixedUpdate started (Read Only). This is the time in seconds since the start of the game. */
            public static get fixedTimeAsDouble(): number;
            /** The timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game. */
            public static get unscaledTime(): number;
            /** The double precision timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game. */
            public static get unscaledTimeAsDouble(): number;
            /** The timeScale-independent time at the beginning of the last MonoBehaviour.FixedUpdate phase (Read Only). This is the time in seconds since the start of the game. */
            public static get fixedUnscaledTime(): number;
            /** The double precision timeScale-independent time at the beginning of the last MonoBehaviour.FixedUpdate (Read Only). This is the time in seconds since the start of the game. */
            public static get fixedUnscaledTimeAsDouble(): number;
            /** The timeScale-independent interval in seconds from the last frame to the current one (Read Only). */
            public static get unscaledDeltaTime(): number;
            /** The timeScale-independent interval in seconds from the last MonoBehaviour.FixedUpdate phase to the current one (Read Only). */
            public static get fixedUnscaledDeltaTime(): number;
            /** The interval in seconds at which physics and other fixed frame rate updates (like MonoBehaviour's MonoBehaviour.FixedUpdate) are performed. */
            public static get fixedDeltaTime(): number;
            public static set fixedDeltaTime(value: number);
            /** The maximum value of Time.deltaTime in any given frame. This is a time in seconds that limits the increase of Time.time between two frames. */
            public static get maximumDeltaTime(): number;
            public static set maximumDeltaTime(value: number);
            /** A smoothed out Time.deltaTime (Read Only). */
            public static get smoothDeltaTime(): number;
            /** The maximum time a frame can spend on particle updates. If the frame takes longer than this, then updates are split into multiple smaller updates. */
            public static get maximumParticleDeltaTime(): number;
            public static set maximumParticleDeltaTime(value: number);
            /** The scale at which time passes. */
            public static get timeScale(): number;
            public static set timeScale(value: number);
            /** The total number of frames since the start of the game (Read Only). */
            public static get frameCount(): number;
            public static get renderedFrameCount(): number;
            /** The real time in seconds since the game started (Read Only). */
            public static get realtimeSinceStartup(): number;
            /** The real time in seconds since the game started (Read Only). Double precision version of Time.realtimeSinceStartup.  */
            public static get realtimeSinceStartupAsDouble(): number;
            /** Slows your application’s playback time to allow Unity to save screenshots in between frames. */
            public static get captureDeltaTime(): number;
            public static set captureDeltaTime(value: number);
            /** The reciprocal of Time.captureDeltaTime. */
            public static get captureFramerate(): number;
            public static set captureFramerate(value: number);
            /** Returns true if called inside a fixed time step callback (like MonoBehaviour's MonoBehaviour.FixedUpdate), otherwise returns false. */
            public static get inFixedTimeStep(): boolean;
            public constructor ()
        }
        /** Base class for all entities in Unity Scenes. */
        class GameObject extends UnityEngine.Object
        {
        /** The Transform attached to this GameObject. */
            public get transform(): UnityEngine.Transform;
            /** The layer the game object is in. */
            public get layer(): number;
            public set layer(value: number);
            /** The local active state of this GameObject. (Read Only) */
            public get activeSelf(): boolean;
            /** Defines whether the GameObject is active in the Scene. */
            public get activeInHierarchy(): boolean;
            /** Gets and sets the GameObject's StaticEditorFlags. */
            public get isStatic(): boolean;
            public set isStatic(value: boolean);
            /** The tag of this game object. */
            public get tag(): string;
            public set tag(value: string);
            /** Scene that the GameObject is part of. */
            public get scene(): UnityEngine.SceneManagement.Scene;
            /** Scene culling mask Unity uses to determine which scene to render the GameObject in. */
            public get sceneCullingMask(): bigint;
            public get gameObject(): UnityEngine.GameObject;
            /** Creates a game object with a primitive mesh renderer and appropriate collider. * @param type The type of primitive object to create.
            */
            public static CreatePrimitive ($type: UnityEngine.PrimitiveType) : UnityEngine.GameObject
            /** Returns the component of Type type if the game object has one attached, null if it doesn't. * @param type The type of Component to retrieve.
            */
            public GetComponent ($type: System.Type) : UnityEngine.Component
            /** Returns the component with name type if the game object has one attached, null if it doesn't. * @param type The type of Component to retrieve.
            */
            public GetComponent ($type: string) : UnityEngine.Component
            /** Returns the component of Type type in the GameObject or any of its children using depth first search.
            * @param type The type of Component to retrieve.
            * @returns A component of the matching type, if found. 
            */
            public GetComponentInChildren ($type: System.Type, $includeInactive: boolean) : UnityEngine.Component
            /** Returns the component of Type type in the GameObject or any of its children using depth first search.
            * @param type The type of Component to retrieve.
            * @returns A component of the matching type, if found. 
            */
            public GetComponentInChildren ($type: System.Type) : UnityEngine.Component
            /** Retrieves the component of Type type in the GameObject or any of its parents.
            * @param type Type of component to find.
            * @returns Returns a component if a component matching the type is found. Returns null otherwise. 
            */
            public GetComponentInParent ($type: System.Type, $includeInactive: boolean) : UnityEngine.Component
            /** Retrieves the component of Type type in the GameObject or any of its parents.
            * @param type Type of component to find.
            * @returns Returns a component if a component matching the type is found. Returns null otherwise. 
            */
            public GetComponentInParent ($type: System.Type) : UnityEngine.Component
            /** Returns all components of Type type in the GameObject. * @param type The type of component to retrieve.
            */
            public GetComponents ($type: System.Type) : System.Array$1<UnityEngine.Component>
            public GetComponents ($type: System.Type, $results: System.Collections.Generic.List$1<UnityEngine.Component>) : void
            /** Returns all components of Type type in the GameObject or any of its children. * @param type The type of Component to retrieve.
            * @param includeInactive Should Components on inactive GameObjects be included in the found set?
            */
            public GetComponentsInChildren ($type: System.Type) : System.Array$1<UnityEngine.Component>
            /** Returns all components of Type type in the GameObject or any of its children. * @param type The type of Component to retrieve.
            * @param includeInactive Should Components on inactive GameObjects be included in the found set?
            */
            public GetComponentsInChildren ($type: System.Type, $includeInactive: boolean) : System.Array$1<UnityEngine.Component>
            public GetComponentsInParent ($type: System.Type) : System.Array$1<UnityEngine.Component>
            /** Returns all components of Type type in the GameObject or any of its parents. * @param type The type of Component to retrieve.
            * @param includeInactive Should inactive Components be included in the found set?
            */
            public GetComponentsInParent ($type: System.Type, $includeInactive: boolean) : System.Array$1<UnityEngine.Component>
            /** Gets the component of the specified type, if it exists.
            * @param type The type of component to retrieve.
            * @param component The output argument that will contain the component or null.
            * @returns Returns true if the component is found, false otherwise. 
            */
            public TryGetComponent ($type: System.Type, $component: $Ref<UnityEngine.Component>) : boolean
            /** Returns one active GameObject tagged tag. Returns null if no GameObject was found. * @param tag The tag to search for.
            */
            public static FindWithTag ($tag: string) : UnityEngine.GameObject
            public SendMessageUpwards ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            public SendMessage ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            public BroadcastMessage ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            /** Adds a component class of type componentType to the game object. C# Users can use a generic version. */
            public AddComponent ($componentType: System.Type) : UnityEngine.Component
            /** ActivatesDeactivates the GameObject, depending on the given true or false/ value. * @param value Activate or deactivate the object, where true activates the GameObject and false deactivates the GameObject.
            */
            public SetActive ($value: boolean) : void
            /** Is this game object tagged with tag ? * @param tag The tag to compare.
            */
            public CompareTag ($tag: string) : boolean
            public static FindGameObjectWithTag ($tag: string) : UnityEngine.GameObject
            /** Returns an array of active GameObjects tagged tag. Returns empty array if no GameObject was found. * @param tag The name of the tag to search GameObjects for.
            */
            public static FindGameObjectsWithTag ($tag: string) : System.Array$1<UnityEngine.GameObject>
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessageUpwards ($methodName: string, $value: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessageUpwards ($methodName: string, $value: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessageUpwards ($methodName: string) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessage ($methodName: string, $value: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessage ($methodName: string, $value: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName The name of the method to call.
            * @param value An optional parameter value to pass to the called method.
            * @param options Should an error be raised if the method doesn't exist on the target object?
            */
            public SendMessage ($methodName: string) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. */
            public BroadcastMessage ($methodName: string, $parameter: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. */
            public BroadcastMessage ($methodName: string, $parameter: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. */
            public BroadcastMessage ($methodName: string) : void
            /** Finds a GameObject by name and returns it. */
            public static Find ($name: string) : UnityEngine.GameObject
            public constructor ($name: string)
            public constructor ()
            public constructor ($name: string, ...components: System.Type[])
        }
        /** Base class for all objects Unity can reference. */
        class Object extends System.Object
        {
        }
        /** The various primitives that can be created using the GameObject.CreatePrimitive function. */
        enum PrimitiveType
        { Sphere = 0, Capsule = 1, Cylinder = 2, Cube = 3, Plane = 4, Quad = 5 }
        /** Base class for everything attached to GameObjects. */
        class Component extends UnityEngine.Object
        {
        /** The Transform attached to this GameObject. */
            public get transform(): UnityEngine.Transform;
            /** The game object this component is attached to. A component is always attached to a game object. */
            public get gameObject(): UnityEngine.GameObject;
            /** The tag of this game object. */
            public get tag(): string;
            public set tag(value: string);
            /** Returns the component of Type type if the GameObject has one attached, null if it doesn't. Will also return disabled components. * @param type The type of Component to retrieve.
            */
            public GetComponent ($type: System.Type) : UnityEngine.Component
            /** Gets the component of the specified type, if it exists.
            * @param type The type of the component to retrieve.
            * @param component The output argument that will contain the component or null.
            * @returns Returns true if the component is found, false otherwise. 
            */
            public TryGetComponent ($type: System.Type, $component: $Ref<UnityEngine.Component>) : boolean
            /** Returns the component with name type if the GameObject has one attached, null if it doesn't. */
            public GetComponent ($type: string) : UnityEngine.Component
            public GetComponentInChildren ($t: System.Type, $includeInactive: boolean) : UnityEngine.Component
            /** Returns the component of Type type in the GameObject or any of its children using depth first search.
            * @param t The type of Component to retrieve.
            * @returns A component of the matching type, if found. 
            */
            public GetComponentInChildren ($t: System.Type) : UnityEngine.Component
            /** Returns all components of Type type in the GameObject or any of its children. Works recursively. * @param t The type of Component to retrieve.
            * @param includeInactive Should Components on inactive GameObjects be included in the found set? includeInactive decides which children of the GameObject will be searched.  The GameObject that you call GetComponentsInChildren on is always searched regardless. Default is false.
            */
            public GetComponentsInChildren ($t: System.Type, $includeInactive: boolean) : System.Array$1<UnityEngine.Component>
            public GetComponentsInChildren ($t: System.Type) : System.Array$1<UnityEngine.Component>
            /** Returns the component of Type type in the GameObject or any of its parents.
            * @param t The type of Component to retrieve.
            * @returns A component of the matching type, if found. 
            */
            public GetComponentInParent ($t: System.Type) : UnityEngine.Component
            /** Returns all components of Type type in the GameObject or any of its parents. * @param t The type of Component to retrieve.
            * @param includeInactive Should inactive Components be included in the found set?
            */
            public GetComponentsInParent ($t: System.Type, $includeInactive: boolean) : System.Array$1<UnityEngine.Component>
            public GetComponentsInParent ($t: System.Type) : System.Array$1<UnityEngine.Component>
            /** Returns all components of Type type in the GameObject. * @param type The type of Component to retrieve.
            */
            public GetComponents ($type: System.Type) : System.Array$1<UnityEngine.Component>
            public GetComponents ($type: System.Type, $results: System.Collections.Generic.List$1<UnityEngine.Component>) : void
            /** Is this game object tagged with tag ? * @param tag The tag to compare.
            */
            public CompareTag ($tag: string) : boolean
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName Name of method to call.
            * @param value Optional parameter value for the method.
            * @param options Should an error be raised if the method does not exist on the target object?
            */
            public SendMessageUpwards ($methodName: string, $value: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName Name of method to call.
            * @param value Optional parameter value for the method.
            * @param options Should an error be raised if the method does not exist on the target object?
            */
            public SendMessageUpwards ($methodName: string, $value: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName Name of method to call.
            * @param value Optional parameter value for the method.
            * @param options Should an error be raised if the method does not exist on the target object?
            */
            public SendMessageUpwards ($methodName: string) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour. * @param methodName Name of method to call.
            * @param value Optional parameter value for the method.
            * @param options Should an error be raised if the method does not exist on the target object?
            */
            public SendMessageUpwards ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName Name of the method to call.
            * @param value Optional parameter for the method.
            * @param options Should an error be raised if the target object doesn't implement the method for the message?
            */
            public SendMessage ($methodName: string, $value: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName Name of the method to call.
            * @param value Optional parameter for the method.
            * @param options Should an error be raised if the target object doesn't implement the method for the message?
            */
            public SendMessage ($methodName: string) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName Name of the method to call.
            * @param value Optional parameter for the method.
            * @param options Should an error be raised if the target object doesn't implement the method for the message?
            */
            public SendMessage ($methodName: string, $value: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object. * @param methodName Name of the method to call.
            * @param value Optional parameter for the method.
            * @param options Should an error be raised if the target object doesn't implement the method for the message?
            */
            public SendMessage ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. * @param methodName Name of the method to call.
            * @param parameter Optional parameter to pass to the method (can be any value).
            * @param options Should an error be raised if the method does not exist for a given target object?
            */
            public BroadcastMessage ($methodName: string, $parameter: any, $options: UnityEngine.SendMessageOptions) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. * @param methodName Name of the method to call.
            * @param parameter Optional parameter to pass to the method (can be any value).
            * @param options Should an error be raised if the method does not exist for a given target object?
            */
            public BroadcastMessage ($methodName: string, $parameter: any) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. * @param methodName Name of the method to call.
            * @param parameter Optional parameter to pass to the method (can be any value).
            * @param options Should an error be raised if the method does not exist for a given target object?
            */
            public BroadcastMessage ($methodName: string) : void
            /** Calls the method named methodName on every MonoBehaviour in this game object or any of its children. * @param methodName Name of the method to call.
            * @param parameter Optional parameter to pass to the method (can be any value).
            * @param options Should an error be raised if the method does not exist for a given target object?
            */
            public BroadcastMessage ($methodName: string, $options: UnityEngine.SendMessageOptions) : void
            public constructor ()
        }
        /** Options for how to send a message. */
        enum SendMessageOptions
        { RequireReceiver = 0, DontRequireReceiver = 1 }
        /** Position, rotation and scale of an object. */
        class Transform extends UnityEngine.Component implements System.Collections.IEnumerable
        {
        /** The world space position of the Transform. */
            public get position(): UnityEngine.Vector3;
            public set position(value: UnityEngine.Vector3);
            /** Position of the transform relative to the parent transform. */
            public get localPosition(): UnityEngine.Vector3;
            public set localPosition(value: UnityEngine.Vector3);
            /** The rotation as Euler angles in degrees. */
            public get eulerAngles(): UnityEngine.Vector3;
            public set eulerAngles(value: UnityEngine.Vector3);
            /** The rotation as Euler angles in degrees relative to the parent transform's rotation. */
            public get localEulerAngles(): UnityEngine.Vector3;
            public set localEulerAngles(value: UnityEngine.Vector3);
            /** The red axis of the transform in world space. */
            public get right(): UnityEngine.Vector3;
            public set right(value: UnityEngine.Vector3);
            /** The green axis of the transform in world space. */
            public get up(): UnityEngine.Vector3;
            public set up(value: UnityEngine.Vector3);
            /** Returns a normalized vector representing the blue axis of the transform in world space. */
            public get forward(): UnityEngine.Vector3;
            public set forward(value: UnityEngine.Vector3);
            /** A Quaternion that stores the rotation of the Transform in world space. */
            public get rotation(): UnityEngine.Quaternion;
            public set rotation(value: UnityEngine.Quaternion);
            /** The rotation of the transform relative to the transform rotation of the parent. */
            public get localRotation(): UnityEngine.Quaternion;
            public set localRotation(value: UnityEngine.Quaternion);
            /** The scale of the transform relative to the GameObjects parent. */
            public get localScale(): UnityEngine.Vector3;
            public set localScale(value: UnityEngine.Vector3);
            /** The parent of the transform. */
            public get parent(): UnityEngine.Transform;
            public set parent(value: UnityEngine.Transform);
            /** Matrix that transforms a point from world space into local space (Read Only). */
            public get worldToLocalMatrix(): UnityEngine.Matrix4x4;
            /** Matrix that transforms a point from local space into world space (Read Only). */
            public get localToWorldMatrix(): UnityEngine.Matrix4x4;
            /** Returns the topmost transform in the hierarchy. */
            public get root(): UnityEngine.Transform;
            /** The number of children the parent Transform has. */
            public get childCount(): number;
            /** The global scale of the object (Read Only). */
            public get lossyScale(): UnityEngine.Vector3;
            /** Has the transform changed since the last time the flag was set to 'false'? */
            public get hasChanged(): boolean;
            public set hasChanged(value: boolean);
            /** The transform capacity of the transform's hierarchy data structure. */
            public get hierarchyCapacity(): number;
            public set hierarchyCapacity(value: number);
            /** The number of transforms in the transform's hierarchy data structure. */
            public get hierarchyCount(): number;
            /** Set the parent of the transform. * @param parent The parent Transform to use.
            * @param worldPositionStays If true, the parent-relative position, scale and rotation are modified such that the object keeps the same world space position, rotation and scale as before.
            */
            public SetParent ($p: UnityEngine.Transform) : void
            /** Set the parent of the transform. * @param parent The parent Transform to use.
            * @param worldPositionStays If true, the parent-relative position, scale and rotation are modified such that the object keeps the same world space position, rotation and scale as before.
            */
            public SetParent ($parent: UnityEngine.Transform, $worldPositionStays: boolean) : void
            /** Sets the world space position and rotation of the Transform component. */
            public SetPositionAndRotation ($position: UnityEngine.Vector3, $rotation: UnityEngine.Quaternion) : void
            /** Moves the transform in the direction and distance of translation. */
            public Translate ($translation: UnityEngine.Vector3, $relativeTo: UnityEngine.Space) : void
            /** Moves the transform in the direction and distance of translation. */
            public Translate ($translation: UnityEngine.Vector3) : void
            /** Moves the transform by x along the x axis, y along the y axis, and z along the z axis. */
            public Translate ($x: number, $y: number, $z: number, $relativeTo: UnityEngine.Space) : void
            /** Moves the transform by x along the x axis, y along the y axis, and z along the z axis. */
            public Translate ($x: number, $y: number, $z: number) : void
            /** Moves the transform in the direction and distance of translation. */
            public Translate ($translation: UnityEngine.Vector3, $relativeTo: UnityEngine.Transform) : void
            /** Moves the transform by x along the x axis, y along the y axis, and z along the z axis. */
            public Translate ($x: number, $y: number, $z: number, $relativeTo: UnityEngine.Transform) : void
            /** Applies a rotation of eulerAngles.z degrees around the z-axis, eulerAngles.x degrees around the x-axis, and eulerAngles.y degrees around the y-axis (in that order). * @param eulers The rotation to apply in euler angles.
            * @param relativeTo Determines whether to rotate the GameObject either locally to  the GameObject or relative to the Scene in world space.
            */
            public Rotate ($eulers: UnityEngine.Vector3, $relativeTo: UnityEngine.Space) : void
            /** Applies a rotation of eulerAngles.z degrees around the z-axis, eulerAngles.x degrees around the x-axis, and eulerAngles.y degrees around the y-axis (in that order). * @param eulers The rotation to apply in euler angles.
            */
            public Rotate ($eulers: UnityEngine.Vector3) : void
            /** The implementation of this method applies a rotation of zAngle degrees around the z axis, xAngle degrees around the x axis, and yAngle degrees around the y axis (in that order). * @param relativeTo Determines whether to rotate the GameObject either locally to the GameObject or relative to the Scene in world space.
            * @param xAngle Degrees to rotate the GameObject around the X axis.
            * @param yAngle Degrees to rotate the GameObject around the Y axis.
            * @param zAngle Degrees to rotate the GameObject around the Z axis.
            */
            public Rotate ($xAngle: number, $yAngle: number, $zAngle: number, $relativeTo: UnityEngine.Space) : void
            /** The implementation of this method applies a rotation of zAngle degrees around the z axis, xAngle degrees around the x axis, and yAngle degrees around the y axis (in that order). * @param xAngle Degrees to rotate the GameObject around the X axis.
            * @param yAngle Degrees to rotate the GameObject around the Y axis.
            * @param zAngle Degrees to rotate the GameObject around the Z axis.
            */
            public Rotate ($xAngle: number, $yAngle: number, $zAngle: number) : void
            /** Rotates the object around the given axis by the number of degrees defined by the given angle. * @param angle The degrees of rotation to apply.
            * @param axis The axis to apply rotation to.
            * @param relativeTo Determines whether to rotate the GameObject either locally to the GameObject or relative to the Scene in world space.
            */
            public Rotate ($axis: UnityEngine.Vector3, $angle: number, $relativeTo: UnityEngine.Space) : void
            /** Rotates the object around the given axis by the number of degrees defined by the given angle. * @param axis The axis to apply rotation to.
            * @param angle The degrees of rotation to apply.
            */
            public Rotate ($axis: UnityEngine.Vector3, $angle: number) : void
            /** Rotates the transform about axis passing through point in world coordinates by angle degrees. */
            public RotateAround ($point: UnityEngine.Vector3, $axis: UnityEngine.Vector3, $angle: number) : void
            /** Rotates the transform so the forward vector points at target's current position. * @param target Object to point towards.
            * @param worldUp Vector specifying the upward direction.
            */
            public LookAt ($target: UnityEngine.Transform, $worldUp: UnityEngine.Vector3) : void
            /** Rotates the transform so the forward vector points at target's current position. * @param target Object to point towards.
            * @param worldUp Vector specifying the upward direction.
            */
            public LookAt ($target: UnityEngine.Transform) : void
            /** Rotates the transform so the forward vector points at worldPosition. * @param worldPosition Point to look at.
            * @param worldUp Vector specifying the upward direction.
            */
            public LookAt ($worldPosition: UnityEngine.Vector3, $worldUp: UnityEngine.Vector3) : void
            /** Rotates the transform so the forward vector points at worldPosition. * @param worldPosition Point to look at.
            * @param worldUp Vector specifying the upward direction.
            */
            public LookAt ($worldPosition: UnityEngine.Vector3) : void
            /** Transforms direction from local space to world space. */
            public TransformDirection ($direction: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms direction x, y, z from local space to world space. */
            public TransformDirection ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            /** Transforms a direction from world space to local space. The opposite of Transform.TransformDirection. */
            public InverseTransformDirection ($direction: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms the direction x, y, z from world space to local space. The opposite of Transform.TransformDirection. */
            public InverseTransformDirection ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            /** Transforms vector from local space to world space. */
            public TransformVector ($vector: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms vector x, y, z from local space to world space. */
            public TransformVector ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            /** Transforms a vector from world space to local space. The opposite of Transform.TransformVector. */
            public InverseTransformVector ($vector: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms the vector x, y, z from world space to local space. The opposite of Transform.TransformVector. */
            public InverseTransformVector ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            /** Transforms position from local space to world space. */
            public TransformPoint ($position: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms the position x, y, z from local space to world space. */
            public TransformPoint ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            /** Transforms position from world space to local space. */
            public InverseTransformPoint ($position: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Transforms the position x, y, z from world space to local space. The opposite of Transform.TransformPoint. */
            public InverseTransformPoint ($x: number, $y: number, $z: number) : UnityEngine.Vector3
            public DetachChildren () : void
            public SetAsFirstSibling () : void
            public SetAsLastSibling () : void
            /** Sets the sibling index. * @param index Index to set.
            */
            public SetSiblingIndex ($index: number) : void
            public GetSiblingIndex () : number
            /** Finds a child by n and returns it.
            * @param n Name of child to be found.
            * @returns The returned child transform or null if no child is found. 
            */
            public Find ($n: string) : UnityEngine.Transform
            /** Is this transform a child of parent? */
            public IsChildOf ($parent: UnityEngine.Transform) : boolean
            public GetEnumerator () : System.Collections.IEnumerator
            /** Returns a transform child by index.
            * @param index Index of the child transform to return. Must be smaller than Transform.childCount.
            * @returns Transform child by index. 
            */
            public GetChild ($index: number) : UnityEngine.Transform
        }
        /** MonoBehaviour is the base class from which every Unity script derives. */
        class MonoBehaviour extends UnityEngine.Behaviour
        {
        /** Disabling this lets you skip the GUI layout phase. */
            public get useGUILayout(): boolean;
            public set useGUILayout(value: boolean);
            /** Allow a specific instance of a MonoBehaviour to run in edit mode (only available in the editor). */
            public get runInEditMode(): boolean;
            public set runInEditMode(value: boolean);
            public IsInvoking () : boolean
            public CancelInvoke () : void
            /** Invokes the method methodName in time seconds. */
            public Invoke ($methodName: string, $time: number) : void
            /** Invokes the method methodName in time seconds, then repeatedly every repeatRate seconds. */
            public InvokeRepeating ($methodName: string, $time: number, $repeatRate: number) : void
            /** Cancels all Invoke calls with name methodName on this behaviour. */
            public CancelInvoke ($methodName: string) : void
            /** Is any invoke on methodName pending? */
            public IsInvoking ($methodName: string) : boolean
            /** Starts a coroutine named methodName. */
            public StartCoroutine ($methodName: string) : UnityEngine.Coroutine
            /** Starts a coroutine named methodName. */
            public StartCoroutine ($methodName: string, $value: any) : UnityEngine.Coroutine
            /** Starts a Coroutine. */
            public StartCoroutine ($routine: System.Collections.IEnumerator) : UnityEngine.Coroutine
            /** Stops the first coroutine named methodName, or the coroutine stored in routine running on this behaviour. * @param methodName Name of coroutine.
            * @param routine Name of the function in code, including coroutines.
            */
            public StopCoroutine ($routine: System.Collections.IEnumerator) : void
            /** Stops the first coroutine named methodName, or the coroutine stored in routine running on this behaviour. * @param methodName Name of coroutine.
            * @param routine Name of the function in code, including coroutines.
            */
            public StopCoroutine ($routine: UnityEngine.Coroutine) : void
            /** Stops the first coroutine named methodName, or the coroutine stored in routine running on this behaviour. * @param methodName Name of coroutine.
            * @param routine Name of the function in code, including coroutines.
            */
            public StopCoroutine ($methodName: string) : void
            public StopAllCoroutines () : void
            /** Logs message to the Unity Console (identical to Debug.Log). */
            public static print ($message: any) : void
            public constructor ()
        }
        /** Behaviours are Components that can be enabled or disabled. */
        class Behaviour extends UnityEngine.Component
        {
        }
        /** MonoBehaviour.StartCoroutine returns a Coroutine. Instances of this class are only used to reference these coroutines, and do not hold any exposed properties or functions. */
        class Coroutine extends UnityEngine.YieldInstruction
        {
        }
        /** Base class for all yield instructions. */
        class YieldInstruction extends System.Object
        {
        }
        /** Representation of 3D vectors and points. */
        class Vector3 extends System.ValueType implements System.IEquatable$1<UnityEngine.Vector3>, System.IFormattable
        {
            public static kEpsilon : number
            public static kEpsilonNormalSqrt : number/** X component of the vector. */
            public x : number/** Y component of the vector. */
            public y : number/** Z component of the vector. */
            public z : number/** Returns this vector with a magnitude of 1 (Read Only). */
            public get normalized(): UnityEngine.Vector3;
            /** Returns the length of this vector (Read Only). */
            public get magnitude(): number;
            /** Returns the squared length of this vector (Read Only). */
            public get sqrMagnitude(): number;
            /** Shorthand for writing Vector3(0, 0, 0). */
            public static get zero(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(1, 1, 1). */
            public static get one(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(0, 0, 1). */
            public static get forward(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(0, 0, -1). */
            public static get back(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(0, 1, 0). */
            public static get up(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(0, -1, 0). */
            public static get down(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(-1, 0, 0). */
            public static get left(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(1, 0, 0). */
            public static get right(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity). */
            public static get positiveInfinity(): UnityEngine.Vector3;
            /** Shorthand for writing Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity). */
            public static get negativeInfinity(): UnityEngine.Vector3;
            /** Spherically interpolates between two vectors. */
            public static Slerp ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3, $t: number) : UnityEngine.Vector3
            /** Spherically interpolates between two vectors. */
            public static SlerpUnclamped ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3, $t: number) : UnityEngine.Vector3
            /** Makes vectors normalized and orthogonal to each other. */
            public static OrthoNormalize ($normal: $Ref<UnityEngine.Vector3>, $tangent: $Ref<UnityEngine.Vector3>) : void
            /** Makes vectors normalized and orthogonal to each other. */
            public static OrthoNormalize ($normal: $Ref<UnityEngine.Vector3>, $tangent: $Ref<UnityEngine.Vector3>, $binormal: $Ref<UnityEngine.Vector3>) : void
            /** Rotates a vector current towards target.
            * @param current The vector being managed.
            * @param target The vector.
            * @param maxRadiansDelta The maximum angle in radians allowed for this rotation.
            * @param maxMagnitudeDelta The maximum allowed change in vector magnitude for this rotation.
            * @returns The location that RotateTowards generates. 
            */
            public static RotateTowards ($current: UnityEngine.Vector3, $target: UnityEngine.Vector3, $maxRadiansDelta: number, $maxMagnitudeDelta: number) : UnityEngine.Vector3
            /** Linearly interpolates between two points.
            * @param a Start value, returned when t = 0.
            * @param b End value, returned when t = 1.
            * @param t Value used to interpolate between a and b.
            * @returns Interpolated value, equals to a + (b - a) * t. 
            */
            public static Lerp ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3, $t: number) : UnityEngine.Vector3
            /** Linearly interpolates between two vectors. */
            public static LerpUnclamped ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3, $t: number) : UnityEngine.Vector3
            /** Calculate a position between the points specified by current and target, moving no farther than the distance specified by maxDistanceDelta.
            * @param current The position to move from.
            * @param target The position to move towards.
            * @param maxDistanceDelta Distance to move current per call.
            * @returns The new position. 
            */
            public static MoveTowards ($current: UnityEngine.Vector3, $target: UnityEngine.Vector3, $maxDistanceDelta: number) : UnityEngine.Vector3
            /** Gradually changes a vector towards a desired goal over time. * @param current The current position.
            * @param target The position we are trying to reach.
            * @param currentVelocity The current velocity, this value is modified by the function every time you call it.
            * @param smoothTime Approximately the time it will take to reach the target. A smaller value will reach the target faster.
            * @param maxSpeed Optionally allows you to clamp the maximum speed.
            * @param deltaTime The time since the last call to this function. By default Time.deltaTime.
            */
            public static SmoothDamp ($current: UnityEngine.Vector3, $target: UnityEngine.Vector3, $currentVelocity: $Ref<UnityEngine.Vector3>, $smoothTime: number, $maxSpeed: number) : UnityEngine.Vector3
            /** Gradually changes a vector towards a desired goal over time. * @param current The current position.
            * @param target The position we are trying to reach.
            * @param currentVelocity The current velocity, this value is modified by the function every time you call it.
            * @param smoothTime Approximately the time it will take to reach the target. A smaller value will reach the target faster.
            * @param maxSpeed Optionally allows you to clamp the maximum speed.
            * @param deltaTime The time since the last call to this function. By default Time.deltaTime.
            */
            public static SmoothDamp ($current: UnityEngine.Vector3, $target: UnityEngine.Vector3, $currentVelocity: $Ref<UnityEngine.Vector3>, $smoothTime: number) : UnityEngine.Vector3
            /** Gradually changes a vector towards a desired goal over time. * @param current The current position.
            * @param target The position we are trying to reach.
            * @param currentVelocity The current velocity, this value is modified by the function every time you call it.
            * @param smoothTime Approximately the time it will take to reach the target. A smaller value will reach the target faster.
            * @param maxSpeed Optionally allows you to clamp the maximum speed.
            * @param deltaTime The time since the last call to this function. By default Time.deltaTime.
            */
            public static SmoothDamp ($current: UnityEngine.Vector3, $target: UnityEngine.Vector3, $currentVelocity: $Ref<UnityEngine.Vector3>, $smoothTime: number, $maxSpeed: number, $deltaTime: number) : UnityEngine.Vector3
            public get_Item ($index: number) : number
            public set_Item ($index: number, $value: number) : void
            /** Set x, y and z components of an existing Vector3. */
            public Set ($newX: number, $newY: number, $newZ: number) : void
            /** Multiplies two vectors component-wise. */
            public static Scale ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Multiplies every component of this vector by the same component of scale. */
            public Scale ($scale: UnityEngine.Vector3) : void
            /** Cross Product of two vectors. */
            public static Cross ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Returns true if the given vector is exactly equal to this vector. */
            public Equals ($other: any) : boolean
            public Equals ($other: UnityEngine.Vector3) : boolean
            /** Reflects a vector off the plane defined by a normal. */
            public static Reflect ($inDirection: UnityEngine.Vector3, $inNormal: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Makes this vector have a magnitude of 1. */
            public static Normalize ($value: UnityEngine.Vector3) : UnityEngine.Vector3
            public Normalize () : void
            /** Dot Product of two vectors. */
            public static Dot ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : number
            /** Projects a vector onto another vector. */
            public static Project ($vector: UnityEngine.Vector3, $onNormal: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Projects a vector onto a plane defined by a normal orthogonal to the plane.
            * @param planeNormal The direction from the vector towards the plane.
            * @param vector The location of the vector above the plane.
            * @returns The location of the vector on the plane. 
            */
            public static ProjectOnPlane ($vector: UnityEngine.Vector3, $planeNormal: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Returns the angle in degrees between from and to.
            * @param from The vector from which the angular difference is measured.
            * @param to The vector to which the angular difference is measured.
            * @returns The angle in degrees between the two vectors. 
            */
            public static Angle ($from: UnityEngine.Vector3, $to: UnityEngine.Vector3) : number
            /** Returns the signed angle in degrees between from and to. * @param from The vector from which the angular difference is measured.
            * @param to The vector to which the angular difference is measured.
            * @param axis A vector around which the other vectors are rotated.
            */
            public static SignedAngle ($from: UnityEngine.Vector3, $to: UnityEngine.Vector3, $axis: UnityEngine.Vector3) : number
            /** Returns the distance between a and b. */
            public static Distance ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3) : number
            /** Returns a copy of vector with its magnitude clamped to maxLength. */
            public static ClampMagnitude ($vector: UnityEngine.Vector3, $maxLength: number) : UnityEngine.Vector3
            public static Magnitude ($vector: UnityEngine.Vector3) : number
            public static SqrMagnitude ($vector: UnityEngine.Vector3) : number
            /** Returns a vector that is made from the smallest components of two vectors. */
            public static Min ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : UnityEngine.Vector3
            /** Returns a vector that is made from the largest components of two vectors. */
            public static Max ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : UnityEngine.Vector3
            public static op_Addition ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3) : UnityEngine.Vector3
            public static op_Subtraction ($a: UnityEngine.Vector3, $b: UnityEngine.Vector3) : UnityEngine.Vector3
            public static op_UnaryNegation ($a: UnityEngine.Vector3) : UnityEngine.Vector3
            public static op_Multiply ($a: UnityEngine.Vector3, $d: number) : UnityEngine.Vector3
            public static op_Multiply ($d: number, $a: UnityEngine.Vector3) : UnityEngine.Vector3
            public static op_Division ($a: UnityEngine.Vector3, $d: number) : UnityEngine.Vector3
            public static op_Equality ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : boolean
            public static op_Inequality ($lhs: UnityEngine.Vector3, $rhs: UnityEngine.Vector3) : boolean
            public ToString () : string
            /** Returns a formatted string for this vector. * @param format A numeric format string.
            * @param formatProvider An object that specifies culture-specific formatting.
            */
            public ToString ($format: string) : string
            /** Returns a formatted string for this vector. * @param format A numeric format string.
            * @param formatProvider An object that specifies culture-specific formatting.
            */
            public ToString ($format: string, $formatProvider: System.IFormatProvider) : string
            public constructor ($x: number, $y: number, $z: number)
            public constructor ($x: number, $y: number)
        }
        /** Quaternions are used to represent rotations. */
        class Quaternion extends System.ValueType implements System.IEquatable$1<UnityEngine.Quaternion>, System.IFormattable
        {
        }
        /** A standard 4x4 transformation matrix. */
        class Matrix4x4 extends System.ValueType implements System.IEquatable$1<UnityEngine.Matrix4x4>, System.IFormattable
        {
        }
        /** The coordinate space in which to operate. */
        enum Space
        { World = 0, Self = 1 }
    }
    namespace System {
        class Object
        {
        }
        class Single extends System.ValueType implements System.IComparable, System.IComparable$1<number>, System.IConvertible, System.IEquatable$1<number>, System.IFormattable
        {
        }
        class ValueType extends System.Object
        {
        }
        interface IComparable
        {
        }
        interface IComparable$1<T>
        {
        }
        interface IConvertible
        {
        }
        interface IEquatable$1<T>
        {
        }
        interface IFormattable
        {
        }
        class Double extends System.ValueType implements System.IComparable, System.IComparable$1<number>, System.IConvertible, System.IEquatable$1<number>, System.IFormattable
        {
        }
        class Void extends System.ValueType
        {
        }
        class Int32 extends System.ValueType implements System.IComparable, System.IComparable$1<number>, System.IConvertible, System.IEquatable$1<number>, System.IFormattable
        {
        }
        class Boolean extends System.ValueType implements System.IComparable, System.IComparable$1<boolean>, System.IConvertible, System.IEquatable$1<boolean>
        {
        }
        class Enum extends System.ValueType implements System.IComparable, System.IConvertible, System.IFormattable
        {
        }
        class Type extends System.Reflection.MemberInfo implements System.Reflection.IReflect, System.Runtime.InteropServices._Type, System.Reflection.ICustomAttributeProvider, System.Runtime.InteropServices._MemberInfo
        {
        }
        class String extends System.Object implements System.ICloneable, System.Collections.IEnumerable, System.IComparable, System.IComparable$1<string>, System.IConvertible, System.IEquatable$1<string>, System.Collections.Generic.IEnumerable$1<number>
        {
        }
        interface ICloneable
        {
        }
        class Char extends System.ValueType implements System.IComparable, System.IComparable$1<number>, System.IConvertible, System.IEquatable$1<number>
        {
        }
        class Array extends System.Object implements System.ICloneable, System.Collections.IEnumerable, System.Collections.IList, System.Collections.IStructuralComparable, System.Collections.IStructuralEquatable, System.Collections.ICollection
        {
        }
        class UInt64 extends System.ValueType implements System.IComparable, System.IComparable$1<bigint>, System.IConvertible, System.IEquatable$1<bigint>, System.IFormattable
        {
        }
        interface IFormatProvider
        {
        }
    }
    namespace System.Reflection {
        class MemberInfo extends System.Object implements System.Reflection.ICustomAttributeProvider, System.Runtime.InteropServices._MemberInfo
        {
        }
        interface ICustomAttributeProvider
        {
        }
        interface IReflect
        {
        }
    }
    namespace System.Runtime.InteropServices {
        interface _MemberInfo
        {
        }
        interface _Type
        {
        }
    }
    namespace System.Collections {
        interface IEnumerable
        {
        }
        interface IList extends System.Collections.IEnumerable, System.Collections.ICollection
        {
        }
        interface ICollection extends System.Collections.IEnumerable
        {
        }
        interface IStructuralComparable
        {
        }
        interface IStructuralEquatable
        {
        }
        interface IEnumerator
        {
        }
    }
    namespace System.Collections.Generic {
        interface IEnumerable$1<T> extends System.Collections.IEnumerable
        {
        }
        interface IList$1<T> extends System.Collections.IEnumerable, System.Collections.Generic.ICollection$1<T>, System.Collections.Generic.IEnumerable$1<T>
        {
        }
        interface ICollection$1<T> extends System.Collections.IEnumerable, System.Collections.Generic.IEnumerable$1<T>
        {
        }
        interface IReadOnlyCollection$1<T> extends System.Collections.IEnumerable, System.Collections.Generic.IEnumerable$1<T>
        {
        }
        interface IReadOnlyList$1<T> extends System.Collections.IEnumerable, System.Collections.Generic.IReadOnlyCollection$1<T>, System.Collections.Generic.IEnumerable$1<T>
        {
        }
        class List$1<T> extends System.Object implements System.Collections.IEnumerable, System.Collections.Generic.IList$1<T>, System.Collections.Generic.IReadOnlyCollection$1<T>, System.Collections.Generic.IReadOnlyList$1<T>, System.Collections.IList, System.Collections.Generic.ICollection$1<T>, System.Collections.ICollection, System.Collections.Generic.IEnumerable$1<T>
        {
        }
    }
    namespace UnityEngine.SceneManagement {
        /** Run-time data structure for *.unity file. */
        class Scene extends System.ValueType
        {
        }
    }
}
