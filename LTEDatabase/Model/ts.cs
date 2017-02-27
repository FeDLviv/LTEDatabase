namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.ts")]
    public partial class ts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ts()
        {
            ts_lte = new HashSet<ts_lte>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idTS { get; set; }

        [Required]
        [StringLength(7)]
        public string number { get; set; }

        [StringLength(255)]
        public string path { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ts_lte> ts_lte { get; set; }
    }
}
