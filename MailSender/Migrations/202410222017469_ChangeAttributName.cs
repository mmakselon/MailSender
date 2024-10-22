namespace MailSender.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAttributName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailMessages", "SendDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.EmailMessages", "SentDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailMessages", "SentDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.EmailMessages", "SendDate");
        }
    }
}
