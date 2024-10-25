namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAccountParams : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailAccountParams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HostSmtp = c.String(nullable: false),
                        EnableSsl = c.Boolean(nullable: false),
                        Port = c.Int(nullable: false),
                        SenderEmail = c.String(nullable: false),
                        SenderEmailPassword = c.String(nullable: false),
                        SenderName = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.EmailMessages", "AccountParamsId", c => c.Int(nullable: false));
            CreateIndex("dbo.EmailMessages", "AccountParamsId");
            AddForeignKey("dbo.EmailMessages", "AccountParamsId", "dbo.EmailAccountParams", "Id", cascadeDelete: true);
            DropColumn("dbo.EmailMessages", "EmailFrom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailMessages", "EmailFrom", c => c.String(nullable: false));
            DropForeignKey("dbo.EmailAccountParams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EmailMessages", "AccountParamsId", "dbo.EmailAccountParams");
            DropIndex("dbo.EmailMessages", new[] { "AccountParamsId" });
            DropIndex("dbo.EmailAccountParams", new[] { "UserId" });
            DropColumn("dbo.EmailMessages", "AccountParamsId");
            DropTable("dbo.EmailAccountParams");
        }
    }
}
