import { MimirorgQrCodeCm, MimirorgVerifyAm } from "@mimirorg/typelibrary-types";

export const onSubmitForm = async (
  data: MimirorgVerifyAm,
  generateMfaAsync: (data: MimirorgVerifyAm) => Promise<MimirorgQrCodeCm>,
  setMfaInfo: (data: MimirorgQrCodeCm) => void,
  setVerificationInfo?: (data: MimirorgVerifyAm) => void,
) => {
  const mfaInfo = await generateMfaAsync(data);
  mfaInfo && setMfaInfo(mfaInfo);
  setVerificationInfo && setVerificationInfo(data);
};
