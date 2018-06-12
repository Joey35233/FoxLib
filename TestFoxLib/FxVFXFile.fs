module TestFoxLib.Fx.Vfx

open System
open System.IO
open FoxLib.Core
open FoxLib.Fx.Vfx
open NUnit.Framework

let private readString (reader : BinaryReader) count =
    let string = new string (reader.ReadChars (int count - 1))
    reader.BaseStream.Position <- 1L
    string

let private createReadFunctions (reader : BinaryReader) =
    { ReadFunctions.ReadUInt8 = new Func<uint8>(reader.ReadByte);
    ReadUInt16 = new Func<uint16>(reader.ReadUInt16);
    ReadInt32 = new Func<int32>(reader.ReadInt32);
    ReadUInt32 = new Func<uint32>(reader.ReadUInt32);
    ReadUInt64 = new Func<uint64>(reader.ReadUInt64);
    ReadSingle = new Func<float32>(reader.ReadSingle);
    ReadDouble = new Func<float>(reader.ReadDouble);
    ReadBool = new Func<bool>(reader.ReadBoolean);
    ReadString = new Func<uint32, string>(fun length -> readString reader length);
    SkipBytes = new Action<int>(fun numBytes -> reader.ReadBytes numBytes |> ignore); }

//let private createWriteFunctions (writer : BinaryWriter) =
//    { WriteFunctions.WriteBool = new Action<bool>(writer.Write);
//    WriteFunctions.WriteInt8 = new Action<int8>(writer.Write);
//    WriteFunctions.WriteUInt8 = new Action<uint8>(writer.Write);
//    WriteFunctions.WriteInt16 = new Action<int16>(writer.Write);
//    WriteFunctions.WriteUInt16 = new Action<uint16>(writer.Write);
//    WriteFunctions.WriteInt32 = new Action<int32>(writer.Write);
//    WriteFunctions.WriteUInt32 = new Action<uint32>(writer.Write);
//    WriteFunctions.WriteInt64 = new Action<int64>(writer.Write);
//    WriteFunctions.WriteUInt64 = new Action<uint64>(writer.Write);
//    WriteFunctions.WriteSingle = new Action<float32>(writer.Write);
//    WriteFunctions.WriteDouble = new Action<double>(writer.Write);
//    WriteFunctions.WriteBytes = new Action<byte[]>(writer.Write);
//    WriteFunctions.GetStreamPosition = new Func<int64>((fun _ -> writer.BaseStream.Position));
//    WriteFunctions.SetStreamPosition = new Action<int64>((fun position -> writer.BaseStream.Position <- position));
//    WriteFunctions.WriteZeroes = new Action<uint32>(writeZeroes writer);
//    WriteFunctions.AlignWrite = new Action<int, byte>((fun alignment data -> alignWrite writer.BaseStream alignment data)) }

//[<Test>]
//[<Category("VFXFile")>]
//let ``one random VFX file should have original value when read`` () =
    //let random = new System.Random()
    //let randomPixels = [|1..8192|]
    //                    |> Array.map (fun _ -> createRandomPixel random)
    
    //use stream = new MemoryStream()
    //use writer = new BinaryWriter(stream)
    //createWriteFunctions writer
    //|> FoxLib.PrecomputedSkyParameters.Write randomPixels
    //|> ignore
        
    //stream.Position <- 0L

    //use reader = new BinaryReader(stream)
    //let test = createReadFunctions reader
    //           |> FoxLib.Fx.Vfx.Read

    //(test = randomPixels) |> Assert.IsTrue

[<Test>]
[<Category("VFXFile")>]
let ``read and then written "test.vfx" should have original value when read`` () =
    let baseDirectory = __SOURCE_DIRECTORY__

    let originalFilePath = Path.Combine(baseDirectory, "test.vfx")
    use originalReadStream = new FileStream(originalFilePath, FileMode.Open)
    use originalReader = new BinaryReader(originalReadStream)
    
    let originalFile = createReadFunctions originalReader
                       |> Read

    originalReader.Close()

    //let newFilePath = Path.Combine(baseDirectory, "test repacked.vfx")
    //use newWriteStream = new FileStream(newFilePath, FileMode.Create)
    //use newWriter = new BinaryWriter(newWriteStream)

    //createWriteFunctions newWriter
    //|> Write originalFile
    //|> ignore

    //newWriter.Close()

    //use newReadStream = new FileStream(newFilePath, FileMode.Open)
    //use newReader = new BinaryReader(newReadStream)

    //let newFile = createReadFunctions newReader
    //              |> Read

    //newReader.Close()

    //(originalFile = newFile) |> Assert.IsTrue