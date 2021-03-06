﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Neo.IronLua;
using ZkData;

namespace PlasmaShared.UnitSyncLib
{
    public class ArchiveCache
    {
        public static FileInfo GetCacheFile(string writableFolder) {
            var di = new DirectoryInfo(Path.Combine(writableFolder, "cache"));
            return di.GetFiles("ArchiveCache*.lua", SearchOption.AllDirectories).OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
        }
            

        public ArchiveCache(string unitsyncWritableFolder) {
            Archives = new List<ResourceInfo>();

            var fi = GetCacheFile(unitsyncWritableFolder);
            if (fi != null)
            {
                var lua = new Lua();
                var luaEnv = lua.CreateEnvironment();
                using (var file = fi.OpenText())
                {
                    dynamic result = luaEnv.DoChunk(file, "dummy.lua");
                    foreach (var archive in result.archives)
                    {
                        var v = archive.Value;

                        if (v.archivedata != null)
                        {

                            var newEntry = new ResourceInfo()
                            {
                                ArchiveName = v.name,
                                ArchiveFolder = v.path,
                                Name = v.archivedata.name,
                                Author = v.archivedata.author,
                                Description = v.archivedata.description,
                                Mutator = v.mutator,
                                ShortGame = v.shortgame,
                                Game = v.game,
                                ShortName = v.shortname,
                                PrimaryModVersion = v.version,
                            };
                            if (v.archivedata.modtype != null) newEntry.ModType = v.archivedata.modtype;
                            if (v.checksum != null)
                            {
                                uint temp;
                                if (uint.TryParse(v.checksum, out temp)) newEntry.CheckSum = temp;
                            }
                            if (v.archivedata.depend != null) foreach (var dep in v.archivedata.depend) newEntry.Dependencies.Add(dep.Value);

                            Archives.Add(newEntry);
                        }
                    }
                }
            } else Trace.TraceError("Cannot locate engine archive cache in {0}", unitsyncWritableFolder);
        }

        public List<ResourceInfo> Archives { get; }
    }
}