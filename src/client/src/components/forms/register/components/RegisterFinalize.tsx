import { TextResources } from "../../../../assets/text";
import { Flexbox } from "../../../../complib/layouts";
import { RegisterQrImage, RegisterFinalizeLink } from "./RegisterFinalize.styled";
import { Divider } from "../../../../complib/data-display";
import { THEME } from "../../../../complib/core";
import { Heading, Text } from "../../../../complib/text";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => (
  <Flexbox flexDirection={"column"} gap={THEME.SPACING.XL}>
    <Flexbox as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <Heading>{TextResources.REGISTER_FINALIZE_VERIFICATION}</Heading>
      <Text>{TextResources.REGISTER_FINALIZE_VERIFICATION_DESCRIPTION}</Text>
    </Flexbox>
    <Divider />
    <Flexbox as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <Heading>{TextResources.REGISTER_FINALIZE_MFA}</Heading>
      <Text>{TextResources.REGISTER_FINALIZE_MFA_DESCRIPTION}</Text>
      <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
      <RegisterFinalizeLink to="/">{TextResources.REGISTER_FINALIZE_FINISH_LINK}</RegisterFinalizeLink>
    </Flexbox>
  </Flexbox>
);
