namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.meters")]
    public partial class meters
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public meters()
        {
            meters_lte = new HashSet<meters_lte>();
            meters_lte_history = new HashSet<meters_lte_history>();
            meters_subabonents = new HashSet<meters_subabonents>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idMeter { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        public bool connectTC { get; set; }

        public byte? minCurrent { get; set; }

        public byte? maxCurrent { get; set; }

        public bool threePhase { get; set; }

        [Column(TypeName = "mediumint")]
        public int? impKWh { get; set; }

        public sbyte? rs485 { get; set; }

        public byte? limit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<meters_lte> meters_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<meters_lte_history> meters_lte_history { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<meters_subabonents> meters_subabonents { get; set; }
    }
}
