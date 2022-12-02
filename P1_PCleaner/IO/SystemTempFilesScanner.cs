﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using P1_PCleaner.Model;
using P1_PCleaner.Repository;

namespace P1_PCleaner.IO;

public class SystemTempFilesScanner : IScanner
{
    public IEnumerable<FileInf> Scan()
    {
        var files = new List<FileInf>();
        DirectoryInfo directory;
        string[] ext;

        // Memory Dumps
        directory = new DirectoryInfo(@"C:\Windows");
        ext = new[] { ".DMP" };
        files.AddRange(IScanner.GetFiles(directory, ext, false));
        directory = new DirectoryInfo(
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\CrashDumps");
        ext = new[] { ".dmp" };
        files.AddRange(IScanner.GetFiles(directory, ext, false));
        directory = new DirectoryInfo(
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\AppData\Local\Temp");
        ext = new[] { "*" };

        // Temp files
        files.AddRange(IScanner.GetFiles(directory, ext));

        return files;
    }

    public IFilesRepository.ScanCategory GetScanCategory()
    {
        return IFilesRepository.ScanCategory.TempFiles;
    }
}