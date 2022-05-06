namespace Mimirorg.Common.Abstract
{
    public interface IDynamicImageDataProvider
    {
        /// <summary>
        /// Get data blob by id
        /// </summary>
        /// <param name="id">The data id that resolves the image blob</param>
        /// <returns>A base64 string value</returns>
        Task<string> GetBlobDataAsync(string id);
    }
}
