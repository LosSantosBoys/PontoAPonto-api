namespace PontoAPonto.Domain.Enums
{
    public enum DriverStatus
    {
        WAITING_OTP_VERIFICATION = 0,
        OTP_BLOCKED = 1,
        SIGNIN_AVAILABLE = 2,
        WAITING_FACE_CAPTURE = 3,
        WAITING_DOCUMENT_CAPTURE = 4,
        WAITING_CAR_INFO = 5,
        WAITING_MANUAL_APPROVAL = 6,
        APPROVED = 7,
        REPROVED = 8
    }
}
