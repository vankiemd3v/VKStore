using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKStore.ViewModels.Catalog.Contacts
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
      
        public string Email { get; set; }
     
        public string Subject { get; set; }
       
        public string Content { get; set; }
    }
}
