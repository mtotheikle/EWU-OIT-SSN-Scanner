using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.COM;
using Nomad.Archive.SevenZip;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SevenZip
{
    class SevenZipFunctions
    {
    public static void SevenZipper(string fileName, uint level)
    {
      try
      {
          //ListOrExtract(fileName, false, 0xFFFFFFFF);// toggle true to extract, false to just list contents of archive
          ListOrExtract(fileName, level);
      }
      catch (Exception e)
      {
          Console.Write("Error: ");
          Console.WriteLine(e.Message);
      }
    }

    private static void ListOrExtract(string archiveName, uint level)
    {
        using (SevenZipFormat Format = new SevenZipFormat(SevenZipDllPath))
        {
            IInArchive Archive = Format.CreateInArchive(SevenZipFormat.GetClassIdFromKnownFormat(KnownSevenZipFormat.Zip));
            if (Archive == null)
                return;

            try
            {
                using (InStreamWrapper ArchiveStream = new InStreamWrapper(File.OpenRead(archiveName)))
                {
                    IArchiveOpenCallback OpenCallback = new ArchiveOpenCallback();

                    // 32k CheckPos is not enough for some 7z archive formats
                    ulong CheckPos = 128 * 1024;
                    if (Archive.Open(ArchiveStream, ref CheckPos, OpenCallback) != 0)
                        //ShowHelp();
                    if (!Directory.Exists(@"tmp_dir"))
                        Directory.CreateDirectory(@"tmp_dir");

                    //Console.WriteLine(archiveName);

                    uint Count = Archive.GetNumberOfItems();
                    for (uint I = 0; I < Count; I++)
                    {
                        PropVariant Name = new PropVariant();
                        Archive.GetProperty(I, ItemPropId.kpidPath, ref Name);
                        string FileName = (string)Name.GetObject();
                        Program.f1.label1.Text = FileName;
                        Application.DoEvents();
                        for(int i = 0; i < level; i++)
                            Console.Write("\t");
                        Console.Write(FileName + "\n");
                        FileName += level;
                        Archive.Extract(new uint[] { I }, 1, 0, new ArchiveExtractCallback(I, FileName));
                        Program.Determine_Parser(new System.IO.FileInfo("tmp_dir//" + FileName), level);
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(Archive);
                Directory.Delete(@"tmp_dir", true);
            }
        }
    }

    private static string SevenZipDllPath
    {
      get { return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "7z.dll"); }
    }

    // Some 7z formats require this empty callback (some not)
    private class ArchiveOpenCallback : IArchiveOpenCallback
    {
      public void SetTotal(IntPtr files, IntPtr bytes)
      {
      }

      public void SetCompleted(IntPtr files, IntPtr bytes)
      {
      }
    }

    private class ArchiveExtractCallback : IProgress, IArchiveExtractCallback
    {
      private uint FileNumber;
      private string FileName;
      private OutStreamWrapper FileStream;

      public ArchiveExtractCallback(uint fileNumber, string fileName)
      {
        this.FileNumber = fileNumber;
        this.FileName = "tmp_dir\\" + fileName;
      }

      #region IProgress Members

      public void SetTotal(ulong total)
      {
      }

      public void SetCompleted(ref ulong completeValue)
      {
      }

      #endregion

      #region IArchiveExtractCallback Members

      public int GetStream(uint index, out ISequentialOutStream outStream, AskMode askExtractMode)
      {
        if ((index == FileNumber) && (askExtractMode == AskMode.kExtract))
        {
          string FileDir = Path.GetDirectoryName(FileName);
          if (!string.IsNullOrEmpty(FileDir))
            Directory.CreateDirectory(FileDir);

          FileStream = new OutStreamWrapper(File.Create(FileName));

          outStream = FileStream;
        }
        else
          outStream = null;

        return 0;
      }

      public void PrepareOperation(AskMode askExtractMode)
      {
      }
      //commented out result OP - gs
      public void SetOperationResult(OperationResult resultEOperationResult)
      {
        FileStream.Dispose();
        //Console.WriteLine(resultEOperationResult);
      }

      #endregion
    }

    private class ArchiveUpdateCallback : IProgress, IArchiveUpdateCallback
    {
      private IList<FileInfo> FileList;
      private Stream CurrentSourceStream;

      public ArchiveUpdateCallback(IList<FileInfo> list)
      {
        FileList = list;
      }

      #region IProgress Members

      public void SetTotal(ulong total)
      {
      }

      public void SetCompleted(ref ulong completeValue)
      {
      }

      #endregion

      #region IArchiveUpdateCallback Members

      public void GetUpdateItemInfo(int index, out int newData, out int newProperties, out uint indexInArchive)
      {
        newData = 1;
        newProperties = 1;
        indexInArchive = 0xFFFFFFFF;
      }

      private void GetTimeProperty(DateTime time, IntPtr value)
      {
        Marshal.GetNativeVariantForObject(time.ToFileTime(), value);
        Marshal.WriteInt16(value, (short)VarEnum.VT_FILETIME);
      }

      public void GetProperty(int index, ItemPropId propID, IntPtr value)
      {
        FileInfo Source = FileList[index];
        switch (propID)
        {
          case ItemPropId.kpidPath:
            Marshal.GetNativeVariantForObject(Path.GetFileName(Source.FullName), value);
            break;
          case ItemPropId.kpidIsFolder:
          case ItemPropId.kpidIsAnti:
            Marshal.GetNativeVariantForObject(false, value);
            break;
          //case ItemPropId.kpidAttributes:
          //  Marshal.WriteInt16(value, (short)VarEnum.VT_EMPTY);
          //  break;
          case ItemPropId.kpidCreationTime:
            GetTimeProperty(Source.CreationTime, value);
            break;
          case ItemPropId.kpidLastAccessTime:
            GetTimeProperty(Source.LastAccessTime, value);
            break;
          case ItemPropId.kpidLastWriteTime:
            GetTimeProperty(Source.LastWriteTime, value);
            break;
          case ItemPropId.kpidSize:
            Marshal.GetNativeVariantForObject((ulong)Source.Length, value);
            break;
          default:
            Marshal.WriteInt16(value, (short)VarEnum.VT_EMPTY);
            break;
        }
      }

      public void GetStream(int index, out ISequentialInStream inStream)
      {
        FileInfo Source = FileList[index];

        Console.Write("Packing: ");
        Console.Write(Path.GetFileName(Source.FullName));
        Console.Write(' ');

        CurrentSourceStream = Source.OpenRead();
        inStream = new InStreamTimedWrapper(CurrentSourceStream);
      }

      public void SetOperationResult(int operationResult)
      {
        CurrentSourceStream.Close();
        Console.WriteLine("Ok");
      }

      #endregion
    }
  }
}
