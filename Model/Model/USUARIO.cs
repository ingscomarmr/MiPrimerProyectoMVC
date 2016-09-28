namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEV_001.USUARIO")]
    public partial class USUARIO
    {
        public decimal ID { get; set; }

        [Required]
        [StringLength(100)]
        public string USER_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string PWD { get; set; }

        [Required]
        [StringLength(100)]
        public string ROL { get; set; }
    }
}
