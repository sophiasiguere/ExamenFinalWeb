using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Text;
using Examen_final.Models;
using Examen_final.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Examen_final.Services;

public static class ApiService
{
    private static HttpClient _client = new();
    private static HttpClientHandler _clientHandler = new();
    private static string url = "https://localhost:7037/api/";

    public static string token = "";

    private static async Task<T> Get<T>(string path)
    {
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.GetAsync(path);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Post<T>(string path, object? data)
    {
        var json_ = JsonConvert.SerializeObject(data);
        var content = new StringContent(json_, Encoding.UTF8, "application/json");
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.PostAsync(path, content);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Put<T>(string path, object? data)
    {
        var json_ = JsonConvert.SerializeObject(data);
        var content = new StringContent(json_, Encoding.UTF8, "application/json");
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.PutAsync(path, content);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    private static async Task<T> Delete<T>(string path)
    {
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        if (!token.IsNullOrEmpty())
        {
            _client.DefaultRequestHeaders.Authorization = null;
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        var response = await _client.DeleteAsync(path);
        int statusCode = (int)response.StatusCode;
        if (statusCode >= 200 && statusCode < 300)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync())!;
        }
        else
        {
            throw new Exception(response.StatusCode.ToString());
        }
    }

    //Movies

    public static async Task<IEnumerable<Bicicleta>> GetBicicleta()
    {
        return await Get<IEnumerable<Bicicleta>>(url + "bicicletas");
    }

    public static async Task<Bicicleta> BicicletaDetails(int? id)
    {
        return await Get<Bicicleta>(url + "bicicletas/" + id);
    }

    public static async Task<GeneralResult> CreateBicicleta(Bicicleta bicicleta)
    {
        return await Post<GeneralResult>(url + "bicicletas", bicicleta);
    }

    public static async Task<GeneralResult> UpdateBicicleta(Bicicleta bicicleta)
    {
        return await Put<GeneralResult>(url + "bicicletas/" + bicicleta.Id, bicicleta);
    }

    public static async Task<GeneralResult> DeleteBicicleta(int id)
    {
        return await Delete<GeneralResult>(url + "bicicletas/" + id);
    }

    public static async Task<UserToken?> Login(UserAuth credentials)
    {
        return await Post<UserToken?>(url + "auth/login", credentials);
    }

}
