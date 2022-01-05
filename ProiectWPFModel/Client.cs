namespace ProiectWPFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstNameC { get; set; }

        [Required]
        [StringLength(50)]
        public string LastNameC { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneC { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
