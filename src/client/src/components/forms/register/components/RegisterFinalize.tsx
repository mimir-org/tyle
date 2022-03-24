import { TextResources } from "../../../../assets/text";
import {
  RegisterFinalizeContainer,
  RegisterFinalizeHeader,
  RegisterFinalizeText,
  RegisterQrImage,
  RegisterFinalizeLink,
  RegisterFinalizeSection,
} from "./RegisterFinalize.styled";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => (
  <RegisterFinalizeContainer>
    <RegisterFinalizeSection>
      <RegisterFinalizeHeader>{TextResources.REGISTER_FINALIZE_VERIFICATION}</RegisterFinalizeHeader>
      <RegisterFinalizeText>{TextResources.REGISTER_FINALIZE_VERIFICATION_DESCRIPTION}</RegisterFinalizeText>
    </RegisterFinalizeSection>
    <RegisterFinalizeSection>
      <RegisterFinalizeHeader>{TextResources.REGISTER_FINALIZE_MFA}</RegisterFinalizeHeader>
      <RegisterFinalizeText>{TextResources.REGISTER_FINALIZE_MFA_DESCRIPTION}</RegisterFinalizeText>
      <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
      <RegisterFinalizeLink to="/">{TextResources.REGISTER_FINALIZE_FINISH_LINK}</RegisterFinalizeLink>
    </RegisterFinalizeSection>
  </RegisterFinalizeContainer>
);
