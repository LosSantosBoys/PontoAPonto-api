using PontoAPonto.Domain.Interfaces.Services;

namespace PontoAPonto.Domain.Constant
{
    public static class Constants
    {
        public struct UserStatus
        {
            public const string WaitingOtpVerification = "Waiting for OTP verification";
            public const string OtpVerified = "OTP verified";
            public const string SignInAvailable = "SignIn Available";
        }

        public struct ResponseMessages
        {
            public const string UserSignUpCreated = "User and OTP code created";
            public const string ErrorCreatingUserSignUp = "Failed to create user.";
            public const string SignInError = "Email and password does not match.";
            public const string SignInSuccess = "SignIn success";
        }

        public struct Email
        {
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

        public struct ErrorCodes
        {
            public const string HttpError = "E01";
        }
    }
}
