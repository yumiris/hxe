using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using SPV3.Compiler.Common;
using Directory = SPV3.Domain.Directory;
using File = SPV3.Domain.File;

namespace SPV3.Compiler.Compressors
{
    /// <inheritdoc />
    public class InternalCompressor : Compressor
    {
        /// <inheritdoc />
        public override void Compress(File target, Directory source)
        {
            ZipFile.CreateFromDirectory(source, target);
        }

        /// <inheritdoc />
        public override void Compress(File target, Directory source, IEnumerable<File> whitelist)
        {
            using (var zip = ZipFile.Open(target, ZipArchiveMode.Create))
            {
                const CompressionLevel level = CompressionLevel.Optimal;

                foreach (var file in whitelist)
                    zip.CreateEntryFromFile(Path.Combine(source, file), file, level);
            }
        }
    }
}