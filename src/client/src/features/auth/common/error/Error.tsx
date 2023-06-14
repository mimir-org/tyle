import { FormErrorBanner } from "complib/form";
import { Text } from "complib/text";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";

interface ErrorProps {
  children?: string;
}

/**
 * Component for catch all error messages in auth forms.
 * Postfixes the error message with a contact support link.
 *
 * @param children
 * @constructor
 */
export const Error = ({ children }: ErrorProps) => {
  const theme = useTheme();
  const { t } = useTranslation("auth");

  return (
    <FormErrorBanner>
      {children}
      <Text as={"a"} href={`mailto:${t("support.email")}`} color={theme.tyle.color.sys.error.on}>
        {t("support.text")}
      </Text>
    </FormErrorBanner>
  );
};
