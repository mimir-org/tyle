import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Divider } from "../../../../../complib/data-display";
import { Flexbox } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";
import { RegisterFinalizeLink, RegisterQrImage } from "./RegisterFinalize.styled";

interface Props {
  qrCodeBase64?: string;
}

export const RegisterFinalize = ({ qrCodeBase64 }: Props) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "forms.register.finalize" });

  return (
    <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
      <Flexbox as={"section"} flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        <Text variant={"display-small"}>{t("verification")}</Text>
        <Text>{t("verificationDescription")}</Text>
      </Flexbox>
      <Divider />
      <Flexbox as={"section"} flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        <Text variant={"display-small"}>{t("mfa")}</Text>
        <Text>{t("mfaDescription")}</Text>
        <RegisterQrImage size={300} src={qrCodeBase64} alt="" />
        <RegisterFinalizeLink to="/">{t("finish")}</RegisterFinalizeLink>
      </Flexbox>
    </Flexbox>
  );
};
