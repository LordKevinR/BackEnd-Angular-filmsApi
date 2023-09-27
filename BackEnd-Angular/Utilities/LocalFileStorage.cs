namespace BackEnd_Angular.Utilities
{
	public class LocalFileStorage : IAzureStorage
	{
		private readonly IWebHostEnvironment env;
		private readonly IHttpContextAccessor httpContextAccessor;

		public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
			this.env = env;
			this.httpContextAccessor = httpContextAccessor;
		}

        public Task DeleteFile(string route, string content)
		{
			if ( string.IsNullOrEmpty(route))
			{
				return Task.CompletedTask;
			}

			var fileName = Path.GetFileName(route);
			var fileDirectory = Path.Combine(env.WebRootPath, content, fileName);

			if (File.Exists(fileDirectory))
			{
				File.Delete(fileDirectory);
			}

			return Task.CompletedTask;
		}

		public async Task<string> EditFile(string content, IFormFile file, string route)
		{
			await DeleteFile(route, content);
			return await SaveFile(content, file);
		}

		public async Task<string> SaveFile(string container, IFormFile file)
		{
			var extension = Path.GetExtension(file.FileName);
			var fileName = $"{Guid.NewGuid()}{extension}";
			string folder = Path.Combine(env.WebRootPath, container);

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			string route = Path.Combine(folder, fileName);

			using (var memoryStream = new MemoryStream())
			{
				await file.CopyToAsync(memoryStream);
				var content = memoryStream.ToArray();
				await File.WriteAllBytesAsync(route, content);
			}

			var actualUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
			var routeForDb = Path.Combine(actualUrl, container, fileName).Replace("\\", "/");
			return routeForDb;
		}
	}
}
