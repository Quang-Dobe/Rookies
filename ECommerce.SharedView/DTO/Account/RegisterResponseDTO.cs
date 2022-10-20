using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedView.DTO.Account
{
    public class RegisterResponseDTO
    {
        public object Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
