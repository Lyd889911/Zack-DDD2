namespace Lyd.Shared
{
    /// <summary>
    /// 基础实体,每个实体都有的部分
    /// </summary>
    public record BaseEntity
    {
        public DateTime? Created { get; private set; }
        public DateTime? Updated { get; set; }
        public bool Deleted { get; set; }
        public Guid Id { get; set; }

        public BaseEntity()
        {
            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
            this.Id = Guid.NewGuid();
            this.Deleted = false;
        }

    }
}