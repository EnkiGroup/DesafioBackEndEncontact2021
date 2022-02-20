using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.File;
using TesteBackendEnContact.Core.Interface.Data;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;

namespace TesteBackendEnContact.Services
{
    public class DataService : IDataService
    {
        private readonly IContactBookRepository _contactBookRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DataService(IContactBookRepository contactBookRepository, IContactRepository contactRepository, IWebHostEnvironment webHostEnvironment)
        {
            _contactBookRepository = contactBookRepository;
            _contactRepository = contactRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> UploadFileContact(UploadFile file)
        {
            //var contacts = new List<Models.DataModel>();
            //var path = CreateFile(file);
            //var data = File.ReadLines(path);
            //foreach (var line in data)
            //{
            //    Models.DataModel contactModel = line;

            //    contacts.Add(new Models.DataModel
            //    {
            //        Name = contactModel.Name,
            //        Phone = contactModel.Phone,
            //        Email = contactModel.Email,
            //        Address = contactModel.Address,
            //        NameCompany = contactModel.NameCompany
            //    });
            //}
            return false;
        }

        public async Task UpdateFileContact()
        {
            var pathFisic = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\File.csv\";
            //var data = _contactRepository.GetAllAsync();

            //File.WriteAllLines(pathFisic, data.Select(x => (string)x);
        }

        public async Task<string> ModelCsv()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = @"\Modelo.csv";
            var modelCsv = new FileStream(path + fileName, FileMode.Create, FileAccess.ReadWrite);

            using (var Csv = new StreamWriter(modelCsv, encoding: System.Text.Encoding.UTF8))
            {
                #region .: Modelo CSV :.
                Csv.WriteLine("Name;Phone;Email;Address;NameCompany");
                #endregion
            }

            return path + fileName;
        }

        private string CreateFile(UploadFile file)
        {
            if (file.File.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\File\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\File\\");
                    }
                    using (FileStream fileStream = File.Create(_webHostEnvironment.WebRootPath + "\\File\\" + file.File.FileName))
                    {
                        file.File.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _webHostEnvironment.WebRootPath + "\\File\\" + file.File.FileName;
        }

        public async Task CadastroContatct(IEnumerable<IDataModel> data)
        {
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.Name))
                {
                    var idContactBook = _contactBookRepository.InsertContactBook(item.Name);
                }
            }
        }
    }
}
