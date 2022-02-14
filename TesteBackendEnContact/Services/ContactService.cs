using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.Services.Models;

namespace TesteBackendEnContact.Services
{
    public class ContactService : IContactService
    {
        private readonly IConfiguration _configuration;
        public ContactService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> UpdateFileContact(HttpRequest httpRequest)
        {
            var contact = new List<ContactServiceModel>();
            var path = httpRequest.Form.Files[0].FileName;


            if (path.Contains(".csv"))
            {
                var c = ReaderCsv(path);
                foreach (var item in c)
                {
                    contact.Add((ContactServiceModel)item);
                }
            }

            return true;

        }

        public IEnumerable<IContact> ReaderCsv(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var line = string.Empty;
                string[] lineSplit = null;
                var numberLine = 0;

                while ((line = reader.ReadLine()) is not null)
                {
                    lineSplit = line.Split(';');

                    var contact = new ContactServiceModel
                    {
                        ContactBookId = int.Parse(lineSplit[0]),
                        CompanyId = int.Parse(lineSplit[1]),
                        Name = lineSplit[2],
                        Phone = lineSplit[3],
                        Email = lineSplit[4],
                        Address = lineSplit[5]
                    };

                    yield return contact.ToContact();
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
                Csv.WriteLine("ContactBookId;CompanyId;Name;Phone;Email;Address;");
                #endregion
            }

            return path + fileName;
        }
    }
}
