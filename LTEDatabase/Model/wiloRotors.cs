namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.wiloRotors")]
    public partial class wiloRotors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wiloRotors()
        {
            wiloCharacteristics = new HashSet<wiloCharacteristics>();
        }

        [Key]
        [Column(TypeName = "umediumint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idRotor { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [StringLength(50)]
        public string typeWheel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wiloCharacteristics> wiloCharacteristics { get; set; }
    }
}
