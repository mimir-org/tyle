import { QrCodeView } from "types/authentication/qrCodeView";
import { VerifyRequest } from "types/authentication/verifyRequest";

export const onSubmitForm = async (
  data: VerifyRequest,
  verifyAsync: (data: VerifyRequest) => Promise<boolean>,
  generateMfaAsync: (data: VerifyRequest) => Promise<QrCodeView>,
  setMfaInfo: (data: QrCodeView) => void,
) => {
  const isVerified = await verifyAsync(data);
  const mfaInfo = await generateMfaAsync(data);
  const userIsVerifiedAndReceivesMfaInfo = isVerified && mfaInfo;

  if (userIsVerifiedAndReceivesMfaInfo) {
    setMfaInfo(mfaInfo);
  }
};
