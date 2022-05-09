namespace Mimirorg.Common.Abstract
{
    public interface IDynamicImageDataProvider
    {
        /// <summary>
        /// Get symbol by id
        /// </summary>
        /// <param name="id">The id that resolves the image symbol</param>
        /// <returns>A base64 string value</returns>
        Task<string> GetSymbolDataAsync(string id);
    }
}
