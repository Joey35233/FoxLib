module FoxLib.Fx.Vfx

open System
open FoxLib.Core

type private Header = {
    NodeCount : uint16
    EdgeCount : uint16
}

let readHeader skipBytes readCount =
    skipBytes 5

    let nodeCount = readCount

    let edgeCount = readCount

    { NodeCount = nodeCount; EdgeCount = edgeCount }

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
    /// Function to read a bool.
    ReadBool : Func<bool>
    /// Function to read a char array of specified length.
    ReadChars : Func<uint32, char[]>
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
    /// Function to read a bool.
    ReadBool : unit -> bool
    /// Function to read a char array of specified length/
    ReadChars : uint32 -> char[]
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
    if rawReadFunctions.ReadBool |> isNull then nullArg "ReadBool"
    if rawReadFunctions.ReadChars |> isNull then nullArg "ReadChars"
    if rawReadFunctions.SkipBytes |> isNull then nullArg "SkipBytes"

    { ConvertedReadFunctions.ReadSingle = rawReadFunctions.ReadSingle.Invoke;
    ReadUInt8 = rawReadFunctions.ReadUInt8.Invoke;
    ReadUInt16 = rawReadFunctions.ReadUInt16.Invoke;
    ReadUInt32 = rawReadFunctions.ReadUInt32.Invoke;
    ReadUInt64 = rawReadFunctions.ReadUInt64.Invoke;
    ReadBool = rawReadFunctions.ReadBool.Invoke;
    ReadChars = rawReadFunctions.ReadChars.Invoke;
    SkipBytes = rawReadFunctions.SkipBytes.Invoke;}

/// <summary>
/// Reads a VFX graph from a .vfx file.
/// </summary>
/// <param name="readFunctions">Functions to read various data types from the input.</param>
/// <returns>The parsed VFX graph.</returns>
let public Read readFunctions =
    let convertedReadFunctions = convertReadFunctions readFunctions
    
    let header = readHeader convertedReadFunctions.SkipBytes convertedReadFunctions.ReadInt16


