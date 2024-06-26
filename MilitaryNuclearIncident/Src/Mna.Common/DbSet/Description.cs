﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilitaryNuclearAccident.Src.Mna.Common.DbSet
{
    [Table("description")]
    public class Description
    {
        [Key]
        [Column("fulldescriptionid")]
        public Guid FullDescriptionId { get; set; }

        [Column("fulldescription")]
        public string? FullDescription { get; set; }

        public BrokenArrow? BrokenArrow { get; set; }

    }
}
