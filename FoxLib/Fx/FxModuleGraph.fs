module FoxLib.Fx.Vfx.FxNodes

open FoxLib.Core

type public FxRandomGatherType =
    | Auto = 0
    | RelativeRootOffset = 1
    | AbsoluteValue = 2

type public FxVectorType =
    | Vector = 0
    | Rotates = 1
    | Color = 2

type public FxRenderBlendMode =
    | Alpha = 0
    | Add = 1
    | Sub = 2
    | Mul = 3
    | Min = 4
    | Opaque = 5

type public FxRenderSortMode =
    | None = 0
    | SimpleSort = 1
    | OnePointSort = 2
    | LocalSort = 3

type public FxPlayModeType =
    | OneShot = 0
    | Loop = 1
    | LoopFadeInOut = 2

type public FxUpdateType =
    | Normal = 0
    | DividesFrames = 1
    | DrawTiming = 2

type public FxExecutionPriorityType =
    | Must = 0
    | Normal = 1

type public FxBoundingBoxType =
    | None = 0
    | TimeProgresses = 1
    | Stop = 2

type public FxSimulationMode =
    | SimulationNormal
    | SimulationDecalPerf
    | SimulationMissileMove
    | SimulationCreateAndDestroyPerf
    | SimulationBulletLineMove
    | SimulationRpgWeaponMove
    | SimulationReceiveColorTest

type public FxShapeBoundBoxType =
    | Manual = 0

type public FxRotateOrderTyoe =
    | XyzOreder = 0

type public FxCameraLodType =
    | CameraDistance = 0
    | CameraArea = 1
    | LodPriority = 2

type public FxLodEmitPriorityLevel =
    | Level0 = 0
    | Level1 = 1
    | Level2 = 2
    | Level3 = 3
    | Level4 = 4
    | Level5 = 5
    | Level6 = 6
    | Level7 = 7
    | Level8 = 8
    | LevelMax = 9

type public FxGenerationFilterType =
    | Generation7 = 0
    | Generation8 = 1
    | Generation9 = 2

type public FxVariationGenerationFilterType =
    | None = 0
    | Generation7 = 1
    | Generation8 = 2

type public FxModuleGraph = {
    EffectName : StrCodeHash
    DebugInfo : bool
    AllFrame : uint32
    PlayMode : FxPlayModeType
    FadeInEndFrame : uint32
    FadeOutStartFrame : uint32
    UpdateType : FxUpdateType
    BoundingBoxType : FxBoundingBoxType
    BoundingBoxOffsetPos : Vector3
    BoundingBoxSize : Vector3
    ExecutionPriorityType : FxExecutionPriorityType
}

let private readFxModuleGraph readHash readBool readUInt32 readInt32 readVector3 =
    { EffectName = readHash;
    DebugInfo = readBool;
    AllFrame = readUInt32;
    PlayMode = enum <| readInt32();
    FadeInEndFrame = readUInt32;
    FadeOutStartFrame = readUInt32;
    UpdateType = enum <| readInt32();
    BoundingBoxType = enum <| readInt32();
    BoundingBoxOffsetPos = readVector3;
    BoundingBoxSize = readVector3;
    ExecutionPriorityType = enum <| readInt32() }

type public IFxNode = interface end

type public FxIntervalProbabilityEmitNode = {
    DelayFrame : uint32
    DelayFrameRandomRange : uint32
    EmitVersion : int32
    FadeOutPosition : float32
    FadeOutReverse : bool
    IntervalFrame : uint32
    LifeFrame : uint32
    LifeRandomRangeFrame : uint32
    NumMin : uint32
    NumMax : uint32
    Probability : float32
    RandomGatherSeedValue : uint32
    RandomGatherType : FxRandomGatherType
    ReceiveName : string
} with interface IFxNode

let private readFxIntervalProbabilityEmitNode readUInt32 readInt32 readFloat readBool readString =
    { DelayFrame = readUInt32;
    DelayFrameRandomRange = readUInt32;
    EmitVersion = readInt32;
    FadeOutPosition = readFloat;
    FadeOutReverse = readBool;
    IntervalFrame = readUInt32;
    LifeFrame = readUInt32;
    LifeRandomRangeFrame = readUInt32;
    NumMin = readUInt32;
    NumMax = readUInt32;
    Probability = readFloat;
    RandomGatherSeedValue = readUInt32;
    RandomGatherType = enum <| readInt32;
    ReceiveName = readString }

type public FxConstLifeNode = {
    LifeFrame : uint32
} with interface IFxNode

let private readFxConstLifeNode readUInt32 = 
    { LifeFrame = readUInt32 }

type public FxRandomVectorNode = {
    Force : float32
    Global : bool
    GlobalEvaluateRealTimeRootRotate : bool
    RandomGatherSeedValue : uint32
    RandomGatherType : FxRandomGatherType
    RandomMax : Vector4
    RandomMin : Vector4
    VectorType : FxVectorType
    XySquere : bool
} with interface IFxNode

let private readFxRandomVectorNode readFloat readBool readUInt32 readInt32 readVector4 =
    { Force = readFloat;
    Global = readBool;
    GlobalEvaluateRealTimeRootRotate = readBool;
    RandomGatherSeedValue = readUInt32;
    RandomGatherType = enum <| readInt32;
    RandomMax = readVector4;
    RandomMin = readVector4;
    VectorType = enum <| readInt32;
    XySquere = readBool }

type public FxConstVectorNode = {
    Force : float32
    Global : bool
    Vector : Vector4
    VectorType : FxVectorType
} with interface IFxNode

let private readFxConstVectorNode readFloat readBool readVector4 readInt32 =
    { Force = readFloat;
    Global = readBool;
    Vector = readVector4;
    VectorType = readInt32 }

type public FxColorVectorNode = {
    Color : Vector4
} with interface IFxNode

let private readFxColorVectorNode readVector4 = 
    { Color = readVector4 }

type public FxUniformVelocityVectorNode = struct end with interface IFxNode

type public FxUniformVelocityTimeVectorNode = struct end with interface IFxNode

type public FxDragTimeVectorNode = {
    Drag : float32
    Method : int32
    Scale : float32
} with interface IFxNode

let private readFxDragTimeVectorNode readFloat readInt32 = 
    { Drag = readFloat;
    Method = readInt32;
    Scale = readFloat }

type public FxCompositionVectorNode = {
    MaskValue : float32
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

let private readFxCompositionVectorNode readFloat readBool = 
    { FxCompositionVectorNode.MaskValue = readFloat;
    SecondMaskW = readBool;
    SecondMaskX = readBool;
    SecondMaskY = readBool;
    SecondMaskZ = readBool }

type public FxOscillateVector2Node = {
    Periodicity : bool
} with interface IFxNode

let private readFxOscillateVector2Node readBool = 
    { Periodicity = readBool }

type public FxMultiplyVectorNode = {
    MaskValue : float32
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

let private readFxMultiplyVectorNode readFloat readBool = 
    { FxMultiplyVectorNode.MaskValue = readFloat;
    SecondMaskW = readBool;
    SecondMaskX = readBool;
    SecondMaskY = readBool;
    SecondMaskZ = readBool }

type public FxPlaneRotShapeNode = {
    AutoBoundingBoxMargin : Vector4
    AxisFix : int32
    AxisFixParticleDirectionPoolName : string
    BaseRot : Quaternion
    BaseSizeScale : Quaternion
    BoundingBoxType : FxBoundingBoxType
    CenterU : float32
    CenterV : float32
    CullFace : bool
    Enable : bool
    LocalSpace : bool
    ManualBoundingBoxOffset : Vector3
    ManualBoundingBoxSize : Vector3
    NumSimulatedMaxParticle : uint32
    RotGlobal : bool
    RotateOrderType : FxRotateOrderTyoe
    SortMode : FxRenderSortMode
    SortOffset : float32
} with interface IFxNode

let private readFxPlaneRotShapeNode readVector4 readInt32 readString readQuaternion readFloat readBool readUInt32 readVector3 =
    { AutoBoundingBoxMargin = readVector4;
    AxisFix = readInt32;
    AxisFixParticleDirectionPoolName = readString;
    BaseRot = readQuaternion;
    BaseSizeScale = readQuaternion;
    BoundingBoxType = enum <| readInt32;
    CenterU = readFloat;
    CenterV = readFloat;
    CullFace = readBool;
    Enable = readBool;
    LocalSpace = readBool;
    ManualBoundingBoxOffset = readVector3;
    ManualBoundingBoxSize = readVector3;
    NumSimulatedMaxParticle = readUInt32;
    RotGlobal = readBool;
    RotateOrderType = enum <| readInt32;
    SortMode = enum <| readInt32;
    SortOffset = readFloat }

type public FxLightInfluenceMaterialNode = {
    AmbientRate : float32
    CameraFadeInFar : float32
    CameraFadeInNear : float32
    CameraZOffset : float32
    DirectionalLightRate : float32
    Opaque : bool
    PointLightRate : float32
    ReceiveShadowMap : bool
    ShaderType : int32
    SoftBlend : bool
    SoftBlendFactor : float32
    TextureAnimeBlend : bool
    TextureAnimeBlendFrame : float32
    TextureAnimeBlendHeight : uint32
    TextureAnimeBlendWidth : uint32
    TextureAnimeClamp : bool
    TextureAnimeRandomStart : bool
    TextureFile : string 
} with interface IFxNode

let private readFxLightInfluenceMaterialNode readFloat readBool readInt32 readUInt32 readString = 
    { AmbientRate = readFloat;
    CameraFadeInFar = readFloat;
    CameraFadeInNear = readFloat;
    CameraZOffset = readFloat;
    DirectionalLightRate = readFloat;
    Opaque = readBool;
    PointLightRate = readFloat;
    ReceiveShadowMap = readBool;
    ShaderType = readInt32;
    SoftBlend = readBool;
    SoftBlendFactor = readFloat;
    TextureAnimeBlend = readBool;
    TextureAnimeBlendFrame = readFloat;
    TextureAnimeBlendHeight = readUInt32;
    TextureAnimeBlendWidth = readUInt32;
    TextureAnimeClamp = readBool;
    TextureAnimeRandomStart = readBool;
    TextureFile = readString }

type public FxTimeScaleVectorNode = {
    EndScale : float32
    MaskW : bool
    MaskX : bool
    MaskY : bool
    MaskZ : bool
    StartScale : float32 
} with interface IFxNode

let private readFxTimeScaleVectorNode readFloat readBool =
    { EndScale = readFloat;
    MaskW = readBool;
    MaskX = readBool;
    MaskY = readBool;
    MaskZ = readBool;
    StartScale = readFloat }

type public FxUVMapRandomVectorNode = {
    RandomDivisionHeightGrid : uint32
    RandomDivisionWidthGrid : uint32
    RandomFlipU : bool
    RandomFlipV : bool
    RandomGatherSeedValue : uint32
    RandomGatherType : FxRandomGatherType
}

let private readFxUVMapRandomVectorNode readUInt32 readBool readInt32 =
    { RandomDivisionHeightGrid = readUInt32;
    RandomDivisionWidthGrid = readUInt32;
    RandomFlipU = readBool;
    RandomFlipV = readBool;
    RandomGatherSeedValue = readUInt32;
    RandomGatherType = enum <| readInt32 }

type public FxDynamicLuminanceMaterialNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
    UnknownBool0 : bool
    UnknownString0 : string
    UnknownFloat0 : float32
    UnknownFloat1 : float32
    UnknownFloat2 : float32
    UnknownFloat3 : float32
    UnknownBool1 : bool
    UnknownUInt4 : uint32
    UnknownFloat4 : float32
    UnknownFloat5 : float32
    UnknownUInt5 : uint32
    UnknownUInt6 : uint32
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownString1 : string 
} with interface IFxNode

let private readFxDynamicLuminanceMaterialNode readUInt32 readBool readString readFloat =
    { UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownUInt2 = readUInt32;
    UnknownUInt3 = readUInt32;
    UnknownBool0 = readBool;
    UnknownString0 = readString;
    UnknownFloat0 = readFloat;
    UnknownFloat1 = readFloat;
    UnknownFloat2 = readFloat;
    UnknownFloat3 = readFloat;
    UnknownBool1 = readBool;
    UnknownUInt4 = readUInt32;
    UnknownFloat4 = readFloat;
    UnknownFloat5 = readFloat;
    UnknownUInt5 = readUInt32;
    UnknownUInt6 = readUInt32;
    UnknownBool2 = readBool;
    UnknownBool3 = readBool;
    UnknownString1 = readString }

type public FxUVMapVectorNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownVector0 : Vector4
} with interface IFxNode

let private readFxUVMapVectorNode readUInt32 readBool readVector4 =
    { UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownBool0 = readBool;
    UnknownBool1 = readBool;
    UnknownVector0 = readVector4 }

type public FxSpriteRotShapeNode = {
    UnknownVector0 : Vector4
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUInt3 : uint32
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownUInt4 : uint32
    UnknownUInt5 : uint32
} with interface IFxNode

let private readFxSpriteRotShapeNode readVector4 readUInt32 readBool = 
    { UnknownVector0 = readVector4;
    UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownUInt2 = readUInt32;
    UnknownBool0 = readBool;
    UnknownBool1 = readBool;
    UnknownBool2 = readBool;
    UnknownVector1 = readVector4;
    UnknownVector2 = readVector4;
    UnknownUInt3 = readUInt32;
    UnknownBool3 = readBool;
    UnknownBool4 = readBool;
    UnknownUInt4 = readUInt32;
    UnknownUInt5 = readUInt32 }

type public FxInfinityLifeNode = {
    UnknownUInt0 : uint32
} with interface IFxNode

let readFxInfinityLifeNode readUInt32 = 
    { UnknownUInt0 = readUInt32 }

type public FxRandomLifeNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
} with interface IFxNode

let private readFxRandomLifeNode readUInt32 = 
    { UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownUInt2 = readUInt32;
    UnknownUInt3 = readUInt32 }

type public FxFirstLoopOnlyEmitNode = struct end

type public FxKeyframeVectorNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
    UnknownUInt4 : uint32
    UnknownUInt5 : uint32
    UnknownUInt6 : uint32
    UnknownFloat0 : float32[]
    UnknownFloat1 : float32[]
    UnknownUInt7 : uint32
    UnknownFloat2 : float32
    UnknownUInt8 : uint32
    UnknownFloat3 : float32
    UnknownUInt9 : uint32
    UnknownFloat4 : float32
} with interface IFxNode

let private readFxKeyframeVectorNode readUInt32 readFloatArray readFloat = 
    { UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownUInt2 = readUInt32;
    UnknownUInt3 = readUInt32;
    UnknownUInt4 = readUInt32;
    UnknownUInt5 = readUInt32;
    UnknownUInt6 = readUInt32;
    UnknownFloat0 = readFloatArray;
    UnknownFloat1 = readFloatArray;
    UnknownUInt7 = readUInt32;
    UnknownFloat2 = readFloat;
    UnknownUInt8 = readUInt32;
    UnknownFloat3 = readFloat;
    UnknownUInt9 = readUInt32;
    UnknownFloat4 = readFloat }

type public FxLodVectorNode = {
    UnknownFloat0 : float32
    UnknownFloat1 : float32
    UnknownFloat2 : float32
    UnknownFloat3 : float32
    UnknownFloat4 : float32
} with interface IFxNode

let private readFxLodVectorNode readFloat = 
    { UnknownFloat0 = readFloat;
    UnknownFloat1 = readFloat;
    UnknownFloat2 = readFloat;
    UnknownFloat3 = readFloat;
    UnknownFloat4 = readFloat }

type public FxUniformAccelVectorNode = struct end

type public WindFxVectorNode = {
    UnknownFloat0 : float32
    UnknownBool0 : bool
    UnknownUInt0 : uint32 
} with interface IFxNode

let private readWindFxVectorNode readFloat readBool readUInt32 = 
    { UnknownFloat0 = readFloat;
    UnknownBool0 = readBool;
    UnknownUInt0 = readUInt32 }

type public FxUVAnimeIntervalVectorNode = {
    UnknownFloat0 : float32
    UnknownBool0 : bool
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
    UnknownBool5 : bool 
} with interface IFxNode

let private readFxUVAnimeIntervalVectorNode readFloat readBool readUInt32 = 
    { UnknownFloat0 = readFloat;
    UnknownBool0 = readBool;
    UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownBool1 = readBool;
    UnknownBool2 = readBool;
    UnknownBool3 = readBool;
    UnknownBool4 = readBool;
    UnknownUInt2 = readUInt32;
    UnknownUInt3 = readUInt32;
    UnknownBool5 = readBool }

type public FxCameraCorrectionVectorNode = {
    UnknownFloat0 : float32
    UnknownUInt0 : uint32
    UnknownFloat1 : float32
    UnknownBool0 : bool
    UnknownUInt1 : uint32
    UnknownFloat2 : float32
    UnknownFloat3 : float32
    UnknownFloat4 : float32
    UnknownFloat5 : float32
} with interface IFxNode

let private readFxCameraCorrectionVectorNode readFloat readUInt32 readBool =
    { UnknownFloat0 = readFloat;
    UnknownUInt0 = readUInt32;
    UnknownFloat1 = readFloat;
    UnknownBool0 = readBool;
    UnknownUInt1 = readUInt32;
    UnknownFloat2 = readFloat;
    UnknownFloat3 = readFloat;
    UnknownFloat4 = readFloat;
    UnknownFloat5 = readFloat }

type public TppLensFlareShapeNode = {
    UnknownVector0 : Vector4
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownBool0 : bool
    UnknownDouble0 : double
    UnknownString0 : string
    UnknownBool1 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUInt2 : uint32
    UnknownString1 : string
    UnknownBool2 : bool
    UnknownFloat0 : float32
} with interface IFxNode

let private readTppLensFlareShapeNode readVector4 readUInt32 readBool readDouble readString readFloat =
    { UnknownVector0 = readVector4;
    UnknownUInt0 = readUInt32;
    UnknownUInt1 = readUInt32;
    UnknownBool0 = readBool;
    UnknownDouble0 = readDouble;
    UnknownString0 = readString;
    UnknownBool1 = readBool;
    UnknownVector1 = readVector4;
    UnknownVector2 = readVector4;
    UnknownUInt2 = readUInt32;
    UnknownString1 = readString;
    UnknownBool2 = readBool;
    UnknownFloat0 = readFloat }

type public FxReceiveVectorNode = {
    UnknownBool0 : bool
    UnknownVector0 : Vector4
    UnknownFloat0 : float32
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownBool5 : bool
    UnknownBool6 : bool
    UnknownString0 : string
} with interface IFxNode

let private readFxReceiveVectorNode readBool readVector4 readFloat readString=
    { UnknownBool0 = readBool;
    UnknownVector0 = readVector4;
    UnknownFloat0 = readFloat;
    UnknownBool1 = readBool;
    UnknownBool2 = readBool;
    UnknownBool3 = readBool;
    UnknownBool4 = readBool;
    UnknownBool5 = readBool;
    UnknownBool6 = readBool;
    UnknownString0 = readString }

type public FxTrailShapeNode = {
    UnknownFloat0 : float32
    UnknownBool0 : bool
    UnknownUInt0 : uint32
    UnknownFloat1 : float32
    UnknownFloat2 : float32
    UnknownUInt1 : uint32
    UnknownVector0 : Vector4
    UnknownFloat3 : float32
    UnknownUInt2 : uint32
    UnknownBool1 : bool
    UnknownUInt3 : uint32
    UnknownUInt4 : uint32
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUInt5 : uint32
    UnknownUInt6 : uint32
    UnknownBool3 : bool
    UnknownUInt7 : uint32
    UnknownBool4 : bool
    UnknownUInt8 : uint32
    UnknownUInt9 : uint32
} with interface IFxNode

let private FxTrailShapeNode readFloat readBool readUInt32 readVector4 = 
    { UnknownFloat0 = readFloat;
    UnknownBool0 = readBool;
    UnknownUInt0 = readUInt32;
    UnknownFloat1 = readFloat;
    UnknownFloat2 = readFloat;
    UnknownUInt1 = readUInt32;
    UnknownVector0 = readVector4;
    UnknownFloat3 = readFloat;
    UnknownUInt2 = readUInt32;
    UnknownBool1 = readBool;
    UnknownUInt3 = readUInt32;
    UnknownUInt4 = readUInt32;
    UnknownBool2 = readBool;
    UnknownVector1 = readVector4;
    UnknownVector2 = readVector4;
    UnknownUInt5 = readUInt32;
    UnknownUInt6 = readUInt32;
    UnknownBool3 = readBool;
    UnknownUInt7 = readUInt32;
    UnknownBool4 = readBool;
    UnknownUInt8 = readUInt32;
    UnknownUInt9 = readUInt32 }

type public FxSpriteShapeNode = {
    UnknownVector0 : Vector4
    UnknownUInt0 : uint32
    UnknownFloat0 : float32
    UnknownFloat1 : float32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
} with interface IFxNode

let private readFxSpriteShapeNode readVector4 readUInt32 readFloat readBool = 
    { UnknownVector0 = readVector4;
    UnknownUInt0 = readUInt32;
    UnknownFloat0 = readFloat;
    UnknownFloat1 = readFloat;
    UnknownBool0 = readBool;
    UnknownBool1 = readBool;
    UnknownBool2 = readBool;
    UnknownVector1 = readVector4;
    UnknownVector2 = readVector4;
    UnknownUInt1 = readUInt32;
    UnknownUInt2 = readUInt32;
    UnknownUInt3 = readUInt32 }