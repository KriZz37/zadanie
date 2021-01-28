using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zadanie.Entities;
using zadanie.Models;
using zadanie.ViewModels;

namespace zadanie.Services
{
    public class TreeService
    {
        private readonly AppDbContext dbContext;

        public TreeService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public HomeViewModel GetTree()
        {
            var files = dbContext.Files
                .ToList()
                .Select(x => new FileViewModel
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    FormId = x.FormId,
                    Name = x.Name,
                    IsLast = x.Subfiles.Count == 0
                })
                .OrderBy(x => x.Name)
                .OrderByDescending(x => x.FormId);

            var vm = new HomeViewModel
            {
                SeededFiles = new SeededFilesViewModel
                {
                    Files = files,
                    Seed = null
                }
            };

            return vm;
        }

        public string AddNewFolder(NewFolderViewModel data)
        {
            var parentFolder = dbContext.Files
                .Where(x => x.FormId == null)
                .SingleOrDefault(x => x.Id == data.ParentId);
            var parentFolderIsRoot = data.ParentId == 0;
            if (parentFolder == null && parentFolderIsRoot == false)
            {
                var folderExistsError = "Folder 'Rodzic' nie istnieje";
                return folderExistsError;
            }

            if (string.IsNullOrWhiteSpace(data.Name))
            {
                var nameError = "Nazwa nie może być pusta";
                return nameError;
            }

            var newFolder = new File
            {
                ParentId = data.ParentId == 0 ? null : data.ParentId,
                Form = null,
                Name = data.Name,
            };

            dbContext.Files.Add(newFolder);
            dbContext.SaveChanges();
            return null;
        }

        public string MoveFolder(MoveFolderViewModel data)
        {
            var moveToItself = data.FolderId == data.NewParentId;
            if (moveToItself)
            {
                var moveToItselfError = "Nie możesz przenieść pliku do samego siebie";
                return moveToItselfError;
            }

            var folder = dbContext.Files
                .AsEnumerable()
                .Single(x => x.Id == data.FolderId);

            var parentFolderIsRoot = data.NewParentId == 0;
            if (parentFolderIsRoot)
            {
                folder.ParentId = null;
                dbContext.SaveChanges();
                return null;
            }

            var parentFolder = dbContext.Files
                    .Where(x => x.FormId == null)
                    .SingleOrDefault(x => x.Id == data.NewParentId);
            if (parentFolder == null)
            {
                var folderExistsError = "Folder 'Nowy rodzic' nie istnieje";
                return folderExistsError;
            }

            var flattenedSubfolderIdsList = FlattenSubfolderIds(folder);
            if (flattenedSubfolderIdsList.Contains(parentFolder.Id))
            {
                var moveInsideError = "Nie możesz przenieść pliku do jego wnętrza";
                return moveInsideError;
            }

            folder.ParentId = data.NewParentId;
            dbContext.SaveChanges();
            return null;
        }

        public IEnumerable<long> FlattenSubfolderIds(File root)
        {
            var stack = new Stack<File>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var currentFolder = stack.Pop();

                yield return currentFolder.Id;

                foreach (var subfile in currentFolder.Subfiles)
                {
                    if (subfile.FormId == null)
                    {
                        stack.Push(subfile);
                    }
                }
            }
        }

        public string ChangeFileName(ChangeFileNameViewModel data)
        {
            if (string.IsNullOrWhiteSpace(data.NewFileName))
            {
                var nameError = "Nazwa nie może być pusta";
                return nameError;
            }

            var file = dbContext.Files.Single(x => x.Id == data.FileId);

            file.Name = data.NewFileName;
            dbContext.SaveChanges();
            return null;
        }

        public void DeleteFile(long id)
        {
            var file = dbContext.Files
                .Include(x => x.Form)
                .AsEnumerable()
                .Single(x => x.Id == id);

            DeleteSubfiles(file);
            if (file.Form != null)
            {
                dbContext.Forms.Remove(file.Form);
            }
            dbContext.Files.Remove(file);
            dbContext.SaveChanges();
        }

        private void DeleteSubfiles(File root)
        {
            foreach (var subfile in root.Subfiles.ToList())
            {
                if (subfile.Subfiles.Count > 0)
                {
                    DeleteSubfiles(subfile);
                }

                if (subfile.Form != null)
                {
                    dbContext.Forms.Remove(subfile.Form);
                }

                dbContext.Files.Remove(subfile);
            }
        }
    }
}
