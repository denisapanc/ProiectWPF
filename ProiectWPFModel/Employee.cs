namespace ProiectWPFModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstNameE { get; set; }

        [Required]
        [StringLength(50)]
        public string LastNameE { get; set; }

        [Required]
        [StringLength(10)]
        public string PhoneE { get; set; }

        [StringLength(50)]
        public string EmployeeCod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
