namespace Botcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceRecordXrpToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PriceRecordXrps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 10),
                        DateRegistered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PriceRecordXrps");
        }
    }
}
