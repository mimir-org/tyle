import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";

export const onSubmitForm = async (
  data: VerifyRequest,
  generateMfaAsync: (data: VerifyRequest) => Promise<QrCodeView>,
  setMfaInfo: (data: QrCodeView) => void,
  setVerificationInfo?: (data: VerifyRequest) => void,
) => {
  const mfaInfo = await generateMfaAsync(data);
  mfaInfo && setMfaInfo(mfaInfo);
  setVerificationInfo && setVerificationInfo(data);
};
