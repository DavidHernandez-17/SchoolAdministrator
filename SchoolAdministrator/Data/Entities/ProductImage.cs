﻿using System.ComponentModel.DataAnnotations;

namespace SchoolAdministrator.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Foto")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:7213/images/noimage.png"
            : $"https://shopping4.blob.core.windows.net/products/{ImageId}";

    }
}
