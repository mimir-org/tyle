import { FormErrorBanner, Text } from "@mimirorg/component-library";
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
const Error = ({ children }: ErrorProps) => {
  const theme = useTheme();

  return (
    <FormErrorBanner>
      {children}
      <Text as={"a"} href="mailto:orgmimir@gmail.com" color={theme.tyle.color.error.on}>
        contact support.
      </Text>
    </FormErrorBanner>
  );
};

export default Error;
