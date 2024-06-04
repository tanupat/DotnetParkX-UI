using System.Net;

namespace ParkXUI.Utility;

public class ApiResponse
{
    public HttpStatusCode HttpStatus { get; set; }
    public string MessageError { get; set; }
    public string Data { get; set; }

    public ApiResponse(HttpStatusCode httpStatus, string messageError, string data)
    {
        HttpStatus = httpStatus;
        MessageError = messageError;
        Data = data;
    }
    
}