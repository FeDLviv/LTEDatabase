namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.subabonents_documents")]
    public partial class subabonents_documents
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idSubDocuments { get; set; }

        [Column(TypeName = "usmallint")]
        public int idSubabonentsLTE { get; set; }

        [StringLength(120)]
        public string pathTech { get; set; }

        [StringLength(120)]
        public string pathAct { get; set; }

        [StringLength(120)]
        public string pathScheme { get; set; }

        public virtual subabonents_lte subabonents_lte { get; set; }
    }
}
