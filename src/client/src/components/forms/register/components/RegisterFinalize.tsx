import { TextResources } from "../../../../assets/text";
import { Flex } from "../../../../complib/layouts";
import { RegisterQrImage, RegisterFinalizeLink } from "./RegisterFinalize.styled";
import { Divider } from "../../../../complib/data-display";
import { THEME } from "../../../../complib/core";
import { Heading, Text } from "../../../../complib/text";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => (
  <Flex flexDirection={"column"} gap={"var(--spacing-xl)"}>
    <Flex as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <Heading>{TextResources.REGISTER_FINALIZE_VERIFICATION}</Heading>
      <Text>{TextResources.REGISTER_FINALIZE_VERIFICATION_DESCRIPTION}</Text>
    </Flex>
    <Divider />
    <Flex as={"section"} flexDirection={"column"} gap={THEME.SPACING.XL}>
      <Heading>{TextResources.REGISTER_FINALIZE_MFA}</Heading>
      <Text>{TextResources.REGISTER_FINALIZE_MFA_DESCRIPTION}</Text>
      <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
      <RegisterFinalizeLink to="/">{TextResources.REGISTER_FINALIZE_FINISH_LINK}</RegisterFinalizeLink>
    </Flex>
  </Flex>
);
