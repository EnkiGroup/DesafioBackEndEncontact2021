using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Services.Interface;

namespace TesteBackendEnContact.Services
{
    public class ContactService : IContactService
    {
        private readonly IConfiguration _configuration;
        public ContactService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<IContactModel>> UpdateFileContact(HttpRequest httpRequest)
        {
            var listContact = new List<Models.ContactService>();
            var path = httpRequest.Form.Files[0].FileName;

            if (path.Contains(".csv"))
            {
                var contactModels = ReaderCsv(path);
                foreach (var contactModel in contactModels)
                {
                    listContact.Add((Models.ContactService)contactModel);
                }
            }
            else
                throw new InvalidOperationException("Formato de arquivo não suportado.");

            return listContact;
        }

        private IEnumerable<IContactModel> ReaderCsv(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var line = string.Empty;
                string[] lineSplit = null;
                var numberLine = 0;

                while ((line = reader.ReadLine()) is not null)
                {
                    lineSplit = line.Split(';');

                    var contactService = new Models.ContactService
                    {
                        Name = lineSplit[0],
                        Phone = lineSplit[1],
                        Email = lineSplit[2],
                        Address = lineSplit[3],
                        NameCompany = lineSplit[4],
                    };

                    yield return contactService.ToContact();
                }
            }
        }

        public async Task<string> ModelCsv()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ModeloCSV\";
            var fileName = "Modelo CSV.csv";
            var modelCsv = new FileStream(path + fileName, FileMode.Create, FileAccess.ReadWrite);

            using (var Csv = new StreamWriter(modelCsv, encoding: System.Text.Encoding.UTF8))
            {
                #region .: Modelo CSV :.
                Csv.WriteLine("Name;Phone;Email;Address;NameCompany");
                #endregion
            }

            return path + fileName;
        }
    }
}
