namespace Boost
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visit")]
    public partial class Visit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? Client_Id { get; set; }

        public int? Employee_Id { get; set; }

        public int? Service_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateTime { get; set; }

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Service Service { get; set; }
    }
}
