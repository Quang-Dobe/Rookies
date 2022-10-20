using System.Net;

namespace ECommerce.SharedView.DTO.Account
{
    public class LoginResponseDTO
    {
        public string Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
