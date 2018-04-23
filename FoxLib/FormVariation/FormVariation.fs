﻿module FoxLib.FormVariation

open FoxLib.Core
open System

/// <summary>
/// Metadata for an fv2 file.
/// </summary>
type private Header = {
    Section2Offset : uint16;
    ExternalFileSectionOffset : uint16;
    Section2Entries : uint16;
    ExternalFileSectionEntries : uint16;
    Section3Offset : uint16;
    Section3Entries : uint16;
    NumTextures : uint16;
    NumHiddenMeshGroups : byte;
    NumShownMeshGroups : byte;
    NumMaterialInstances : byte;
    Unknown0 : byte;
    NumBoneAttachedModels : byte;
    NumCNPAttachedModels : byte;
}

/// <summary>
/// A section 1 section for texture-swapping.
/// </summary>
type private MaterialInstance = {
    MaterialInstanceHash : StrCode32Hash[]
    TextureTypeHash : StrCode32Hash[]
    ExternalFileListIndex : uint16[]
}

/// <summary>
/// A section 1 entry for models connected via bones.
/// </summary>
type private BoneConnection = {
    ExternalFileListIndex : uint16
    ExternalFileListIndexFrdv : uint16
    ExternalFileListIndexUnknown0 : uint16
    ExternalFileListIndexUnknown1 : uint16
    ExternalFileListIndexSim : uint16
    ExternalFileListIndexUnknown2 : uint16
}

/// <summary>
/// A section 1 entry for models connected via bones.
/// </summary>
type private CNPConnection = {
    CNPHash : StrCode32Hash
    EmptyHash : StrCode32Hash
    ExternalFileListIndex : uint16
    ExternalFileListIndexFrdv : uint16
    ExternalFileListIndexUnknown0 : uint16
    ExternalFileListIndexUnknown1 : uint16
    ExternalFileListIndexSim : uint16
    ExternalFileListIndexUnknown2 : uint16
}

/// <summary>
/// A texture-swapping form variation.
/// </summary>
type public TextureSwap = {
    MaterialInstanceHash : StrCode32Hash
    TextureTypeHash : StrCode32Hash
    TextureFileHash : StrCode64Hash
}

/// <summary>
/// An attachment to a bone.
/// </summary>
type public BoneAttachment = {
    ModelFileHash : StrCode64Hash
    FrdvFileHash : StrCode64Hash
    SimFileHash : StrCode64Hash
}

/// <summary>
/// An attachment to a connection point (CNP).
/// </summary>
type public CNPAttachment = {
    CNPHash : StrCode32Hash
    ModelFileHash : StrCode64Hash
    FrdvFileHash : StrCode64Hash
    SimFileHash : StrCode64Hash
}

/// <summary>
/// A set ot form variations.
/// </summary>
type public FormVariation = {
    HiddenMeshGroups : StrCode32Hash[]
    ShownMeshGroups : StrCode32Hash[]
    TextureSwaps : TextureSwap[]
    CNPAttachments : CNPAttachment[]
    BoneAttachments : BoneAttachment[]
}

/// <summary>
/// Read an fv2 header.
/// </summary>
/// <param name="readChars">Function to read a number of chars.</param>
/// <param name="readEntriesCount">Function to read an entries count.</param>
/// <param name="readOffset">Function to read an offset.</param>
/// <param name="readCount">Function to read a count.</param>
/// <param name="skipBytes">Function to skip bytes.</param>
/// <returns>Returns a parsed fv2 header.</returns>
let private readHeader readEntriesCount readOffset readCount skipBytes =
    skipBytes 8
    
    let section2Offset = readOffset();
    let externalFileSectionOffset = readOffset();
    let section2Entries = readEntriesCount();
    let externalFileSectionEntries = readEntriesCount();
    let section3Offset = readOffset();
    let section3Entries = readEntriesCount();

    skipBytes 4

    let numTextures = readEntriesCount();

    skipBytes 4

    let numHiddenMeshGroups = readCount();
    let numShownMeshGroups = readCount();
    let numMaterialInstances = readCount();
    let unknown0 = readCount();
    let numBoneAttachedModels = readCount();
    let numCNPAttachedModels = readCount();

    { Section2Offset = section2Offset;
    ExternalFileSectionOffset = externalFileSectionOffset;
    Section2Entries = section2Entries;
    ExternalFileSectionEntries = externalFileSectionEntries;
    Section3Offset = section3Offset;
    Section3Entries =  section3Entries;

    NumTextures = numTextures;
    
    NumHiddenMeshGroups = numHiddenMeshGroups;
    NumShownMeshGroups = numShownMeshGroups;
    NumMaterialInstances = numMaterialInstances;
    Unknown0 = unknown0;
    NumBoneAttachedModels = numBoneAttachedModels;
    NumCNPAttachedModels = numCNPAttachedModels;
    }

/// <summary>
/// Function to read a list of mesh groups to be hidden.
/// </summary>
/// <param name="readHash"></param>
/// <param name="numHiddenMeshGroups">The number of mesh groups to hide.</param>
/// <returns>Returns a list of mesh groups to be shown.</returns>
let private readHiddenMeshGroups readHash numHiddenMeshGroups=
     match numHiddenMeshGroups with
     | i when i > 0uy -> [|1uy..numHiddenMeshGroups|]
                          |> Array.map (fun _ -> readHash())
     | _ -> Array.empty<StrCode32Hash>

/// <summary>
/// Function to read a list of mesh groups to be shown.
/// </summary>
/// <param name="readHash">Function to read an StrCode32Hash</param>
/// <param name="readIndex">The number of mesh groups to show.</param>
/// <returns>Returns a list of mesh groups to be hidden.</returns>
let private readShownMeshGroups readHash numShownMeshGroups =
     match numShownMeshGroups with
     | i when i > 0uy -> [|1uy..numShownMeshGroups|]
                          |> Array.map (fun _ -> readHash())
     | _ -> Array.empty<StrCode32Hash>

/// <summary>
/// Function to read material instances.
/// </summary>
/// <param name="readHash">Function to read an StrCode32Hash</param>
/// <param name="readIndex">Function to read an index.</param>
/// <param name="skipBytes">Function to skip bytes.</param>
/// <param name="readIndex">The number of material instance entries.</param>
/// <returns>Returns a material instance list.</returns>
let private readMaterialInstances readHash readIndex skipBytes materialInstanceCount = 
    match materialInstanceCount with
    | i when i > 0uy -> let materialInstanceHash = [|1uy..materialInstanceCount|]
                                                    |> Array.map (fun _ -> readHash())

                        let textureTypeHash = [|1uy..materialInstanceCount|]
                                               |> Array.map (fun _ -> readHash())

                        let externalFileListIndex = [|1uy..materialInstanceCount|]
                                                     |> Array.map (fun _ -> readIndex())

                        skipBytes ((2uy * materialInstanceCount) |> int)
    
                        { MaterialInstanceHash = materialInstanceHash;
                        TextureTypeHash = textureTypeHash;
                        ExternalFileListIndex = externalFileListIndex;
                        }
    | _ -> { MaterialInstanceHash = Array.Empty<StrCode32Hash>() ; TextureTypeHash = Array.Empty<StrCode32Hash>(); ExternalFileListIndex = Array.Empty<uint16>(); }

/// <summary>
/// Function to read connections connected via bones.
/// </summary>
/// <param name="readIndex">Function to read an index.</param>
/// <param name="readIndex">The number of bone connection entries.</param>
/// <returns>Returns a bone connection list.</returns>
let private readBoneConnections readIndex numBoneConnections =
    match numBoneConnections with
    | i when i > 0uy -> [|1uy..numBoneConnections|]
                         |> Array.map (fun _ -> { ExternalFileListIndex = readIndex();
                                                  ExternalFileListIndexFrdv = readIndex();
                                                  ExternalFileListIndexUnknown0 = readIndex();
                                                  ExternalFileListIndexUnknown1 = readIndex();
                                                  ExternalFileListIndexSim = readIndex();
                                                  ExternalFileListIndexUnknown2 = readIndex(); })
    | _ -> Array.empty<BoneConnection>

/// <summary>
/// Function to read connections connected via connection points (CNPs).
/// </summary>
/// <param name="readIndex">Function to read an index.</param>
/// <param name="readIndex">The number of CNP connection entries.</param>
/// <returns>Returns a CNP connection list.</returns>
let private readCNPConnections readHash readIndex numCNPConnections =
    match numCNPConnections with
    | i when i > 0uy -> [|1uy..numCNPConnections|]
                         |> Array.map (fun _ -> { CNPHash = readHash();
                                                  EmptyHash = readHash();
                                                  ExternalFileListIndex = readIndex();
                                                  ExternalFileListIndexFrdv = readIndex();
                                                  ExternalFileListIndexUnknown0 = readIndex();
                                                  ExternalFileListIndexUnknown1 = readIndex();
                                                  ExternalFileListIndexSim = readIndex();
                                                  ExternalFileListIndexUnknown2 = readIndex(); })
    | _ -> Array.empty<CNPConnection>

/// <summary>
/// Function to read a list of external file hashes.
/// </summary>
/// <param name="readHash">Function to read an StrCode32Hash.</param>
/// <param name="section3Entries">The number of external file hashes.</param>
/// <returns>Returns a list of external file hashes.</returns>
let private readExternalFileHashes (readHash : unit -> uint64) section3Entries = 
    let (hashes : StrCode64Hash[]) = [|0us..section3Entries|]
                                    |> Array.map (fun _ -> readHash())
    hashes

/// <summary>
/// Converts from a MaterialInstance to a more user-friendly array of TextureSwaps.
/// </summary>
/// <param name="materialInstance">The MaterialInstance.</param>
/// <param name="fileHashes">A list of hashed file names as StrCode64 hashes.</param>
let private makeTextureSwap (materialInstance : MaterialInstance ) (fileHashes : StrCode64Hash[]) = 
    [|1..materialInstance.ExternalFileListIndex.Length|]
     |> Array.map (fun index -> { MaterialInstanceHash = materialInstance.MaterialInstanceHash.[index]; 
                                TextureTypeHash = materialInstance.TextureTypeHash.[index]; 
                                TextureFileHash = fileHashes.[materialInstance.ExternalFileListIndex.[index] |> int] })

/// <summary>
/// Converts from a BoneConnection array to a more user-friendly array of BoneAttachments.
/// </summary>
/// <param name="boneConnections">The BoneConnection array.</param>
/// <param name="fileHashes">A list of hashed file names as StrCode64 hashes.</param>
let private makeBoneAttachment (boneConnections : BoneConnection[]) (fileHashes : StrCode64Hash[]) = 
    boneConnections
    |> Array.map (fun boneConnection -> { ModelFileHash = fileHashes.[boneConnection.ExternalFileListIndex |> int]; 
                                        FrdvFileHash = fileHashes.[boneConnection.ExternalFileListIndexFrdv |> int]; 
                                        SimFileHash = fileHashes.[boneConnection.ExternalFileListIndexSim |> int] })

/// <summary>
/// Converts from a CNPConnection array to a more user-friendly array of CNPAttachments.
/// </summary>
/// <param name="CNPConnections">The CNPConnection array.</param>
/// <param name="fileHashes">A list of hashed file names as StrCode64 hashes.</param>
let private makeCNPAttachment (CNPConnections : CNPConnection[]) (fileHashes : StrCode64Hash[]) = 
    CNPConnections
    |> Array.map (fun CNPConnection -> { CNPHash = CNPConnection.CNPHash;
                                        ModelFileHash = fileHashes.[CNPConnection.ExternalFileListIndex |> int]; 
                                        FrdvFileHash = fileHashes.[CNPConnection.ExternalFileListIndexFrdv |> int]; 
                                        SimFileHash = fileHashes.[CNPConnection.ExternalFileListIndexSim |> int] })

/// <summmary>
/// Input functions to the Read function.
/// </summmary>
type public ReadFunctions = {
    /// Function to read a uint16.
    ReadUInt16 : Func<uint16>
    /// Function to read a uint32.
    ReadUInt32 : Func<uint32>
    /// Function to read a uint64.
    ReadUInt64 : Func<uint64>
    /// Function to read a byte.
    ReadByte : Func<byte>
    /// Function to skip a number of bytes.
    SkipBytes : Action<int>
}

/// <summmary>
/// Read parameters converted to F# functions.
/// </summmary>
type private ConvertedReadFunctions = {
    ReadUInt16 : unit -> uint16
    ReadUInt32 : unit -> uint32
    ReadUInt64 : unit -> uint64
    ReadByte : unit -> byte
    SkipBytes : int -> unit
}

/// <summmary>
/// Converts the Read function's .NET Funcs into F# functions.
/// </summmary>
/// <param name="rawReadFunctions">Input functions supplied to the Read function.</param>
/// <returns>The converted functions.</returns>
let private convertReadFunctions (rawReadFunctions : ReadFunctions) =
    if rawReadFunctions.ReadUInt16 |> isNull then nullArg "ReadUInt16"
    if rawReadFunctions.ReadUInt32 |> isNull then nullArg "ReadUInt32"
    if rawReadFunctions.ReadUInt64 |> isNull then nullArg "ReadUInt64"
    if rawReadFunctions.ReadByte |> isNull then nullArg "ReadByte"
    if rawReadFunctions.SkipBytes |> isNull then nullArg "SkipBytes"

    { ConvertedReadFunctions.ReadUInt16 = rawReadFunctions.ReadUInt16.Invoke;
    ReadUInt32 = rawReadFunctions.ReadUInt32.Invoke;
    ReadUInt64 = rawReadFunctions.ReadUInt64.Invoke;
    ReadByte = rawReadFunctions.ReadByte.Invoke;
    SkipBytes = rawReadFunctions.SkipBytes.Invoke; }

/// <summmary>
/// Parses a FormVariation list from fv2 format.
/// </summmary>
/// <param name="readFunction">Function to read a data type from the input.</param>
/// <returns>The parsed FormVariation list.</returns>
let public Read (readFunctions : ReadFunctions) =
    let convertedFunctions = convertReadFunctions readFunctions

    let header = readHeader convertedFunctions.ReadUInt16 convertedFunctions.ReadUInt16 convertedFunctions.ReadByte convertedFunctions.SkipBytes   
    
    let hiddenMeshGroups = readHiddenMeshGroups convertedFunctions.ReadUInt32 header.NumShownMeshGroups
    
    let shownMeshGroups = readShownMeshGroups convertedFunctions.ReadUInt32 header.NumShownMeshGroups

    let materialInstances = readMaterialInstances convertedFunctions.ReadUInt32 convertedFunctions.ReadUInt16 convertedFunctions.SkipBytes header.NumMaterialInstances

    let boneConnections = readBoneConnections convertedFunctions.ReadUInt16 header.NumBoneAttachedModels

    let CNPConnections = readCNPConnections convertedFunctions.ReadUInt32 convertedFunctions.ReadUInt16 header.NumCNPAttachedModels

    let externalFileHashes = readExternalFileHashes convertedFunctions.ReadUInt64 header.Section3Entries

    { HiddenMeshGroups = hiddenMeshGroups;
    ShownMeshGroups = shownMeshGroups;
    TextureSwaps = makeTextureSwap materialInstances externalFileHashes;
    BoneAttachments = makeBoneAttachment boneConnections externalFileHashes;
    CNPAttachments = makeCNPAttachment CNPConnections externalFileHashes }

/// <summary>
/// A null value that is written if any data section slot is unused.
/// </summary>
let private nullValue = 0xFFFFus

/// <summary>
/// A list of external file hashes.
/// </summary>
let mutable (fileList : StrCode64Hash list) = List.empty

/// <summary>
/// Adds a given hash to the fileList and returns the index of said hash.
/// </summary>
/// <param name="hash">The hash to add to the fileList.</param>
/// <returns>The index of the hash in the file list.</returns>
let private getFileIndex hash =
    let index = fileList.Length |> uint16

    fileList <- hash :: fileList

    index

/// <summary>
/// Creates a MaterialInstance from an array of TextureSwaps.
/// </summary>
/// <param name="textureSwaps">The TextureSwap array to convert.</param>
/// <returns>The converted MaterialInstance array.</returns>
let private makeMaterialInstances textureSwaps =
    { MaterialInstanceHash = textureSwaps |> Array.map (fun textureSwap -> textureSwap.MaterialInstanceHash);
    TextureTypeHash = textureSwaps |> Array.map (fun textureSwap -> textureSwap.TextureTypeHash);
    ExternalFileListIndex = textureSwaps |> Array.map (fun textureSwap ->  getFileIndex textureSwap.TextureFileHash)
    }

/// <summary>
/// Creates an array of BoneConnections from an array of BoneAttachments.
/// </summary>
/// <param name="boneAttachments">The BoneAttachment array to convert.</param>
/// <returns>The converted BoneConnection array.</returns>
let private makeBoneConnections (boneAttachments : BoneAttachment[]) = 
    boneAttachments
    |> Array.map (fun boneAttachment -> { ExternalFileListIndex = getFileIndex boneAttachment.ModelFileHash;
                                        ExternalFileListIndexFrdv = getFileIndex boneAttachment.FrdvFileHash;
                                        ExternalFileListIndexUnknown0 = nullValue;
                                        ExternalFileListIndexUnknown1 = nullValue;
                                        ExternalFileListIndexSim = getFileIndex boneAttachment.SimFileHash;
                                        ExternalFileListIndexUnknown2 = nullValue; })

/// <summary>
/// Creates an array of CNPConnections from an array of CNPAttachments.
/// </summary>
/// <param name="boneAttachments">The CNPAttachment array to convert.</param>
/// <returns>The converted CNPConnection array.</returns>
let private makeCNPConnections CNPAttachments = 
    CNPAttachments
    |> Array.map (fun CNPAttachment -> { CNPHash = CNPAttachment.CNPHash;
                                        EmptyHash = 0xbf169f98u;
                                        ExternalFileListIndex = getFileIndex CNPAttachment.ModelFileHash;
                                        ExternalFileListIndexFrdv = getFileIndex CNPAttachment.FrdvFileHash;
                                        ExternalFileListIndexUnknown0 = nullValue;
                                        ExternalFileListIndexUnknown1 = nullValue;
                                        ExternalFileListIndexSim = getFileIndex CNPAttachment.SimFileHash;
                                        ExternalFileListIndexUnknown2 = nullValue; })

/// <summary>
/// Creates a Header from a FormVariaton.
/// </summary>
/// <param name="formVariation">The FormVariation to convert.</param>
let private calculateHeader formVariation =
    let hiddenMeshGroupsNum = formVariation.HiddenMeshGroups.Length |> byte
    
    let shownMeshGroupsNum = formVariation.ShownMeshGroups.Length |> byte

    let materialInstanceNum = formVariation.TextureSwaps.Length |> byte

    let boneConnectionsNum = formVariation.BoneAttachments.Length |> byte
    
    let CNPConnectionsNum = formVariation.CNPAttachments.Length |> byte
    
    let headerSize = 40us
    let meshGroupsEntrySize = 4us
    let hiddenMeshGroupsSize = meshGroupsEntrySize * (hiddenMeshGroupsNum |> uint16)
    let shownMeshGroupsSize = meshGroupsEntrySize * (shownMeshGroupsNum |> uint16)

    let materialInstanceEntrySize = 12us
    let materialInstanceSize = materialInstanceEntrySize * (materialInstanceNum |> uint16)

    let boneConnectionsEntrySize = 12us
    let boneConnectionsSize = boneConnectionsEntrySize * (boneConnectionsNum |> uint16)

    let CNPConnectionsEntrySize = 20us
    let CNPConnectionsSize = CNPConnectionsEntrySize * (CNPConnectionsNum |> uint16)

    let section2EntrySize = 0us //Not actually zero, but it is unknown what the purpose of this section is.
    let section2Size = section2EntrySize * 0us //This section is being ignored for now.

    let section3EntrySize = 0us //Seems to always be null.
    let section3Size = section3EntrySize * 0us

    let fileHashListEntrySize = 8us
    let fileHashListSize = fileHashListEntrySize * (fileList.Length |> uint16)

    let section2Offset = headerSize + hiddenMeshGroupsSize + shownMeshGroupsSize + materialInstanceSize + boneConnectionsSize + CNPConnectionsSize

    let section3Offset = section2Offset + section2Size
    
    let section4Offset = section3Offset + section3Size

    { Section2Offset = section2Offset;
    ExternalFileSectionOffset = section4Offset;
    Section2Entries = 0us;
    ExternalFileSectionEntries = fileList.Length |> uint16;
    Section3Offset = section3Offset;
    Section3Entries = 0us;
    NumTextures = materialInstanceNum |> uint16;
    NumHiddenMeshGroups = hiddenMeshGroupsNum;
    NumShownMeshGroups = shownMeshGroupsNum;
    NumMaterialInstances = materialInstanceNum;
    Unknown0 = 0uy;
    NumBoneAttachedModels = boneConnectionsNum;
    NumCNPAttachedModels = CNPConnectionsNum;
    }

/// <summary>
/// A function to write an fv2 Header to a file.
/// </summary>
/// <param name="header">The Header to write.</param>
/// <param name="writeChars">A function to write a given number of chars.</param>
/// <param name="writeEntriesCount">A function to write a an amount of section entries.</param>
/// <param name="writeOffset">A function to write an offset.</param>
/// <param name="writeCount">A function to write a count.</param>
/// <param name="writeEmptyBytes">A function to write a given number of empty bytes.</param>
let private writeHeader header writeChars writeEntriesCount writeOffset writeCount writeEmptyBytes = 
    let signature = [| 'F'; 'O'; 'V' ; '2' ; 'w'; 'i'; 'n' |]
    writeChars signature
    writeEmptyBytes 1

    writeOffset header.Section2Offset

    writeOffset header.ExternalFileSectionOffset

    writeEntriesCount header.Section2Entries
    
    writeEntriesCount header.ExternalFileSectionEntries

    writeOffset header.Section3Offset

    writeEntriesCount header.Section3Entries

    writeEmptyBytes 6

    writeEntriesCount header.NumTextures

    writeEmptyBytes 4

    writeCount header.NumHiddenMeshGroups

    writeCount header.NumShownMeshGroups

    writeCount header.NumMaterialInstances

    writeCount header.Unknown0

    writeCount header.NumBoneAttachedModels

    writeCount header.NumCNPAttachedModels

    writeEmptyBytes 2

/// <summary>
/// Writes an array of StrCode32Hashes to a file.
/// </summary>
/// <param name="hashes">The hashes to write.</param>
/// <param name="writeHash">A function to write an StrCode32Hash.</param>
let private writeStrCode32Hashes hashes writeHash =
    hashes
    |> Array.iter (fun hash -> writeHash hash)

/// <summary>
/// Writes a MaterialInstance set to a file.
/// </summary>
/// <param name="materialInstance">The MaterialInstance to write.</param>
/// <param name="writeHash">A function to write an StrCode32Hash.</param>
/// <param name="writeIndex">A function to write an index.</param>
let private writeMaterialInstances (materialInstance : MaterialInstance) writeHash writeIndex = 
    materialInstance.MaterialInstanceHash
    |> Array.iter (fun materialInstanceHash -> writeHash materialInstanceHash)

    materialInstance.TextureTypeHash
    |> Array.iter (fun textureTypeHash -> writeHash textureTypeHash)

    materialInstance.ExternalFileListIndex
    |> Array.iter (fun externalFileListIndex -> writeIndex externalFileListIndex)

    materialInstance.ExternalFileListIndex
    |> Array.iter (fun _ -> writeIndex nullValue)

/// <summary>
/// Writes an array of BoneConnections to a file.
/// </summary>
/// <param name="boneConnections">The BoneConnections to write.</param>
/// <param name="writeIndex">A function to write an index.</param>
let private writeBoneConnections (boneConnections : BoneConnection[]) writeIndex =
    boneConnections
    |> Array.iter (fun boneConnection -> writeIndex boneConnection.ExternalFileListIndex
                                         writeIndex boneConnection.ExternalFileListIndexFrdv
                                         writeIndex nullValue
                                         writeIndex nullValue
                                         writeIndex boneConnection.ExternalFileListIndexSim
                                         writeIndex nullValue)

/// <summary>
/// Writes an array of CNPConnections to a file.
/// </summary>
/// <param name="CNPConnections">The CNPConnections to write.</param>
/// <param name="writeHash">A function to write an StrCode32Hash.</param>
/// <param name="writeIndex">A function to write an index.</param>
let private writeCNPConnections (CNPConnections : CNPConnection[]) writeHash writeIndex =
    CNPConnections
    |> Array.iter (fun CNPConnection ->  writeHash CNPConnection.CNPHash
                                         writeHash CNPConnection.EmptyHash
                                         writeIndex CNPConnection.ExternalFileListIndex
                                         writeIndex CNPConnection.ExternalFileListIndexFrdv
                                         writeIndex nullValue
                                         writeIndex nullValue
                                         writeIndex CNPConnection.ExternalFileListIndexSim
                                         writeIndex nullValue)

/// <summary>
/// Writes an array of StrCode64Hashes to a file.
/// </summary>
/// <param name="hashes">The hashes to write.</param>
/// <param name="writeHash">A function to write an StrCode64Hash.</param>
let writeStrCode64Hashes hashes writeHash =
    hashes
    |> Array.iter (fun hash -> writeHash hash)

/// <summmary>
/// Input functions to the Write function.
/// </summmary>
type public WriteFunctions = {
    /// Function to write a uint16.
    WriteUInt16 : Action<uint16>
    /// Function to write a uint32.
    WriteUInt32 : Action<uint32>
    /// Function to write a uint64.
    WriteUInt64 : Action<uint64>
    /// Function to write a byte.
    WriteByte : Action<byte>
    /// Function to write a number of bytes.
    WriteBytes : Action<byte[]>
    /// Function to write a number of chars.
    WriteChars : Action<char[]>
    /// Function to write a number of filler bytes.
    WriteEmptyBytes : Action<int>
}

/// <summmary>
/// Write parameters converted to F# functions.
/// </summmary>
type private ConvertedWriteFunctions = {
    WriteUInt16 : uint16 -> unit
    WriteUInt32 : uint32 -> unit
    WriteUInt64 : uint64 -> unit
    WriteByte : byte -> unit
    WriteBytes : byte[] -> unit
    WriteChars : char[] -> unit
    WriteEmptyBytes : int32 -> unit
}

/// <summmary>
/// Converts the Write function's .NET Funcs into F# functions.
/// </summmary>
/// <param name="rawWriteFunctions">Input functions supplied to the Write function.</param>
/// <returns>The converted functions.</returns>
let private convertWriteFunctions (rawWriteFunctions : WriteFunctions) =
    if rawWriteFunctions.WriteUInt16 |> isNull then nullArg "WriteUInt16"
    if rawWriteFunctions.WriteUInt32 |> isNull then nullArg "WriteUInt32"
    if rawWriteFunctions.WriteUInt64 |> isNull then nullArg "WriteUInt64"
    if rawWriteFunctions.WriteByte |> isNull then nullArg "WriteByte"
    if rawWriteFunctions.WriteBytes |> isNull then nullArg "WriteBytes"
    if rawWriteFunctions.WriteChars |> isNull then nullArg "WriteChars"
    if rawWriteFunctions.WriteEmptyBytes |> isNull then nullArg "WriteEmptyBytes"

    { WriteUInt16 = rawWriteFunctions.WriteUInt16.Invoke;
    WriteUInt32 = rawWriteFunctions.WriteUInt32.Invoke;
    WriteUInt64 = rawWriteFunctions.WriteUInt64.Invoke;
    WriteByte = rawWriteFunctions.WriteByte.Invoke;
    WriteBytes = rawWriteFunctions.WriteBytes.Invoke;
    WriteChars = rawWriteFunctions.WriteChars.Invoke;
    WriteEmptyBytes = rawWriteFunctions.WriteEmptyBytes.Invoke; }

/// <summary>
/// Writes a FormVariation to fv2 format.
/// </summary>
/// <param name="formVariation">The form variation to write.</param>
/// <param name="writeFunctions">Functions to write various data types.</param>
let public Write (formVariation : FormVariation) writeFunctions = 
    let convertedWriteFunctions = convertWriteFunctions writeFunctions

    let materialInstance = makeMaterialInstances formVariation.TextureSwaps

    let boneConnections = makeBoneConnections formVariation.BoneAttachments

    let CNPConnections = makeCNPConnections formVariation.CNPAttachments

    let header = calculateHeader formVariation (*(formVariation.HiddenMeshGroups.Length |> byte) (formVariation.ShownMeshGroups.Length |> byte) (formVariation.TextureSwaps.Length |> byte) (formVariation.BoneAttachments.Length |> byte) (formVariation.CNPAttachments.Length |> byte) (formVariation.TextureSwaps.Length |> uint16)*)

    writeHeader header convertedWriteFunctions.WriteChars convertedWriteFunctions.WriteUInt16 convertedWriteFunctions.WriteUInt16 convertedWriteFunctions.WriteByte convertedWriteFunctions.WriteEmptyBytes

    writeStrCode32Hashes formVariation.HiddenMeshGroups convertedWriteFunctions.WriteUInt32

    writeStrCode32Hashes formVariation.ShownMeshGroups convertedWriteFunctions.WriteUInt32

    writeMaterialInstances materialInstance convertedWriteFunctions.WriteUInt32 convertedWriteFunctions.WriteUInt16

    writeBoneConnections boneConnections convertedWriteFunctions.WriteUInt16

    writeCNPConnections CNPConnections convertedWriteFunctions.WriteUInt32 convertedWriteFunctions.WriteUInt16

    writeStrCode64Hashes (List.toArray fileList) convertedWriteFunctions.WriteUInt64