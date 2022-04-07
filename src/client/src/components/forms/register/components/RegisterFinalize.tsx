import { TextResources } from "../../../../assets/text";
import { Flex } from "../../../../complib/layouts";
import { RegisterQrImage, RegisterFinalizeLink } from "./RegisterFinalize.styled";
import { Divider } from "../../../../complib/data-display";
import { THEME } from "../../../../complib/core/constants";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => (
  <Flex flexDirection={"column"} gap={"var(--spacing-xl)"}>
    <Flex as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <h1>{TextResources.REGISTER_FINALIZE_VERIFICATION}</h1>
      <p>{TextResources.REGISTER_FINALIZE_VERIFICATION_DESCRIPTION}</p>
    </Flex>
    <Divider />
    <Flex as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <h1>{TextResources.REGISTER_FINALIZE_MFA}</h1>
      <p>{TextResources.REGISTER_FINALIZE_MFA_DESCRIPTION}</p>
      <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
      <RegisterFinalizeLink to="/">{TextResources.REGISTER_FINALIZE_FINISH_LINK}</RegisterFinalizeLink>
    </Flex>
  </Flex>
);
