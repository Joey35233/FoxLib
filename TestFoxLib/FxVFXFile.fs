module TestFoxLib.Fx.Vfx

open System
open System.IO
open FoxLib.Core
open FoxLib.Fx.Vfx
open NUnit.Framework

let private readString (reader : BinaryReader) count =
    let string = new string (reader.ReadChars (int count))
    reader.BaseStream.Position <- reader.BaseStream.Position + 1L
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

[<Test>]
[<Category("VFXFile")>]
let ``reading "test.vfx" is successful`` () =
    let baseDirectory = __SOURCE_DIRECTORY__

    let originalFilePath = Path.Combine(baseDirectory, "test.vfx")
    use originalReadStream = new FileStream(originalFilePath, FileMode.Open)
    use originalReader = new BinaryReader(originalReadStream)

    originalReader.BaseStream.Position <- 0L
    
    let originalFile = createReadFunctions originalReader
                       |> Read

    originalReader.Close()

    true |> Assert.IsTrue