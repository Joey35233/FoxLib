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
    BoundingBoxType : int32
    BoundingBoxOffsetPos : Vector3
    BoundingBoxSize : Vector3
    ExecutionPriorityType : int32
}

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

type public FxConstLifeNode = {
    LifeFrame : uint32
} with interface IFxNode

type public FxRandomVectorNode = {
    Force : float
    Global : bool
    GlobalEvaluateRealTimeRootRotate : bool
    RandomGatherSeedValue : uint32
    RandomGatherType : int32
    RandomMax : Vector4
    RandomMin : Vector4
    VectorType : int32
    XySquere : bool
} with interface IFxNode

type public FxConstVectorNode = {
    Force : float
    Global : bool
    Vector : Vector4
    VectorType : int32
} with interface IFxNode

type public FxColorVectorNode = {
    Color : Vector4
} with interface IFxNode

type public FxUniformVelocityVectorNode = struct end with interface IFxNode

type public FxUniformVelocityTimeVectorNode = struct end with interface IFxNode

type public FxDragTimeVectorNode = {
    Drag : float
    Method : int32
    Scale : float 
} with interface IFxNode

type public FxCompositionVectorNode = {
    MaskValue : float
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

type public FxOscillateVector2Node = {
    Periodicity : bool
} with interface IFxNode

type public FxMultiplyVectorNode = {
    MaskValue : float
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

type public FxPlaneRotShapeNode = {
    AutoBoundingBoxMargin : Vector4
    AxisFix : int32
    AxisFixParticleDirectionPoolName : uint64
    BaseRot : Quaternion
    BaseSizeScale : Quaternion
    BoundingBoxType : int32
    CenterU : float
    CenterV : float
    CullFace : bool
    Enable : bool
    LocalSpace : bool
    ManualBoundingBoxOffset : Vector3
    ManualBoundingBoxSize : Vector3
    NumSimulatedMaxParticle : uint32
    RotGlobal : bool
    RotateOrderType : int32
    SortMode : int32
    SortOffset : float 
} with interface IFxNode

type public FxLightInfluenceMaterialNode = {
    AmbientRate : float
    CameraFadeInFar : float
    CameraFadeInNear : float
    CameraZOffset : float
    DirectionalLightRate : float
    Opaque : bool
    PointLightRate : float
    ReceiveShadowMap : bool
    ShaderType : int32
    SoftBlend : bool
    SoftBlendFactor : float
    TextureAnimeBlend : bool
    TextureAnimeBlendFrame : float
    TextureAnimeBlendHeight : uint32
    TextureAnimeBlendWidth : uint32
    TextureAnimeClamp : bool
    TextureAnimeRandomStart : bool
    TextureFile : string 
} with interface IFxNode

type public FxTimeScaleVectorNode = {
    EndScale : float
    MaskW : bool
    MaskX : bool
    MaskY : bool
    MaskZ : bool
    StartScale : float 
} with interface IFxNode

type public FxUVMapRandomVectorNode = {
    RandomDivisionHeightGrid : uint32
    RandomDivisionWidthGrid : uint32
    RandomFlipU : bool
    RandomFlipV : bool
    RandomGatherSeedValue : uint32
    RandomGatherType : int32
}

type public FxDynamicLuminanceMaterialNode = {
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownUint2 : uint32
    UnknownUint3 : uint32
    UnknownBool0 : bool
    UnknownString0 : string
    UnknownFloat0 : float
    UnknownFloat1 : float
    UnknownFloat2 : float
    UnknownFloat3 : float
    UnknownBool1 : bool
    UnknownUint4 : uint32
    UnknownFloat4 : float
    UnknownFloat5 : float
    UnknownUint5 : uint32
    UnknownUint6 : uint32
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownString1 : string 
} with interface IFxNode

type public FxUVMapVectorNode = {
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownVector0 : Vector4
} with interface IFxNode

type public FxSpriteRotShapeNode = {
    UnknownVector0 : Vector4
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownUint2 : uint32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUint3 : uint32
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownUint4 : uint32
    UnknownUint5 : uint32
} with interface IFxNode

type public FxInfinityLifeNode = {
    UnknownUint0 : uint32
} with interface IFxNode

type public FxRandomLifeNode = {
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownUint2 : uint32
    UnknownUint3 : uint32
} with interface IFxNode

type public FxFirstLoopOnlyEmitNode = struct end

type public FxKeyframeVectorNode = {
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownUint2 : uint32
    UnknownUint3 : uint32
    UnknownUint4 : uint32
    UnknownUint5 : uint32
    UnknownUint6 : uint32
    UnknownFloat0 : float[]
    UnknownFloat1 : float[]
    UnknownUint7 : uint32
    UnknownFloat2 : float
    UnknownUint8 : uint32
    UnknownFloat3 : float
    UnknownUint9 : uint32
    UnknownFloat4 : float
} with interface IFxNode

type public FxLodVectorNode = {
    UnknownFloat0 : float
    UnknownFloat1 : float
    UnknownFloat2 : float
    UnknownFloat3 : float
    UnknownFloat4 : float
} with interface IFxNode

type public FxUniformAccelVectorNode = struct end

type public WindFxVectorNode = {
    UnknownFloat0 : float
    UnknownBool0 : bool
    UnknownUint0 : uint32 
} with interface IFxNode

type public FxUVAnimeIntervalVectorNode = {
    UnknownFloat0 : float
    UnknownBool0 : bool
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownUint2 : uint32
    UnknownUint3 : uint32
    UnknownBool5 : bool 
} with interface IFxNode

type public FxCameraCorrectionVectorNode = {
    UnknownFloat0 : float
    UnknownUint0 : uint32
    UnknownFloat1 : float
    UnknownBool0 : bool
    UnknownUint1 : uint32
    UnknownFloat2 : float
    UnknownFloat3 : float
    UnknownFloat4 : float
    UnknownFloat5 : float
} with interface IFxNode

type public TppLensFlareShapeNode = {
    UnknownVector0 : Vector4
    UnknownUint0 : uint32
    UnknownUint1 : uint32
    UnknownBool0 : bool
    UnknownDouble0 : double
    UnknownString0 : string
    UnknownBool1 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUint2 : uint32
    UnknownString1 : string
    UnknownBool2 : bool
    UnknownFloat0 : float
} with interface IFxNode

type public FxReceiveVectorNode = {
    UnknownBool0 : bool
    UnknownVector0 : Vector4
    UnknownFloat0 : float
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownBool3 : bool
    UnknownBool4 : bool
    UnknownBool5 : bool
    UnknownBool6 : bool
    UnknownString0 : string
} with interface IFxNode

type public FxTrailShapeNode = {
    UnknownFloat0 : float
    UnknownBool0 : bool
    UnknownUint0 : uint32
    UnknownFloat1 : float
    UnknownFloat2 : float
    UnknownUint1 : uint32
    UnknownVector0 : Vector4
    UnknownFloat3 : float
    UnknownUint2 : uint32
    UnknownBool1 : bool
    UnknownUint3 : uint32
    UnknownUint4 : uint32
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUint5 : uint32
    UnknownUint6 : uint32
    UnknownBool3 : bool
    UnknownUint7 : uint32
    UnknownBool4 : bool
    UnknownUint8 : uint32
    UnknownUint9 : uint32
} with interface IFxNode

type public FxSpriteShapeNode = {
    UnknownVector0 : Vector4
    UnknownUint0 : uint32
    UnknownFloat0 : float
    UnknownFloat1 : float
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownBool2 : bool
    UnknownVector1 : Vector4
    UnknownVector2 : Vector4
    UnknownUint1 : uint32
    UnknownUint2 : uint32
    UnknownUint3 : uint32
} with interface IFxNode