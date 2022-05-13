import { TextResources } from "../../../../assets/text";
import { Flexbox } from "../../../../complib/layouts";
import { RegisterFinalizeLink, RegisterQrImage } from "./RegisterFinalize.styled";
import { Divider } from "../../../../complib/data-display";
import { Text } from "../../../../complib/text";
import { useTheme } from "styled-components";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => {
  const theme = useTheme();

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
      <Flexbox as={"section"} flexDirection={"column"} gap={theme.tyle.spacing.medium}>
        <Text variant={"display-small"}>{TextResources.REGISTER_FINALIZE_VERIFICATION}</Text>
        <Text>{TextResources.REGISTER_FINALIZE_VERIFICATION_DESCRIPTION}</Text>
      </Flexbox>
      <Divider />
      <Flexbox as={"section"} flexDirection={"column"} gap={theme.tyle.spacing.medium}>
        <Text variant={"display-small"}>{TextResources.REGISTER_FINALIZE_MFA}</Text>
        <Text>{TextResources.REGISTER_FINALIZE_MFA_DESCRIPTION}</Text>
        <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
        <RegisterFinalizeLink to="/">{TextResources.REGISTER_FINALIZE_FINISH_LINK}</RegisterFinalizeLink>
      </Flexbox>
    </Flexbox>
  );
};
