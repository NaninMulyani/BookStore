﻿namespace bookApi.Core.Models;

public record AzureBlobUploadResponse
{
    public string NewFileName { get; set; } = null!;
}