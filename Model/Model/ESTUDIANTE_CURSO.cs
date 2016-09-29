namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEV_001.ESTUDIANTE_CURSO")]
    public partial class ESTUDIANTE_CURSO
    {
        [Key]
        [Column(Order = 0)]
        public decimal ID_ESTUDIANTE { get; set; }

        public decimal? ID_CURSO { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal PUNTOS_CURSO { get; set; }

        public virtual CURSO CURSO { get; set; }

        public virtual ESTUDIANTE ESTUDIANTE { get; set; }
    }
}
