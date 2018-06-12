module FoxLib.Fx.VfxNodes

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

type public FxConstVectorNode = {
    Force : float32
    Global : bool
    Vector : Vector4
    VectorType : FxVectorType
} with interface IFxNode

type public FxColorVectorNode = {
    Color : Vector4
} with interface IFxNode

type public FxUniformVelocityVectorNode = struct end with interface IFxNode

type public FxUniformVelocityTimeVectorNode = struct end with interface IFxNode

type public FxDragTimeVectorNode = {
    Drag : float32
    Method : int32
    Scale : float32
} with interface IFxNode

type public FxCompositionVectorNode = {
    MaskValue : float32
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

type public FxOscillateVector2Node = {
    Periodicity : bool
} with interface IFxNode

type public FxMultiplyVectorNode = {
    MaskValue : float32
    SecondMaskW : bool
    SecondMaskX : bool
    SecondMaskY : bool
    SecondMaskZ : bool 
} with interface IFxNode

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

type public FxTimeScaleVectorNode = {
    EndScale : float32
    MaskW : bool
    MaskX : bool
    MaskY : bool
    MaskZ : bool
    StartScale : float32 
} with interface IFxNode

type public FxUVMapRandomVectorNode = {
    RandomDivisionHeightGrid : uint32
    RandomDivisionWidthGrid : uint32
    RandomFlipU : bool
    RandomFlipV : bool
    RandomGatherSeedValue : uint32
    RandomGatherType : FxRandomGatherType
} with interface IFxNode

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

type public FxUVMapVectorNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownBool0 : bool
    UnknownBool1 : bool
    UnknownVector0 : Vector4
} with interface IFxNode

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

type public FxInfinityLifeNode = {
    UnknownUInt0 : uint32
} with interface IFxNode

type public FxRandomLifeNode = {
    UnknownUInt0 : uint32
    UnknownUInt1 : uint32
    UnknownUInt2 : uint32
    UnknownUInt3 : uint32
} with interface IFxNode
 
type public FxFirstLoopOnlyEmitNode = struct end with interface IFxNode

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

type public FxLodVectorNode = {
    UnknownFloat0 : float32
    UnknownFloat1 : float32
    UnknownFloat2 : float32
    UnknownFloat3 : float32
    UnknownFloat4 : float32
} with interface IFxNode

type public FxUniformAccelVectorNode = struct end with interface IFxNode

type public WindFxVectorNode = {
    UnknownFloat0 : float32
    UnknownBool0 : bool
    UnknownUInt0 : uint32 
} with interface IFxNode

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