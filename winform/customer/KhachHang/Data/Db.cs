using System;
using System.Data;
using System.Data.SqlClient;

namespace KhachHang.Data
{
    public static class Db
    {
        public static DataTable ExecProcToTable(string proc, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(DbConfig.ConnStr))
            using (var cmd = new SqlCommand(proc, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 60; // tăng lên 60s
                if (ps != null) cmd.Parameters.AddRange(ps);

                using (var da = new SqlDataAdapter(cmd))
                {
                    da.SelectCommand.CommandTimeout = 60;
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public static int ExecProcNonQuery(string proc, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(DbConfig.ConnStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 60;

                    if (ps != null) cmd.Parameters.AddRange(ps);

                    cmd.ExecuteNonQuery();

                    // Quy ước: nếu có @KQ output thì trả về, không có thì 0
                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        if (string.Equals(p.ParameterName, "@KQ", StringComparison.OrdinalIgnoreCase)
                            && p.Direction == ParameterDirection.Output)
                        {
                            return (p.Value == DBNull.Value) ? 0 : Convert.ToInt32(p.Value);
                        }
                    }
                    return 0;
                }
            }
        }

        public static object ExecProcScalar(string proc, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(DbConfig.ConnStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(proc, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (ps != null) cmd.Parameters.AddRange(ps);

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataTable QueryToTable(string sql, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(DbConfig.ConnStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (ps != null) cmd.Parameters.AddRange(ps);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static int ExecTextNonQuery(string sql, params SqlParameter[] ps)
        {
            using (var conn = new SqlConnection(DbConfig.ConnStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (ps != null) cmd.Parameters.AddRange(ps);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
