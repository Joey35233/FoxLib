module FoxLib.Fx.Vfx

open System
open FoxLib.Core
open FoxLib.Fx.VfxNodes

/// <summmary>
/// Input functions to the Read function.
/// </summmary>
type public ReadFunctions = {
    /// Function to read a uint8.
    ReadUInt8 : Func<uint8>
    /// Function to read a int16.
    ReadUInt16 : Func<uint16>
    /// Function to read an int32.
    ReadInt32 : Func<int32>
    /// Function to read a uint32.
    ReadUInt32 : Func<uint32>
    /// Function to read a uint64.
    ReadUInt64 : Func<uint64>
    /// Function to read a float32.
    ReadSingle : Func<float32>
    /// Function to read a float64.
    ReadDouble : Func<float>
    /// Function to read a bool.
    ReadBool : Func<bool>
    /// Function to read a string.
    ReadString : Func<uint32, string>
    /// Function to skip a number of bytes.
    SkipBytes : Action<int>
}

/// <summmary>
/// Read parameters converted to F# functions.
/// </summmary>
type private ConvertedReadFunctions = {
    /// Function to read a uint8.
    ReadUInt8 : unit -> uint8 
    /// Function to read a uint16.
    ReadUInt16 : unit -> uint16
    /// Function to read a uint32.
    ReadInt32 : unit -> int32
    /// Function to read an int32.
    ReadUInt32 : unit -> uint32
    /// Function to read a uint64.
    ReadUInt64 : unit -> uint64
    /// Function to read a float32.
    ReadSingle : unit -> float32
    /// Function to read a float64.
    ReadDouble : unit -> float
    /// Function to read a bool.
    ReadBool : unit -> bool
    /// Function to read a string.
    ReadString : uint32 -> string
    /// Function to skip a number of bytes.
    SkipBytes : int -> unit
}

/// <summmary>
/// Converts the Read function's .NET Funcs into F# functions.
/// </summmary>
/// <param name="rawReadFunctions">Input functions supplied to the Read function.</param>
/// <returns>The converted functions.</returns>
let private convertReadFunctions (rawReadFunctions : ReadFunctions) =
    if rawReadFunctions.ReadUInt8 |> isNull then nullArg "ReadUInt8"
    if rawReadFunctions.ReadUInt16 |> isNull then nullArg "ReadUInt16"
    if rawReadFunctions.ReadInt32 |> isNull then nullArg "ReadUInt32"
    if rawReadFunctions.ReadUInt32 |> isNull then nullArg "ReadUInt32"
    if rawReadFunctions.ReadUInt64 |> isNull then nullArg "ReadUInt64"
    if rawReadFunctions.ReadSingle |> isNull then nullArg "ReadSingle"
    if rawReadFunctions.ReadDouble |> isNull then nullArg "ReadDouble"
    if rawReadFunctions.ReadBool |> isNull then nullArg "ReadBool"
    if rawReadFunctions.ReadString |> isNull then nullArg "ReadString"
    if rawReadFunctions.SkipBytes |> isNull then nullArg "SkipBytes"

    { ConvertedReadFunctions.ReadUInt8 = rawReadFunctions.ReadUInt8.Invoke;
    ReadUInt16 = rawReadFunctions.ReadUInt16.Invoke;
    ReadInt32 = rawReadFunctions.ReadInt32.Invoke;
    ReadUInt32 = rawReadFunctions.ReadUInt32.Invoke;
    ReadUInt64 = rawReadFunctions.ReadUInt64.Invoke;
    ReadSingle = rawReadFunctions.ReadSingle.Invoke;
    ReadDouble = rawReadFunctions.ReadDouble.Invoke;
    ReadBool = rawReadFunctions.ReadBool.Invoke;
    ReadString = rawReadFunctions.ReadString.Invoke;
    SkipBytes = rawReadFunctions.SkipBytes.Invoke;}

/// <summmary>
/// Read VFX Node parameters converted to F# functions.
/// </summmary>
type private FxReadFunctions = {
        /// Function to read a uint8.
        ReadUInt8 : unit -> uint8 
        /// Function to read an array of uint8s.
        ReadUInt8s : unit -> uint8[]
        /// Function to read a uint16.
        ReadUInt16 : unit -> uint16
        /// Function to read an array of uint16s.
        ReadUInt16s : unit -> uint16[]
        /// Function to read an int32.
        ReadInt32 : unit -> int32
        /// Function to read an array of int32s.
        ReadInt32s : unit -> int32[]
        /// Function to read a uint32.
        ReadUInt32 : unit -> uint32
        /// Function to read an array of uint32s.
        ReadUInt32s : unit -> uint32[]
        /// Function to read a uint64.
        ReadUInt64 : unit -> uint64
        /// Function to read an array of uint64s.
        ReadUInt64s : unit -> uint64[]
        /// Function to read a float32.
        ReadSingle : unit -> float32
        /// Function to read an array of float32s.
        ReadSingles : unit -> float32[]
        /// Function to read a float64.
        ReadDouble : unit -> float
        /// Function to read an array of float64s.
        ReadDoubles : unit -> float[]
        /// Function to read a bool.
        ReadBool : unit -> bool
        /// Function to read an array of bools.
        ReadBools : unit -> bool[]
        /// Function to read a string.
        ReadString : unit -> string
        /// Function to read an array of strings.
        ReadStrings : unit -> string[]
        /// Function to read a Vector3.
        ReadVector3 : unit -> Vector3
        /// Function to read an array of Vector3s.
        ReadVector3s : unit -> Vector3[]
        /// Function to read a Vector4.
        ReadVector4 : unit -> Vector4
        /// Function to read an array of Vector4s.
        ReadVector4s : unit -> Vector4[]
        /// Function to read a Quaternion.
        ReadQuaternion: unit -> Quaternion
        /// Function to read an array of Quaternions.
        ReadQuaternions : unit -> Quaternion[]
}

let private convertConvertedReadFunctions (convertedReadFunctions : ConvertedReadFunctions) =
    let readBool () = 
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadBool()

    let readBools () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadBool())

    let readUInt8 () = 
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt8()

    let readUInt8s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt8())

    let readUInt16 () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt16()

    let readUInt16s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt16())

    let readInt32 () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadInt32()

    let readInt32s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadInt32())

    let readUInt32 () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt32()

    let readUInt32s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt32())

    let readUInt64 () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt64()

    let readUInt64s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt64())

    let readFloat () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadSingle()

    let readFloats () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadSingle())

    let readDouble () =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadDouble()

    let readDoubles () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadDouble())

    let readString () =
        convertedReadFunctions.SkipBytes 1
        let charCount = convertedReadFunctions.ReadUInt16()
        convertedReadFunctions.ReadString (uint32 <| charCount)

    let readStrings () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> let charCount = convertedReadFunctions.ReadUInt16()
                                convertedReadFunctions.ReadString (uint32 <| charCount))

    let readVector3 () =
        convertedReadFunctions.SkipBytes 1
        let vector = FoxLib.Vector3.Read convertedReadFunctions.ReadSingle
        convertedReadFunctions.SkipBytes 4
        vector

    let readVector3s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> let vector = FoxLib.Vector3.Read convertedReadFunctions.ReadSingle
                                convertedReadFunctions.SkipBytes 4
                                vector )

    let readVector4 () =
        convertedReadFunctions.SkipBytes 1
        FoxLib.Vector4.Read convertedReadFunctions.ReadSingle


    let readVector4s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> FoxLib.Vector4.Read convertedReadFunctions.ReadSingle)
    
    let readQuaternion () =
        convertedReadFunctions.SkipBytes 1
        FoxLib.Quaternion.Read convertedReadFunctions.ReadSingle

    let readQuaternions () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> FoxLib.Quaternion.Read convertedReadFunctions.ReadSingle)

    { FxReadFunctions.ReadBool = readBool;
    ReadBools = readBools;
    ReadUInt8 = readUInt8;
    ReadUInt8s = readUInt8s;
    ReadUInt16 = readUInt16;
    ReadUInt16s = readUInt16s;
    ReadInt32 = readInt32;
    ReadInt32s = readInt32s;
    ReadUInt32 = readUInt32;
    ReadUInt32s = readUInt32s;
    ReadUInt64 = readUInt64;
    ReadUInt64s = readUInt64s;
    ReadSingle = readFloat;
    ReadSingles = readFloats;
    ReadDouble = readDouble;
    ReadDoubles = readDoubles;
    ReadString = readString;
    ReadStrings = readStrings;
    ReadVector3 = readVector3;
    ReadVector3s = readVector3s;
    ReadVector4 = readVector4;
    ReadVector4s = readVector4s;
    ReadQuaternion = readQuaternion;
    ReadQuaternions = readQuaternions }

type private Header = {
    NodeCount : uint16
    EdgeCount : uint16
}

let private readHeader skipBytes readCount =
    skipBytes 5

    let nodeCount = readCount()

    let edgeCount = readCount()   

    skipBytes 6

    { NodeCount = nodeCount; EdgeCount = edgeCount }

let private readGraph readUInt64 readBool readUInt32 readInt32 readVector3 readHash =
    let thing = readHash(); //assert (readUInt64() = 0x61d75b06ca22UL)
    
    printf "Is %i is equal to \"0x61d75b06ca22UL\"? %b" thing (thing = 0x61d75b06ca22UL)

    { AllFrame = readUInt32();
    BoundingBoxOffsetPos = readVector3();
    BoundingBoxSize = readVector3();
    BoundingBoxType = enum <| readInt32();
    DebugInfo = readBool();
    EffectName = readUInt64();
    ExecutionPriorityType = enum <| readInt32();
    FadeInEndFrame = readUInt32();
    FadeOutStartFrame = readUInt32();
    PlayMode = enum <| readInt32();
    UpdateType = enum <| readInt32() }

let private readFxIntervalProbabilityEmitNode readUInt32 readInt32 readFloat readBool readString =
    { DelayFrame = readUInt32();
    DelayFrameRandomRange = readUInt32();
    EmitVersion = readInt32();
    FadeOutPosition = readFloat();
    FadeOutReverse = readBool();
    IntervalFrame = readUInt32();
    LifeFrame = readUInt32();
    LifeRandomRangeFrame = readUInt32();
    NumMin = readUInt32();
    NumMax = readUInt32();
    Probability = readFloat();
    RandomGatherSeedValue = readUInt32();
    RandomGatherType = enum <| readInt32();
    ReceiveName = readString() }

let private readFxConstLifeNode readUInt32 = 
    { LifeFrame = readUInt32() }

let private readFxRandomVectorNode readFloat readBool readUInt32 readInt32 readVector4 =
    { Force = readFloat();
    Global = readBool();
    GlobalEvaluateRealTimeRootRotate = readBool();
    RandomGatherSeedValue = readUInt32();
    RandomGatherType = enum <| readInt32();
    RandomMax = readVector4();
    RandomMin = readVector4();
    VectorType = enum <| readInt32();
    XySquere = readBool() }

let private readFxConstVectorNode readFloat readBool readVector4 readInt32 =
    { Force = readFloat();
    Global = readBool();
    Vector = readVector4();
    VectorType = enum <| readInt32() }

let private readFxColorVectorNode readVector4 = 
    { Color = readVector4() }

let private readFxUniformVelocityVectorNode () =
    new FxUniformVelocityVectorNode()

let private readFxUniformVelocityTimeVectorNode () =
    new FxUniformVelocityTimeVectorNode()

let private readFxDragTimeVectorNode readFloat readInt32 = 
    { Drag = readFloat();
    Method = readInt32();
    Scale = readFloat() }

let private readFxCompositionVectorNode readFloat readBool = 
    { FxCompositionVectorNode.MaskValue = readFloat();
    SecondMaskW = readBool();
    SecondMaskX = readBool();
    SecondMaskY = readBool();
    SecondMaskZ = readBool() }

let private readFxOscillateVector2Node readBool = 
    { Periodicity = readBool() }

let private readFxMultiplyVectorNode readFloat readBool = 
    { FxMultiplyVectorNode.MaskValue = readFloat();
    SecondMaskW = readBool();
    SecondMaskX = readBool();
    SecondMaskY = readBool();
    SecondMaskZ = readBool() }
    
let private readFxPlaneRotShapeNode readVector4 readInt32 readString readQuaternion readFloat readBool readUInt32 readVector3 =
    { AutoBoundingBoxMargin = readVector4();
    AxisFix = readInt32();
    AxisFixParticleDirectionPoolName = readString();
    BaseRot = readQuaternion();
    BaseSizeScale = readFloat();
    BoundingBoxType = enum <| readInt32();
    CenterU = readFloat();
    CenterV = readFloat();
    CullFace = readBool();
    Enable = readBool();
    LocalSpace = readBool();
    ManualBoundingBoxOffset = readVector3();
    ManualBoundingBoxSize = readVector3();
    NumSimulatedMaxParticle = readUInt32();
    RotGlobal = readBool();
    RotateOrderType = enum <| readInt32();
    SortMode = enum <| readInt32();
    SortOffset = readFloat() }
    
let private readFxLightInfluenceMaterialNode readFloat readBool readInt32 readUInt32 readString = 
    { AmbientRate = readFloat();
    CameraFadeInFar = readFloat();
    CameraFadeInNear = readFloat();
    CameraZOffset = readFloat();
    DirectionalLightRate = readFloat();
    Opaque = readBool();
    PointLightRate = readFloat();
    ReceiveShadowMap = readBool();
    ShaderType = readInt32();
    SoftBlend = readBool();
    SoftBlendFactor = readFloat();
    TextureAnimeBlend = readBool();
    TextureAnimeBlendFrame = readFloat();
    TextureAnimeBlendHeight = readUInt32();
    TextureAnimeBlendWidth = readUInt32();
    TextureAnimeClamp = readBool();
    TextureAnimeRandomStart = readBool();
    TextureFile = readString() }
    
let private readFxTimeScaleVectorNode readFloat readBool =
    { EndScale = readFloat();
    MaskW = readBool();
    MaskX = readBool();
    MaskY = readBool();
    MaskZ = readBool();
    StartScale = readFloat() }
    
let private readFxUVMapRandomVectorNode readUInt32 readBool readInt32 =
    { RandomDivisionHeightGrid = readUInt32();
    RandomDivisionWidthGrid = readUInt32();
    RandomFlipU = readBool();
    RandomFlipV = readBool();
    RandomGatherSeedValue = readUInt32();
    RandomGatherType = enum <| readInt32() }
    
let private readFxDynamicLuminanceMaterialNode readUInt32 readBool readString readFloat =
    { UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownUInt2 = readUInt32();
    UnknownUInt3 = readUInt32();
    UnknownBool0 = readBool();
    UnknownString0 = readString();
    UnknownFloat0 = readFloat();
    UnknownFloat1 = readFloat();
    UnknownFloat2 = readFloat();
    UnknownFloat3 = readFloat();
    UnknownBool1 = readBool();
    UnknownUInt4 = readUInt32();
    UnknownFloat4 = readFloat();
    UnknownFloat5 = readFloat();
    UnknownUInt5 = readUInt32();
    UnknownUInt6 = readUInt32();
    UnknownBool2 = readBool();
    UnknownBool3 = readBool();
    UnknownString1 = readString() }
    
let private readFxUVMapVectorNode readUInt32 readBool readVector4 =
    { UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownBool0 = readBool();
    UnknownBool1 = readBool();
    UnknownVector0 = readVector4() }
    
let private readFxSpriteRotShapeNode readVector4 readUInt32 readBool = 
    { UnknownVector0 = readVector4();
    UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownUInt2 = readUInt32();
    UnknownBool0 = readBool();
    UnknownBool1 = readBool();
    UnknownBool2 = readBool();
    UnknownVector1 = readVector4();
    UnknownVector2 = readVector4();
    UnknownUInt3 = readUInt32();
    UnknownBool3 = readBool();
    UnknownBool4 = readBool();
    UnknownUInt4 = readUInt32();
    UnknownUInt5 = readUInt32() }

let private readFxInfinityLifeNode readUInt32 = 
    { UnknownUInt0 = readUInt32() }

let private readFxRandomLifeNode readUInt32 = 
    { UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownUInt2 = readUInt32();
    UnknownUInt3 = readUInt32() }

let private readFxFirstLoopOnlyEmitNode () = 
    new FxFirstLoopOnlyEmitNode()

let private readFxKeyframeVectorNode readUInt32 readFloatArray = 
    { UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownUInt2 = readUInt32();
    UnknownUInt3 = readUInt32();
    UnknownUInt4 = readUInt32();
    UnknownUInt5 = readUInt32();
    UnknownUInt6 = readUInt32();
    UnknownFloat0 = readFloatArray();
    UnknownFloat1 = readFloatArray();
    UnknownFloat2 = readFloatArray();
    UnknownFloat3 = readFloatArray();
    UnknownFloat4 = readFloatArray();
    UnknownFloat5 = readFloatArray();
    UnknownFloat6 = readFloatArray();
    UnknownFloat7 = readFloatArray() }

let private readFxLodVectorNode readFloat = 
    { UnknownFloat0 = readFloat();
    UnknownFloat1 = readFloat();
    UnknownFloat2 = readFloat();
    UnknownFloat3 = readFloat();
    UnknownFloat4 = readFloat() }

let private readFxUniformAccelVectorNode () =
    new FxUniformAccelVectorNode()
    
let private readWindFxVectorNode readFloat readBool readUInt32 = 
    { UnknownFloat0 = readFloat();
    UnknownBool0 = readBool();
    UnknownUInt0 = readUInt32() }
    
let private readFxUVAnimeIntervalVectorNode readFloat readBool readUInt32 = 
    { UnknownFloat0 = readFloat();
    UnknownBool0 = readBool();
    UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownBool1 = readBool();
    UnknownBool2 = readBool();
    UnknownBool3 = readBool();
    UnknownBool4 = readBool();
    UnknownUInt2 = readUInt32();
    UnknownUInt3 = readUInt32();
    UnknownBool5 = readBool() }
    
let private readFxCameraCorrectionVectorNode readFloat readUInt32 readBool =
    { UnknownFloat0 = readFloat();
    UnknownUInt0 = readUInt32();
    UnknownFloat1 = readFloat();
    UnknownBool0 = readBool();
    UnknownUInt1 = readUInt32();
    UnknownFloat2 = readFloat();
    UnknownFloat3 = readFloat();
    UnknownFloat4 = readFloat();
    UnknownFloat5 = readFloat() }

let private readTppLensFlareShapeNode readVector4 readUInt32 readBool readDouble readString readFloat =
    { UnknownVector0 = readVector4();
    UnknownUInt0 = readUInt32();
    UnknownUInt1 = readUInt32();
    UnknownBool0 = readBool();
    UnknownDouble0 = readDouble();
    UnknownString0 = readString();
    UnknownBool1 = readBool();
    UnknownVector1 = readVector4();
    UnknownVector2 = readVector4();
    UnknownUInt2 = readUInt32();
    UnknownString1 = readString();
    UnknownBool2 = readBool();
    UnknownFloat0 = readFloat() }

let private readFxReceiveVectorNode readBool readVector4 readFloat readString=
    { UnknownBool0 = readBool();
    UnknownVector0 = readVector4();
    UnknownFloat0 = readFloat();
    UnknownBool1 = readBool();
    UnknownBool2 = readBool();
    UnknownBool3 = readBool();
    UnknownBool4 = readBool();
    UnknownBool5 = readBool();
    UnknownBool6 = readBool();
    UnknownString0 = readString() }

let private readFxTrailShapeNode readFloat readBool readUInt32 readVector4 = 
    { UnknownFloat0 = readFloat();
    UnknownBool0 = readBool();
    UnknownUInt0 = readUInt32();
    UnknownFloat1 = readFloat();
    UnknownFloat2 = readFloat();
    UnknownUInt1 = readUInt32();
    UnknownVector0 = readVector4();
    UnknownFloat3 = readFloat();
    UnknownUInt2 = readUInt32();
    UnknownBool1 = readBool();
    UnknownUInt3 = readUInt32();
    UnknownUInt4 = readUInt32();
    UnknownBool2 = readBool();
    UnknownVector1 = readVector4();
    UnknownVector2 = readVector4();
    UnknownUInt5 = readUInt32();
    UnknownUInt6 = readUInt32();
    UnknownBool3 = readBool();
    UnknownUInt7 = readUInt32();
    UnknownBool4 = readBool();
    UnknownUInt8 = readUInt32();
    UnknownUInt9 = readUInt32() }

let private readFxSpriteShapeNode readVector4 readUInt32 readFloat readBool = 
    { UnknownVector0 = readVector4();
    UnknownUInt0 = readUInt32();
    UnknownFloat0 = readFloat();
    UnknownFloat1 = readFloat();
    UnknownBool0 = readBool();
    UnknownBool1 = readBool();
    UnknownBool2 = readBool();
    UnknownVector1 = readVector4();
    UnknownVector2 = readVector4();
    UnknownUInt1 = readUInt32();
    UnknownUInt2 = readUInt32();
    UnknownUInt3 = readUInt32() }

let private readNode (readUInt64 : unit -> uint64) (convertedFxReadFunctions : FxReadFunctions) : IFxNode = 
    match readUInt64() with
    | 0x2ccdc3ed2f6eUL -> (readFxIntervalProbabilityEmitNode convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadInt32 convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadString) :> IFxNode
    | 0x37388fadd256UL -> (readFxConstLifeNode convertedFxReadFunctions.ReadUInt32) :> IFxNode
    | 0xc3c76ab27693UL -> (readFxRandomVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadInt32 convertedFxReadFunctions.ReadVector4) :> IFxNode
    | 0x3ee12d07f4daUL -> (readFxConstVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadInt32) :> IFxNode
    | 0x697609fd2ae8UL -> (readFxUniformVelocityVectorNode ()) :> IFxNode
    | 0x9a5329c60ebcUL -> (readFxUniformVelocityTimeVectorNode ()) :> IFxNode
    | 0x9d7c0f2a4e3cUL -> (readFxColorVectorNode convertedFxReadFunctions.ReadVector4) :> IFxNode
    | 0x3905e060b8c7UL -> (readFxDragTimeVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadInt32) :> IFxNode
    | 0xc7a9f99df5f2UL -> (readFxCompositionVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0xc562fcbd414eUL -> (readFxOscillateVector2Node convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0xf4173d852e1cUL -> (readFxMultiplyVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0x162c431c53eaUL -> (readFxPlaneRotShapeNode convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadInt32 convertedFxReadFunctions.ReadString convertedFxReadFunctions.ReadQuaternion convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadVector3) :> IFxNode
    | 0xaa67cb565c60UL -> (readFxLightInfluenceMaterialNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadInt32 convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadString) :> IFxNode
    | 0xe64ae08647e7UL -> (readFxTimeScaleVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0xb4f04ae19f65UL -> (readFxUVMapRandomVectorNode convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadInt32) :> IFxNode
    | 0xe009774be7bdUL -> (readFxDynamicLuminanceMaterialNode convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadString convertedFxReadFunctions.ReadSingle):> IFxNode
    | 0x3a880680f7c8UL -> (readFxUVMapVectorNode convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadVector4) :> IFxNode
    | 0x87eba51c93dcUL -> (readFxSpriteRotShapeNode convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0xd47883800b1fUL -> (readFxInfinityLifeNode convertedFxReadFunctions.ReadUInt32) :> IFxNode
    | 0x328abc5662e9UL -> (readFxRandomLifeNode convertedFxReadFunctions.ReadUInt32) :> IFxNode
    | 0x42d7f0c46bdeUL -> (readFxFirstLoopOnlyEmitNode ()) :> IFxNode
    | 0x4499d0fddfe8UL -> (readFxKeyframeVectorNode convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadSingles) :> IFxNode
    | 0x3770626249deUL -> (readFxLodVectorNode convertedFxReadFunctions.ReadSingle) :> IFxNode
    | 0x9d13f7cb5a60UL -> (readFxUniformAccelVectorNode ()) :> IFxNode
    | 0xa37723c4be1dUL -> (readWindFxVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadUInt32) :> IFxNode
    | 0xb242b2105308UL -> (readFxUVAnimeIntervalVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadUInt32) :> IFxNode
    | 0xd94ae32798abUL -> (readFxCameraCorrectionVectorNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool) :> IFxNode
    | 0x244cd42b24a3UL -> (readTppLensFlareShapeNode convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadDouble convertedFxReadFunctions.ReadString convertedFxReadFunctions.ReadSingle) :> IFxNode
    | 0x1cd77d3e83f4UL -> (readFxReceiveVectorNode convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadString) :> IFxNode
    | 0x9e5bbc411d37UL -> (readFxTrailShapeNode convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadVector4) :> IFxNode
    | 0x8438c31e5c16UL -> (readFxSpriteShapeNode convertedFxReadFunctions.ReadVector4 convertedFxReadFunctions.ReadUInt32 convertedFxReadFunctions.ReadSingle convertedFxReadFunctions.ReadBool) :> IFxNode
    | x -> failwithf "Error: \"%i\" does not have a corresponding read function." x

type public Edge = {
    SourceNodeIndex : byte
    SourcePortIndex : byte
    
    TargetNodeIndex : byte
    TargetPortIndex : byte
}

let private readEdge readByte =
    let sourceNodeIndex = readByte()
    let targetNodeIndex = readByte()

    let sourcePortType = readByte()
    let sourcePortIndex = readByte()

    let targetPortType = readByte()
    let targetPortIndex = readByte()

    if (sourcePortType <> targetPortType) then printf "Error: Types do not match."

    { SourceNodeIndex = sourceNodeIndex; SourcePortIndex = sourcePortIndex; TargetNodeIndex = targetNodeIndex; TargetPortIndex = targetPortIndex }

type public VFXGraph = {
    HasDebugInfo : bool
    FrameCount : uint32
    PlayMode : FxPlayModeType
    FadeInEndFrameIndex : uint32
    FadeOutStartFrame : uint32
    UpdateType : FxUpdateType
    BoundingBoxType : FxBoundingBoxType
    BoundingBoxOffset : Vector3
    BoundingBoxSize : Vector3
    ExecutionPriorityType : FxExecutionPriorityType
    Nodes : IFxNode[]
    Edges : Edge[]
}

/// <summary>
/// Reads a VFX graph from a .vfx file.
/// </summary>
/// <param name="readFunctions">Functions to read various data types from the input.</param>
/// <returns>The parsed VFX graph.</returns>
let public Read readFunctions =
    let convertedReadFunctions = convertReadFunctions readFunctions

    let convertedConvertedReadFunctions = convertConvertedReadFunctions convertedReadFunctions
    
    let header = readHeader convertedReadFunctions.SkipBytes convertedReadFunctions.ReadUInt16

    //convertedReadFunctions.SkipBytes 6

    let moduleGraph = readGraph convertedConvertedReadFunctions.ReadUInt64 convertedConvertedReadFunctions.ReadBool convertedConvertedReadFunctions.ReadUInt32 convertedConvertedReadFunctions.ReadInt32 convertedConvertedReadFunctions.ReadVector3 convertedReadFunctions.ReadUInt64

    let nodes = [|1..((int <| header.NodeCount) - 1)|]
                 |> Array.map (fun _ -> readNode convertedReadFunctions.ReadUInt64 convertedConvertedReadFunctions)

    let edges = [|1..(int <| header.EdgeCount)|]
                 |> Array.map (fun _ -> readEdge convertedReadFunctions.ReadUInt8)

    { HasDebugInfo = moduleGraph.DebugInfo;
    FrameCount = moduleGraph.AllFrame;
    PlayMode = moduleGraph.PlayMode;
    FadeInEndFrameIndex = moduleGraph.FadeInEndFrame;
    FadeOutStartFrame = moduleGraph.FadeOutStartFrame;
    UpdateType = moduleGraph.UpdateType;
    BoundingBoxType = moduleGraph.BoundingBoxType;
    BoundingBoxOffset = moduleGraph.BoundingBoxOffsetPos;
    BoundingBoxSize = moduleGraph.BoundingBoxSize;
    ExecutionPriorityType = moduleGraph.ExecutionPriorityType;
    Nodes = nodes;
    Edges = edges }