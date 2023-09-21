using Azure.Storage.Blobs;

namespace BackEnd_Angular.Utilities
{
	public class AzureStorage : IAzureStorage
	{
		private string connectionString;
		public AzureStorage(IConfiguration configuration)
		{
			connectionString = configuration.GetConnectionString("AzureStorage");
		}

		public async Task<string> SaveFile(string content, IFormFile file)
		{
			var client = new BlobContainerClient(connectionString, content);
			await client.CreateIfNotExistsAsync();
			client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

			var extension = Path.GetExtension(file.FileName);
			var fileName = $"{Guid.NewGuid()}{extension}";
			var blob = client.GetBlobClient(fileName);
			await blob.UploadAsync(file.OpenReadStream());
			return blob.Uri.ToString();
		}

		public async Task DeleteFile(string route, string content)
		{
			if (string.IsNullOrEmpty(route))
			{
				return;
			}

			var client = new BlobContainerClient(connectionString, content);
			await client.CreateIfNotExistsAsync();
			var file = Path.GetFileName(route);
			var blob = client.GetBlobClient(file);
			await blob.DeleteIfExistsAsync();
		}

		public async Task<string> EditFile(string content, IFormFile file, string route)
		{
			await DeleteFile(route, content);
			return await SaveFile(content, file);
		}
	}
}
