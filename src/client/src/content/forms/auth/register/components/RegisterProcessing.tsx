import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Spinner } from "../../../../../complib/feedback";
import { Flexbox } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";

export const RegisterProcessing = () => {
  const theme = useTheme();
  const { t } = useTranslation();

  return (
    <Flexbox
      flex={1}
      flexDirection={"column"}
      justifyContent={"center"}
      alignItems={"center"}
      gap={theme.tyle.spacing.xl}
    >
      <Text variant={"title-medium"}>{t("register.processing")}</Text>
      <Spinner />
    </Flexbox>
  );
};
