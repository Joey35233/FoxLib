module FoxLib.Fx.Vfx

open System
open FoxLib.Core

type private Header = {
    NodeCount : uint16
    EdgeCount : uint16
}

let readHeader skipBytes readCount =
    skipBytes 5

    let nodeCount = readCount()

    let edgeCount = readCount()

    let header = { NodeCount = nodeCount; EdgeCount = edgeCount }

    skipBytes 6

    header

/// <summmary>
/// Input functions to the Read function.
/// </summmary>
type public ReadFunctions = {
    /// Function to read a uint8.
    ReadUInt8 : Func<uint8>
    /// Function to read a int16.
    ReadUInt16 : Func<uint16>
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
    ReadString : Func<string>
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
    ReadString : unit -> string
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
    if rawReadFunctions.ReadUInt32 |> isNull then nullArg "ReadUInt32"
    if rawReadFunctions.ReadUInt64 |> isNull then nullArg "ReadUInt64"
    if rawReadFunctions.ReadSingle |> isNull then nullArg "ReadSingle"
    if rawReadFunctions.ReadDouble |> isNull then nullArg "ReadDouble"
    if rawReadFunctions.ReadBool |> isNull then nullArg "ReadBool"
    if rawReadFunctions.ReadString |> isNull then nullArg "ReadString"
    if rawReadFunctions.SkipBytes |> isNull then nullArg "SkipBytes"

    { ConvertedReadFunctions.ReadUInt8 = rawReadFunctions.ReadUInt8.Invoke;
    ReadUInt16 = rawReadFunctions.ReadUInt16.Invoke;
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
        /// Function to read a uint32.
        ReadUInt32 : unit -> uint32
        /// Function to read an array of uint32.
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
        /// Function to read a float64.
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
    let readBool = 
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadBool

    let readBools () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadBool())

    let readUInt8 = 
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt8

    let readUInt8s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt8())

    let readUInt16 =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt16

    let readUInt16s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt16())

    let readUInt32 =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt32

    let readUInt32s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt32())

    let readUInt64 =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadUInt64

    let readUInt64s () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadUInt64())

    let readFloat =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadSingle

    let readFloats () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadSingle())

    let readDouble =
        convertedReadFunctions.SkipBytes 1
        convertedReadFunctions.ReadDouble

    let readDoubles () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.ReadDouble())

    let readString =
        convertedReadFunctions.SkipBytes 3
        convertedReadFunctions.ReadString

    let readStrings () =
        [|1..int <| convertedReadFunctions.ReadUInt8()|]
         |> Array.map (fun _ -> convertedReadFunctions.SkipBytes 2
                                convertedReadFunctions.ReadString() )

    let readVector3 () =
        convertedReadFunctions.SkipBytes 1
        FoxLib.Vector3.Read convertedReadFunctions.ReadSingle

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

/// <summary>
/// Reads a VFX graph from a .vfx file.
/// </summary>
/// <param name="readFunctions">Functions to read various data types from the input.</param>
/// <returns>The parsed VFX graph.</returns>
let public Read readFunctions =
    let convertedReadFunctions = convertReadFunctions readFunctions

    let convertedConvertedReadFunctions = convertConvertedReadFunctions convertedReadFunctions
    
    let header = readHeader convertedReadFunctions.SkipBytes convertedReadFunctions.ReadUInt16

    //[|1..(int <| header.NodeCount)|]
    // |> Array.map (fun _ -> readNode )