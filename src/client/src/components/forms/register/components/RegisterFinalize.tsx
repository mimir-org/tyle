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
      <RegisterFinalizeHeader>{TextResources.Register_Finalize_Verification}</RegisterFinalizeHeader>
      <RegisterFinalizeText>{TextResources.Register_Finalize_Verification_Description}</RegisterFinalizeText>
    </RegisterFinalizeSection>
    <RegisterFinalizeSection>
      <RegisterFinalizeHeader>{TextResources.Register_Finalize_MFA}</RegisterFinalizeHeader>
      <RegisterFinalizeText>{TextResources.Register_Finalize_MFA_Description}</RegisterFinalizeText>
      <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
      <RegisterFinalizeLink to="/">{TextResources.Register_Finalize_Finish_Link}</RegisterFinalizeLink>
    </RegisterFinalizeSection>
  </RegisterFinalizeContainer>
);
