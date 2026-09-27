using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ImageMagick;
using wowmenfashions.Data;
using wowmenfashions.Data.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace wowmenfashions.Services;

public class HomepageImageService : IHomepageImageService
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "HomepageImagesCache";

    public HomepageImageService(ISqlConnectionFactory sqlConnectionFactory, IMemoryCache cache)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _cache = cache;
    }

    public async Task<List<HomepageImage>> GetCachedImagesAsync()
    {
        return await _cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
            using var connection = _sqlConnectionFactory.CreateConnection();
            var images = await connection.QueryAsync<HomepageImage>(
                "SELECT Id, ImageData, ContentType, DisplayOrder FROM HomepageImages ORDER BY DisplayOrder");
            return images.ToList();
        }) ?? new List<HomepageImage>();
    }

    public async Task SaveImagesAsync(List<HomepageImage> images)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync("DELETE FROM HomepageImages", transaction: transaction);
            
            var sql = @"
                INSERT INTO HomepageImages (ImageData, ContentType, DisplayOrder)
                VALUES (@ImageData, @ContentType, @DisplayOrder)";
                
            await connection.ExecuteAsync(sql, images, transaction: transaction);
            transaction.Commit();
            
            // Invalidate and immediately refresh cache
            _cache.Remove(CacheKey);
            await GetCachedImagesAsync();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<byte[]> ProcessImageAsync(Stream fileStream)
    {
        using var memStream = new MemoryStream();
        await fileStream.CopyToAsync(memStream);
        memStream.Position = 0;

        using var image = new MagickImage(memStream);
        image.Format = MagickFormat.Avif;
        // Optimize for web
        image.Quality = 80;
        
        using var outStream = new MemoryStream();
        image.Write(outStream);
        return outStream.ToArray();
    }
}
