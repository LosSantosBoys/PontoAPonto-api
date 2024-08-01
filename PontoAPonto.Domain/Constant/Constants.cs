namespace PontoAPonto.Domain.Constant
{
    public static class Constants
    {
        public struct Email
        {
            public struct SmtpServer
            {
                public const string Host = "smtp.gmail.com";
                public const int Port = 587;
            }

            public const string SubjectOtp = "Ponto a Ponto - Código de verificação";
            public const string BodyOtp = "Seu código de verificação: {0}";
            public const string SubjectForgotPassword = "Ponto a Ponto - Recuperação de senha";
            public const string BodyForgotPassword = "Acesse {0} para alterar sua senha";

            public struct Html
            {
                public const string BodySignUp = @"<!DOCTYPE html>
                                    <html lang=""pt-BR"">
                                    <head>
                                      <meta charset=""UTF-8"">
                                      <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                      <title>Confirme seu cadastro</title>
                                    </head>
                                    <body style=""font-family: Arial, sans-serif; margin: 0; padding: 0; background-color: #F8F9FE;"">
                                      <div style=""max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px; box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);"">
                                        <div style=""text-align: center; padding-bottom: 20px; border-bottom: 1px solid #ddd;"">
                                          <img src=""https://i.imgur.com/V2u2tLI.png"" alt=""Logo Ponto A Ponto"" style=""display: inline-block; margin-bottom: 10px; width: 20%; height: 20%;"" referrerpolicy=""no-referrer"">
                                          <h1 style=""font-size: 24px; font-weight: bold; color: #3755C1;"">Confirme seu cadastro</h1>
                                        </div>
                                    
                                        <p style=""font-size: 16px; line-height: 1.5; color: #2F3036;"">Muito obrigado por se cadastrar no <strong style=""color: #3755C1;"">Ponto a Ponto</strong>, {0}! Para concluir seu cadastro, utilize este código no aplicativo.</p>
                                    
                                        <div style=""text-align: center; padding: 20px 0; background-color: #D4D6DD; border-radius: 5px;"">
                                          <span style=""font-size: 36px; font-weight: bold; color: #1F2024; margin-bottom: 10px;""><strong>{1}</strong></span>
                                          <p style=""font-size: 16px; line-height: 1.5; color: #2F3036;"">Este código é válido por 5 minutos.</p>
                                        </div>
                                    
                                        <div style=""text-align: center; padding-top: 20px;"">
                                          <p style=""font-size: 16px; line-height: 1.5; color: #2F3036;"">Acesse o aplicativo e insira o código acima para validar sua conta!</p>
                                          <p style=""font-size: 16px; line-height: 1.5; color: #2F3036;""><strong>Equipe Ponto a Ponto.</strong></p>
                                        </div>
                                      </div>
                                    </body>
                                    </html>";

            }
        }

        #region Errors
        public struct ErrorCodes
        {
            public const string HttpError = "E01";

            public struct Generic
            {
                public const string InternalError = "Generic.InternalError";
                public const string Unauthorized = "{0}.Unauthorized";

                public struct Database
                {
                    public const string EntityNotFound = "{0}.NotFound";
                }
            }

            public struct Business
            {
                public struct SignUp
                {
                    public const string DataConflict = "SignUp.DataConflict";
                    public const string InvalidDateFormat = "SignUp.InvalidDateFormat";
                }

                public struct SignIn
                {
                    public const string InvalidSignIn = "SignIn.InvalidSignIn";
                    public const string OtpNotCompleted = "SignIn.OtpNotCompleted";
                    public const string ReprovedUser = "SignIn.ReprovedUser";
                }

                public struct Otp
                {
                    public const string UserAlreadyVerified = "Otp.UserAlreadyVerified";
                    public const string ExceededMaximumAttempts = "Otp.ExceededMaximumAttempts";
                    public const string ExpiredOtp = "Otp.ExpiredOtp";
                    public const string InvalidOtp = "Otp.InvalidOtp";
                }
            }

            public struct Aws
            {
                public const string UploadFail = "S3.UploadFail";
                public const string GetFileFail = "S3.GetFileFail";
                public const string FileNotFound = "S3.FileNotFound";
            }
        }

        public struct ErrorMessages
        {
            public struct Generic
            {
                public const string InternalError = "Ops! Encontramos um problema interno. Volte em breve e veremos como podemos te levar aonde você precisa!";
                public const string Unauthorized = "Ops! Parece que você se perdeu. Por favor, entre em sua conta e tente novamente!";

                public struct Database
                {
                    public const string EntityNotFound = "{0} solicitado não encontrado.";
                }
            }

            public struct Business
            {
                public struct SignUp
                {
                    public const string DataConflict = "Parece que seus dados já existem em nosso sistema. Vamos tentar novamente?";
                    public const string InvalidDateFormat = "Formato de data inválida. Utilize o formato dd-MM-aaaa.";
                }

                public struct SignIn
                {
                    public const string InvalidSignIn = "Seus dados estão incorretos. Verifique se digitou corretamente seu e-mail e preste atenção nas letras maiúsculas em sua senha.";
                    public const string OtpNotCompleted = "Parece que você ainda não verificou sua conta. Confira o código que enviamos ao seu e-mail ou gere um novo!";
                    public const string ReprovedUser = "Infelizmente você foi reprovado para o Ponto A Ponto. Não se preocupe, você poderá tentar novamente no futuro. Agradecemos pelo seu tempo!";
                }

                public struct Otp
                {
                    public const string UserAlreadyVerified = "Parece que você já verificou sua conta. Tente entrar no aplicativo!";
                    public const string ExceededMaximumAttempts = "Parece que você ultrapassou o limite de tentativas. Tente novamente mais tarde.";
                    public const string ExpiredOtp = "O código informado está expirado. Gere um novo e tente novamente.";
                    public const string InvalidOtp = "O código informado é inválido. Verifique-o e tente novamente.";
                }
            }

            public struct Aws
            {
                public const string UploadFail = "Falha ao realizar o upload do arquivo solicitado.";
                public const string GetFileFail = "Erro ao fazer o download do arquivo solicitado.";
                public const string FileNotFound = "Arquivo solicitado não encontrado.";
            }
        }
        #endregion

        public struct S3
        {
            public const string PublicProfilePictureUrl = "https://{0}.s3.amazonaws.com/{1}";
        }
    }
}
