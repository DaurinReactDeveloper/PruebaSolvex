﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PruebaSolvex.Application.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Application.Servicies
{
    public class CloudinaryService : ICloudinaryServices
    {

        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;

        public CloudinaryService(IConfiguration config, ILogger<CloudinaryService> logger)
        {
            var cloudName = config["Cloudinary:CloudName"];
            var apiKey = config["Cloudinary:ApiKey"];
            var apiSecret = config["Cloudinary:ApiSecret"];

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account) { Api = { Secure = true } };

            _logger = logger;
        }

        public async Task<string?> UploadImageFromUrlAsync(string imageUrl)
        {
            try
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageUrl),
                    UseFilename = true,
                    UniqueFilename = false,
                    Overwrite = true,
                    Folder = "PruebaSolvex/solvexprueba"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl?.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al subir imagen: {Message}", ex.Message);
                return null;
            }

        }
    
    }
}
