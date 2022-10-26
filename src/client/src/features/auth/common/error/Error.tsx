import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormErrorBanner } from "../../../../complib/form";
import { Text } from "../../../../complib/text";

interface ErrorProps {
  children: string;
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
  const { t } = useTranslation();

  return (
    <FormErrorBanner>
      {children}
      <Text as={"a"} href={`mailto:${t("common.support.email")}`} color={theme.tyle.color.sys.error.on}>
        {t("common.support.text")}
      </Text>
    </FormErrorBanner>
  );
};
