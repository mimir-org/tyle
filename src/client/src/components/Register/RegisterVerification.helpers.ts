import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";

export const onSubmitForm = async (
  data: MimirorgVerifyAm,
  verifyAsync: (data: MimirorgVerifyAm) => Promise<boolean>,
  generateMfaAsync: (data: MimirorgVerifyAm) => Promise<MimirorgQrCodeCm>,
  setMfaInfo: (data: MimirorgQrCodeCm) => void,
) => {
  const isVerified = await verifyAsync(data);
  const mfaInfo = await generateMfaAsync(data);
  const userIsVerifiedAndReceivesMfaInfo = isVerified && mfaInfo;

  if (userIsVerifiedAndReceivesMfaInfo) {
    setMfaInfo(mfaInfo);
  }
};
