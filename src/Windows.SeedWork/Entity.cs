using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Windows.SeedWork
{
    [Serializable]
    public abstract class Entity<TPrimaryKey> where TPrimaryKey : struct
    {
        [Key]
        public virtual TPrimaryKey Id { get; set; }
    }
    [Serializable]
    public abstract class Entity : Entity<int>
    {

    }
    public abstract class FullEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreateEntity<TPrimaryKey>, IUpdateEntity<TPrimaryKey>, IEnableEntity where TPrimaryKey : struct
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        public virtual bool IsEnable { get; set; } = true;
        /// <summary>
        /// 创建人Id
        /// </summary>
        [Required]
        public virtual TPrimaryKey CreatedBy { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        [Required]
        public virtual string CreatedByName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public virtual DateTime CreatedOn { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新人Id
        /// </summary>
        public virtual TPrimaryKey? UpdatedBy { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public virtual string UpdatedByName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public virtual DateTime? UpdatedOn { get; set; }
    }
    public interface ICreateEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        TPrimaryKey CreatedBy { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        string CreatedByName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedOn { get; set; }
    }
    public interface IUpdateEntity<TPrimaryKey> where TPrimaryKey : struct
    {
        /// <summary>
        /// 更新人Id
        /// </summary>
        TPrimaryKey? UpdatedBy { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        string UpdatedByName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? UpdatedOn { get; set; }
    }
    public interface IEnableEntity
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnable { get; set; }
    }
}
