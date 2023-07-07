using FileSystem.Models;
using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace FileSystem.Data
{
    public class FileSystemDbContext : DbContext
    {
        public FileSystemDbContext(DbContextOptions<FileSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Folder> Folders { get; set; }

    }
}