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
    public class FormService
    {
        private readonly AppDbContext dbContext;

        public FormService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public long Create(NewFormViewModel data)
        {
            var parentFolder = dbContext.Files
                .Where(x => x.FormId == null)
                .SingleOrDefault(x => x.Id == data.ParentId);
            var parentFolderIsRoot = data.ParentId == 0;
            if (parentFolder == null && parentFolderIsRoot == false)
            {
                var existError = new Exception("Folder 'Rodzic' nie istnieje");
                throw existError;
            }

            if (string.IsNullOrWhiteSpace(data.Name))
            {
                var nameError = new Exception("Nazwa nie może być pusta");
                throw nameError;
            }

            var newFile = new File
            {
                ParentId = data.ParentId == 0 ? null : data.ParentId,
                Name = data.Name,
                Form = new Form()
            };

            dbContext.Files.Add(newFile);
            dbContext.SaveChanges();

            return newFile.Form.Id;
        }

        public EditFormViewModel GetToEdit(long id)
        {
            var form = new Form();

            try
            {
                form = dbContext.Forms
                    .Include(x => x.File)
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                throw new Exception();
            }


            var vm = new EditFormViewModel
            {
                FormId = form.Id,
                Text = form.Text,
                Title = form.Title,
                FileName = form.File.Name
            };

            return vm;
        }

        public void Edit(EditFormViewModel data)
        {
            var form = dbContext.Forms.Single(x => x.Id == data.FormId);

            form.Title = data.Title;
            form.Text = data.Text;
            dbContext.SaveChanges();
        }
    }
}
