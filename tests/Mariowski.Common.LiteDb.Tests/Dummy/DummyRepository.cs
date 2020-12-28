using LiteDB;

namespace Mariowski.Common.LiteDb.Tests.Dummy
{
    public class DummyRepository : LiteDbRepository<LiteDbContext, DummyEntity, int>
    {
        private ILiteCollection<SubDummyEntity> SubDummyCollection => Context.Database.GetCollection<SubDummyEntity>();

        public DummyRepository(LiteDbContext context)
            : base(context)
        {
            context.Database.DropCollection(nameof(DummyEntity));
            context.Database.DropCollection(nameof(SubDummyEntity));

            BsonMapper.Global.Entity<DummyEntity>()
                .DbRef(d => d.Sub, nameof(SubDummyEntity));


            InsertSubDummies();
            InsertDummies();
        }

        private void InsertSubDummies()
        {
            var subDummies = new[]
            {
                new SubDummyEntity { Id = 333 },
                new SubDummyEntity { Id = 444, Bar = "Test!" },
                new SubDummyEntity { Id = 555 },
                new SubDummyEntity { Id = 666 },
                new SubDummyEntity { Id = 777 },
                new SubDummyEntity { Id = 888 },
                new SubDummyEntity { Id = 999 }
            };

            SubDummyCollection.Insert(subDummies);
        }

        private void InsertDummies()
        {
            var subDummyCollection = SubDummyCollection;

            var dummies = new[]
            {
                new DummyEntity
                {
                    Id = 666,
                    Sub = subDummyCollection.FindById(333)
                },
                new DummyEntity
                {
                    Id = 777,
                    Sub = subDummyCollection.FindById(444)
                },
                new DummyEntity
                {
                    Id = 888,
                    Foo = "Sub",
                    Sub = subDummyCollection.FindById(555)
                }
            };

            Collection.Insert(dummies);
        }
    }
}