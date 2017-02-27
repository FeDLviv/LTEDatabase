namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.objects")]
    public partial class objects
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public objects()
        {
            condensers_lte = new HashSet<condensers_lte>();
            meters_lte = new HashSet<meters_lte>();
            motors_lte = new HashSet<motors_lte>();
            ts_lte = new HashSet<ts_lte>();
            tc_lte = new HashSet<tc_lte>();
            subabonents_lte = new HashSet<subabonents_lte>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string region { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        [Required]
        [StringLength(35)]
        public string address { get; set; }

        public float? power { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string category { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string organization { get; set; }

        [StringLength(13)]
        public string contract { get; set; }

        [StringLength(120)]
        public string pathDir { get; set; }

        [StringLength(120)]
        public string pathAct { get; set; }

        [StringLength(120)]
        public string pathLoss { get; set; }

        [StringLength(120)]
        public string pathScheme { get; set; }

        [StringLength(120)]
        public string pathProject { get; set; }

        [StringLength(120)]
        public string pathProtocol { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string notes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<condensers_lte> condensers_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<meters_lte> meters_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<motors_lte> motors_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ts_lte> ts_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tc_lte> tc_lte { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<subabonents_lte> subabonents_lte { get; set; }
    }
}
