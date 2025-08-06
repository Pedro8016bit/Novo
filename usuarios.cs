using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProGEs
{
    public class Usuarios
    {
        private string nome;
        private string tipo;
        private string email;
        private string senha;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }


        public bool CriarUser()
        {
            try
            {
                if (!verificarEmail(Email))
                {
                    MessageBox.Show("E-mail inválido. Digite um e-mail válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                string SenhaCriptografada = CriptografarSenha(Senha);

                using (MySqlConnection conexaoBanco = new BancoDb().Conectar())
                {
                    string inserir = "insert into usuarios (nome_usuario, tipo_usuario, email, senha) value (@nome_usuario, @tipo_usuario, @email, @senha)";

                    MySqlCommand comando = new MySqlCommand(inserir, conexaoBanco);
                    comando.Parameters.AddWithValue("@nome_usuario", Nome);
                    comando.Parameters.AddWithValue("@tipo_usuario", Tipo);
                    comando.Parameters.AddWithValue("@email", Email);
                    comando.Parameters.AddWithValue("@senha", SenhaCriptografada);

                    int resultado = comando.ExecuteNonQuery();

                    if (resultado > 0)
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível cadastrar o usuário. Verifique os dados e tente novamente.", "Falha no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro - Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static string CriptografarSenha(string senha)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(senha ?? ""));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                        builder.Append(b.ToString("x2"));
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível criptografar a senha: " + ex.Message, "Erro - Método Criptografar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static bool verificarEmail(string email)
        {
            string emailValido = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailValido);
            return regex.IsMatch(email);
        }

        public static Usuarios FazerLogin(string login, string senha)
        {
            try
            {
                string senhaCriptografada = CriptografarSenha(senha);

                using (MySqlConnection conexaoBanco = new BancoDb().Conectar())
                {
                    string sql = "SELECT id_usuario, nome_usuario, tipo_usuario, email FROM usuarios WHERE (nome_usuario = @login OR email = @login) AND senha = @senha";

                    MySqlCommand cmd = new MySqlCommand(sql, conexaoBanco);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@senha", senhaCriptografada);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuarios
                            {
                                Nome = reader.GetString("nome_usuario"),
                                Tipo = reader.GetString("tipo_usuario"),
                                Email = reader.GetString("email")
                                // senha não é retornada por segurança
                            };
                        }
                        else
                        {
                            return null; // login inválido
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao fazer login: " + ex.Message, "Erro - Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Usuarios BuscarPorId(int id)
        {
            try
            {
                using (MySqlConnection conexaoBanco = new BancoDb().Conectar())
                {
                    string sql = "SELECT nome_usuario, tipo_usuario, email FROM usuarios WHERE id_usuario = @id";

                    MySqlCommand cmd = new MySqlCommand(sql, conexaoBanco);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuarios
                            {
                                Nome = reader.GetString("nome_usuario"),
                                Tipo = reader.GetString("tipo_usuario"),
                                Email = reader.GetString("email")
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar usuário: " + ex.Message, "Erro - BuscarPorId", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static int BuscarIdPorNome(string nome)
        {
            try
            {
                using (MySqlConnection conexaoBanco = new BancoDb().Conectar())
                {
                    string sql = "SELECT id_usuario FROM usuarios WHERE nome_usuario = @nome LIMIT 1";

                    MySqlCommand cmd = new MySqlCommand(sql, conexaoBanco);
                    cmd.Parameters.AddWithValue("@nome", nome);

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && int.TryParse(resultado.ToString(), out int id))
                    {
                        return id;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar ID: " + ex.Message, "Erro - BuscarIdPorNome", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

    }
}
