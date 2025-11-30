using FluentMigrator;
using Microsoft.AspNetCore.Mvc;

namespace Akshada.API.Migrations
{
    [Migration(20251020232600)]
    public class DB_20251020_232600_MenuMaster : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("menu_master")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("row_id").AsString().NotNullable().Unique("UK_MENU_MASTER_ROW_ID")
                .WithColumn("menu_text").AsString().NotNullable().Unique("UK_MENU_MASTER_TEXT")
                .WithColumn("seq_no").AsInt32().NotNullable()
                .WithColumn("controller").AsString().Nullable()
                .WithColumn("page").AsString().Nullable()
                .WithColumn("fa_icon").AsString().NotNullable()
                .WithColumn("parent_id").AsInt32().Nullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_at").AsDateTime().NotNullable()
                .WithColumn("updated_by").AsInt32().NotNullable()
                .WithColumn("updated_at").AsDateTime().NotNullable();

            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 1,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Master",
                    seq_no = 1,
                    controller = "master",
                    page = "#",
                    fa_icon = "fa-plus",
                    parent_id = DBNull.Value,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });

            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 2,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "System Parameters",
                    seq_no = 1,
                    controller = DBNull.Value,
                    page = "list-system-parameter",
                    fa_icon = "fa-plus",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });

            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 3,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Users",
                    seq_no = 2,
                    controller = DBNull.Value,
                    page = "list-users",
                    fa_icon = "fa-plus",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });


            Insert.IntoTable("menu_master")
                .Row(new
                {
                    id = 4,
                    row_id = System.Guid.NewGuid().ToString(),
                    menu_text = "Service Rates",
                    seq_no = 3,
                    controller = DBNull.Value,
                    page = "list-service-rates",
                    fa_icon = "fa-plus",
                    parent_id = 1,
                    created_by = 1,
                    created_at = System.DateTime.UtcNow,
                    updated_by = 1,
                    updated_at = System.DateTime.UtcNow
                });
        }
    }
}
