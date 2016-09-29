namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEV_001.CURSO")]
    public partial class CURSO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CURSO()
        {
            ESTUDIANTE_CURSO = new HashSet<ESTUDIANTE_CURSO>();
        }

        [Key]
        public decimal ID_CURSO { get; set; }

        [Required]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTUDIANTE_CURSO> ESTUDIANTE_CURSO { get; set; }
    }
}
