import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Spinner } from "../../../../../complib/feedback";
import { Flexbox } from "../../../../../complib/layouts";
import { Heading } from "../../../../../complib/text";

export const RegisterProcessing = () => {
  const theme = useTheme();
  const { t } = useTranslation();

  return (
    <Flexbox flexDirection={"column"} justifyContent={"center"} alignItems={"center"} gap={theme.tyle.spacing.xl}>
      <Heading as={"h2"}>{t("forms.register.processing")}</Heading>
      <Spinner />
    </Flexbox>
  );
};
