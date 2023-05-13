using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARKit;

[Serializable]
public class Mapping
{
    public ARKitBlendShapeLocation location;
    public string name;
}

[CreateAssetMenu(fileName = "ARFaceBlendShapesMapping", menuName = "AR Face Tracking Tools/Create Blend Shape Mapping", order = 1)]
public class ARFaceBlendShapesMapping : ScriptableObject
{

    public float coefficientScale = 100.0f;

    [SerializeField]
    public List<Mapping> mappings = new List<Mapping>
    {

        new Mapping { location = ARKitBlendShapeLocation.BrowDownLeft, name = "browDown_L" },
        new Mapping { location = ARKitBlendShapeLocation.BrowDownRight , name = "browDown_R" },
        new Mapping { location = ARKitBlendShapeLocation.BrowInnerUp , name = "browInnerUp" },
        new Mapping { location = ARKitBlendShapeLocation.BrowOuterUpLeft , name = "browOuterUp_L" },
        new Mapping { location = ARKitBlendShapeLocation.BrowOuterUpRight , name = "browOuterUp_R" },
        new Mapping { location = ARKitBlendShapeLocation.CheekPuff , name = "cheekPuff" },
        new Mapping { location = ARKitBlendShapeLocation.CheekSquintLeft , name = "cheekSquint_L" },
        new Mapping { location = ARKitBlendShapeLocation.CheekSquintRight , name = "cheekSquint_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeBlinkLeft , name = "eyeBlink_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeBlinkRight , name = "eyeBlink_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookDownLeft , name = "eyeLookDown_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookDownRight , name = "eyeLookDown_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookInLeft , name = "eyeLookIn_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookInRight , name = "eyeLookIn_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookOutLeft , name = "eyeLookOut_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookOutRight , name = "eyeLookOut_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookUpLeft , name = "eyeLookUp_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeLookUpRight , name = "eyeLookUp_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeSquintLeft , name = "eyeSquint_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeSquintRight , name = "eyeSquint_R" },
        new Mapping { location = ARKitBlendShapeLocation.EyeWideLeft , name = "eyeWide_L" },
        new Mapping { location = ARKitBlendShapeLocation.EyeWideRight , name = "eyeWide_R" },
        new Mapping { location = ARKitBlendShapeLocation.JawForward , name = "jawForward" },
        new Mapping { location = ARKitBlendShapeLocation.JawLeft , name = "jawLeft" },
        new Mapping { location = ARKitBlendShapeLocation.JawOpen , name = "jawOpen" },
        new Mapping { location = ARKitBlendShapeLocation.JawRight , name = "jawRight" },
        new Mapping { location = ARKitBlendShapeLocation.MouthClose , name = "mouthClose" },
        new Mapping { location = ARKitBlendShapeLocation.MouthDimpleLeft , name = "mouthDimple_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthDimpleRight , name = "mouthDimple_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthFrownLeft , name = "mouthFrown_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthFrownRight , name = "mouthFrown_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthFunnel , name = "mouthFunnel" },
        new Mapping { location = ARKitBlendShapeLocation.MouthLeft , name = "mouthLeft" },
        new Mapping { location = ARKitBlendShapeLocation.MouthLowerDownLeft , name = "mouthLowerDown_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthLowerDownRight , name = "mouthLowerDown_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthPressLeft , name = "mouthPress_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthPressRight , name = "mouthPress_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthPucker , name = "mouthPucker" },
        new Mapping { location = ARKitBlendShapeLocation.MouthRight , name = "mouthRight" },
        new Mapping { location = ARKitBlendShapeLocation.MouthRollLower , name = "mouthRollLower" },
        new Mapping { location = ARKitBlendShapeLocation.MouthRollUpper , name = "mouthRollUpper" },
        new Mapping { location = ARKitBlendShapeLocation.MouthShrugLower , name = "mouthShrugLower" },
        new Mapping { location = ARKitBlendShapeLocation.MouthShrugUpper , name = "mouthShrugUpper" },
        new Mapping { location = ARKitBlendShapeLocation.MouthSmileLeft , name = "mouthSmile_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthSmileRight , name = "mouthSmile_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthStretchLeft , name = "mouthStretch_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthStretchRight , name = "mouthStretch_R" },
        new Mapping { location = ARKitBlendShapeLocation.MouthUpperUpLeft , name = "mouthUpperUp_L" },
        new Mapping { location = ARKitBlendShapeLocation.MouthUpperUpRight , name = "mouthUpperUp_R" },
        new Mapping { location = ARKitBlendShapeLocation.NoseSneerLeft , name = "noseSneer_L" },
        new Mapping { location = ARKitBlendShapeLocation.NoseSneerRight , name = "noseSneer_R" },
        new Mapping { location = ARKitBlendShapeLocation.TongueOut , name = "tongueOut" }
    };
}