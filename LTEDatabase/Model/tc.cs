namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.tc")]
    public partial class tc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tc()
        {
            tc_lte = new HashSet<tc_lte>();
            tc_lte_history = new HashSet<tc_lte_history>();
            tc_subabonents = new HashSet<tc_subabonents>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idTC { get; set; }

        [Required]
        [StringLength(10)]
        public string type { get; set; }

        [Required]
        [StringLength(5)]
        public string coefficient { get; set; }

        public byte? limit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tc_lte> tc_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tc_lte_history> tc_lte_history { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tc_subabonents> tc_subabonents { get; set; }
    }
}
