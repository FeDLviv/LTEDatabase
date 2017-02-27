namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.subabonents_lte")]
    public partial class subabonents_lte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public subabonents_lte()
        {
            meters_subabonents = new HashSet<meters_subabonents>();
            subabonents_documents = new HashSet<subabonents_documents>();
            tc_subabonents = new HashSet<tc_subabonents>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idSubabonentsLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idSubabonents { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        public float? power { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<meters_subabonents> meters_subabonents { get; set; }

        public virtual objects objects { get; set; }

        public virtual subabonents subabonents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<subabonents_documents> subabonents_documents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tc_subabonents> tc_subabonents { get; set; }
    }
}
