#region Include Definition
using System;
using System.ComponentModel.DataAnnotations;
#endregion

namespace ProductService.Database.Domain 
{
    #region Public Class Definition
    public abstract class BaseEntity
    {
        #region Public Property Definition
        public long ID { get; set; }

        public DateTime CreateDate { get; set; }

        public byte[] Timestamp { get; set; }

        [Required(ErrorMessage = "Status Required.")]
        [Range(0, 4, ErrorMessage = "Invalid Option.")]
        public byte Status { get; set; }
        #endregion
    }
    #endregion
}