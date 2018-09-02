using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Domain.Circles;
using Domain.Domain.Users;
using MySql.Data.MySqlClient;

namespace ProductInfrastructure.Circles
{
    public class CircleRepository : ICircleRepository
    {
        public Circle Find(CircleId id) {
            throw new NotImplementedException();
        }

        public void Save(Circle circle) {
            using (var con = new MySqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var com = con.CreateCommand()) {
                    var note = new CircleNotification();
                    circle.Notify(note);
                    var data = note.Build();

                    com.CommandText = "UPDATE t_circle SET circle_name = @circle_name, join_members = @join_members WHERE id = @id";
                    com.Parameters.Add(new MySqlParameter("@circle_name", data.Name));
                    com.Parameters.Add(new MySqlParameter("@id", data.Id));
                    com.Parameters.Add(new MySqlParameter("@join_members", string.Join(",", data.UserIds))); // 所属メンバーは中間テーブルにしたほうがいいかも
                    com.ExecuteNonQuery();
                }
            }
        }
    }
}
