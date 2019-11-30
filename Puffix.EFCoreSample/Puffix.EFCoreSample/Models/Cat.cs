using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puffix.EFCoreSample.Models
{
    /// <summary>
    /// Cat item.
    /// </summary>
    [Table("Cats")]
    public class Cat : IItem<int>
    {
        /// <summary>
        /// Technical Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Name of the cat.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the cat.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Birth date.
        /// </summary>
        public DateTime BirthDate { get; set; }
    }
}
