namespace Mimirorg.Common.Abstract
{
    public interface IDynamicLogoDataProvider
    {
        /// <summary>
        /// Get company logo by id
        /// </summary>
        /// <param name="id">The id that resolves the image data</param>
        /// <returns>A base64 string value</returns>
        Task<string> GetLogoDataAsync(int id);
    }
}