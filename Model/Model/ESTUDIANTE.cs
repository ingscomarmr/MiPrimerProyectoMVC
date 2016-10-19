namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.Entity;

    [Table("DEV_001.ESTUDIANTE")]
    public partial class ESTUDIANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESTUDIANTE()
        {
            ESTUDIANTE_CURSO = new HashSet<ESTUDIANTE_CURSO>();
            ESTUDIANTE_ARCHIVOS_ADJUNTOS = new HashSet<ESTUDIANTE_ARCHIVOS_ADJUNTOS>();
        }

        public decimal ID { get; set; }

        [Required(ErrorMessage ="Este datos es requerido")]
        [StringLength(100)]
        public string NOMBRE { get; set; }

        [Required(ErrorMessage = "Este datos es requerido")]
        [StringLength(100)]
        public string APELLIDO { get; set; }

        [Required(ErrorMessage = "Este datos es requerido, debe contener el formado de email, ejemplo@email.com")]
        [StringLength(100)]
        public string EMAIL { get; set; }

        [Required]
        public decimal EDAD { get; set; }

        [Required(ErrorMessage ="Es requerido, debe contener el formato YYYY-MM-DD")] //intro mensaje personalizado
        //[RegularExpression(@"\d\d\d\d-\d\d-\d\d",ErrorMessage ="Debe contener el formato YYYY-MM-DD")]
        public DateTime FECHA_NACIMIENTO { get; set; }

        [Required]
        [Range(0,1, ErrorMessage ="Debe seleccionar entre Mujer y Hombre")]
        public decimal SEXO { get; set; }

        [StringLength(100,ErrorMessage ="Solo permite 100 caracetes")]
        public string PWD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTUDIANTE_CURSO> ESTUDIANTE_CURSO { get; set; }

        public virtual ICollection<ESTUDIANTE_ARCHIVOS_ADJUNTOS> ESTUDIANTE_ARCHIVOS_ADJUNTOS { get; set; }        
    }
}
