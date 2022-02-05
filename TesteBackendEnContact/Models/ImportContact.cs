using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TesteBackendEnContact.Repository;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Models
{
    public class ImportContact
    {
        public static List<Contact> StringToContacts(StringBuilder contactsString, IContactBookRepository contactBookRepository) {

            var cont = contactsString.ToString().Split("\n");

            List<Contact> contacts = new List<Contact>();

            int index=0;

            foreach (string c in cont)
            {
                try
                {
                    index++;
                    string[] values = c.Split(';');

                    if (contactBookRepository.IsInDatabase(int.Parse(values[1])))
                    {

                        contacts.Add(new Contact(
                            int.Parse(values[0]),
                            int.Parse(values[1]),
                            int.Parse(values[2]),
                            values[3],
                            values[4],
                            values[5],
                            values[6]
                            ));
                    }
                }
                catch {
                    Console.WriteLine("Houve um problema na importação do contato de índice {0}", index);
                }
            }

            return contacts;
        }
    }
}
