using Mango.Web.Models;
using Mango.Web.Services.IServices;
using MangoServicesCouponAPI.Models.Dto;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                
                    message.Headers.Add("Accept", "application/json");
                
                

                message.RequestUri = new Uri(requestDto.Url);

               
                    if (requestDto.Data != null)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                    }
                HttpResponseMessage? apiResponse = null;
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;

                }
                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSucess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSucess= false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSucess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSucess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }








            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                  Message=ex.Message.ToString(),
                  IsSucess=false,

                };
                return dto;
            }
        }

       
    }
}