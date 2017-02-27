namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.subabonents")]
    public partial class subabonents
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public subabonents()
        {
            subabonents_lte = new HashSet<subabonents_lte>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idSubabonents { get; set; }

        [Required]
        [StringLength(35)]
        public string name { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<subabonents_lte> subabonents_lte { get; set; }
    }
}
