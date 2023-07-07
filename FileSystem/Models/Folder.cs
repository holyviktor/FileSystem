using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FileSystem.Models
{
    public class Folder
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Folder? ParentFolder { get; set; }
        public List<Folder>? SubFolders { get; set; }

    }
}
