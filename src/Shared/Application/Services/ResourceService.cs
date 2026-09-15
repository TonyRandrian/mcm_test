using Mcm.Shared.Application.Interfaces;
using Mcm.Shared.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Security.Cryptography;
using Mcm.Shared.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Mcm.Shared.Domain.ValueObjects;

namespace Mcm.Shared.Application.Services;

public class ResourceService(IConfiguration conf, IWebHostEnvironment env)
    : IResourceService
{
    private readonly IConfiguration _conf = conf;
    private readonly IWebHostEnvironment _env = env;


    public async Task<Resource> SaveResource(IFormFile? file, FileType? Type = FileType.Image)
    {
        if (file is null || file.Length == 0)
            throw new BadRequestException("File is empty");

        var ext = Path.GetExtension(file.FileName).ToLower();
        FileType fileType = ext switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp" or ".svg"=> FileType.Image,
            ".pdf" or "docx" => FileType.Document,
            _ => throw new BadRequestException("Invalid file type"),
        };

        if (fileType != Type)
            throw new BadRequestException($"File type mismatch. Expected {Type}, got {fileType}");
        
        string _internStoragePath = _env.ContentRootPath ?? throw new Exception("Web root path not found");
        _internStoragePath = fileType switch
        {
            FileType.Image => Path.Combine(_internStoragePath, _conf.GetSection("FileConfiguration:image:storage").Value ?? throw new BadRequestException("Storage path not found")),
            FileType.Document => Path.Combine(_internStoragePath, _conf.GetSection("FileConfiguration::storage").Value ?? throw new BadRequestException("Storage path not found")),
            _ => throw new Exception("Invalid file type"),
        };

        if (!Directory.Exists(_internStoragePath)) Directory.CreateDirectory(_internStoragePath);

        var fileName = $"{Guid.NewGuid()}{ext}";
        var path = Path.Combine(_internStoragePath, fileName);

        if (!File.Exists(path))
        {
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        return fileType == FileType.Image
            ?  Resource.Image(
                StorageType.System,
                fileName,
                file.FileName,
                file.ContentType
            )
            : Resource.Document(
                StorageType.System,
                fileName,
                file.FileName,
                file.ContentType
            );
    }   

    public void DeleteResource(Resource resource)
    {
        if (resource is null || resource.Url is null || resource.StorageType != StorageType.System)
            return;
        string _internStoragePath = _env.ContentRootPath ?? throw new Exception("Web root path not found");
        _internStoragePath = resource.FileType switch
        {
            FileType.Image => Path.Combine(_internStoragePath, _conf.GetSection("FileConfiguration:image:storage").Value ?? throw new BadRequestException("Storage path not found")),
            FileType.Document => Path.Combine(_internStoragePath, _conf.GetSection("FileConfiguration:document:storage").Value ?? throw new BadRequestException("Storage path not found")),
            _ => throw new Exception("Invalid file type"),
        };
        var path = Path.Combine(_internStoragePath, resource.Url);
        if (File.Exists(path))
            File.Delete(path);
    
    }

    public (Stream Stream, string ContentType, string FileName) OpenResource(Resource resource)
    {
         if (resource.StorageType == StorageType.OnCloud)
        throw new NotSupportedException("Cloud Storage not supported");

        string basePath = resource.FileType switch
        {
            FileType.Image => Path.Combine(_env.ContentRootPath, _conf["FileConfiguration:image:storage"]!),
            FileType.Document => Path.Combine(_env.ContentRootPath, _conf["FileConfiguration:document:storage"]!),
            _ => throw new Exception("Invalid file type"),
        };

        var path = Path.Combine(basePath, resource.Url);
        if (!File.Exists(path))
            throw new NotFoundException("Fichier introuvable");

        return (File.OpenRead(path), resource.ContentType ?? "application/octet-stream", resource.AlternativeText);
    }
}