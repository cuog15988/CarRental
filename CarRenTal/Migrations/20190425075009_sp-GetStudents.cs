using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRenTal.Migrations
{
    public partial class spGetStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetStudents]
  AS
                BEGIN
select  x.id,TenLoai,TenHang,TenXe,Doi,x.NgayNhap,MaNguoiDang,Tinh,Huyen 
from HangXe h, Loaixe l,TenXe t, Xe x,Users u 
where l.id=h.MaLoaiXe and h.id=t.MaHangXe and X.MaTenXe = t.id and u.id=x.MaNguoiDang
end
";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
