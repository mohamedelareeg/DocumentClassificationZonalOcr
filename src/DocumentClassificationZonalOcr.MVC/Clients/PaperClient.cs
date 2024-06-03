using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;
using System.Net.Http.Headers;

namespace DocumentClassificationZonalOcr.MVC.Clients
{
    public class PaperClient : BaseClient, IPaperClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaperClient(IHttpClientFactory httpClientFactory, ILogger<PaperClient> logger, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BaseResponse<bool>> AddPaperToFormAsync(int formId, PaperRequestDto paper)
        {
            return await PostAsync<PaperRequestDto, BaseResponse<bool>>($"api/Paper/add/{formId}", paper);
        }

        public async Task<BaseResponse<PaperDto>> GetPaperByIdAsync(int paperId)
        {
            return await GetAsync<BaseResponse<PaperDto>>($"api/Paper/get/{paperId}");
        }

        public async Task<BaseResponse<List<PaperDto>>> GetAllPapersByFormIdAsync(int formId)
        {
            return await GetAsync<BaseResponse<List<PaperDto>>>($"api/Paper/form/{formId}");
        }

        public async Task<BaseResponse<List<PaperDto>>> GetAllPapersByDocumentIdAsync(int documentId)
        {
            return await GetAsync<BaseResponse<List<PaperDto>>>($"api/Paper/document/{documentId}");
        }

        public async Task<BaseResponse<ExportedMetaDataDto>> GetAllPaperMetadataAsync(int paperId)
        {
            return await GetAsync<BaseResponse<ExportedMetaDataDto>>($"api/Paper/metadata/{paperId}");
        }

        public async Task<BaseResponse<bool>> ProcessImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BaseResponse<bool>.CreateNullDataResponse("Invalid image");
            }

            var uploadUrl = "api/Paper/process-image";
            using var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            using var stream = image.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

            using var formData = new MultipartFormDataContent();
            formData.Add(fileContent, "image", image.FileName);

            var response = await client.PostAsync(uploadUrl, formData);
            return response.IsSuccessStatusCode ? BaseResponse<bool>.Success(true) : BaseResponse<bool>.CreateNullDataResponse("Failed to process image");
        }

        public async Task<BaseResponse<bool>> ProcessImagesAsync(List<IFormFile> images)
        {
            if (images == null || images.Count == 0)
            {
                return BaseResponse<bool>.CreateNullDataResponse("No images provided");
            }

            var uploadUrl = "api/Paper/process-images";
            using var client = _httpClientFactory.CreateClient("ApiClient");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            var formData = new MultipartFormDataContent();
            try
            {
                foreach (var image in images)
                {
                    if (image == null || image.Length == 0)
                    {
                        continue; // Skip empty or invalid images
                    }

                    var stream = image.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

                    formData.Add(fileContent, "images", image.FileName);
                }

                var response = await client.PostAsync(uploadUrl, formData);
                if (response.IsSuccessStatusCode)
                {
                    return BaseResponse<bool>.Success(true);
                }
                else
                {
                    return BaseResponse<bool>.CreateNullDataResponse("Failed to process images");
                }
            }
            finally
            {
                foreach (var content in formData)
                {
                    if (content is StreamContent streamContent)
                    {
                        await streamContent.ReadAsStreamAsync().ContinueWith(t => t.Result.Dispose());
                    }
                }
                formData.Dispose();
            }
        }
        public async Task<BaseResponse<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, BaseResponse<CustomList<PaperDto>>>("api/Paper/all", options);
        }



    }
}
