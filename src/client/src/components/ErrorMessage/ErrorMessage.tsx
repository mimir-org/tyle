import Box from "components/Box";
import Button from "components/Button";
import Heading from "components/Heading";
import PlainLink from "components/PlainLink";
import Text from "components/Text";
import { useTheme } from "styled-components";

interface NotFoundProps {
  title: string;
  subtitle: string;
  status: string;
  linkText: string;
  linkPath: string;
}

/**
 * Simple error message component offering navigation via link/button
 *
 * @param title
 * @param subtitle
 * @param status
 * @param linkText
 * @param linkPath
 * @constructor
 */
const ErrorMessage = ({ title, subtitle, status, linkText, linkPath }: NotFoundProps) => {
  const theme = useTheme();

  return (
    <Box
      display={"flex"}
      justifyContent={"center"}
      alignItems={"center"}
      width={"100%"}
      height={"100%"}
      spacing={{ p: theme.tyle.spacing.xxxl }}
    >
      <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xxxl} maxWidth={"60ch"}>
        <Heading variant={"display-large"} fontWeight={theme.tyle.typography.typeface.weights.bold}>
          {title}
        </Heading>
        <Heading as={"h2"} variant={"display-medium"}>
          {subtitle}
        </Heading>
        <Text variant={"title-medium"}>{status}</Text>
        <PlainLink tabIndex={-1} to={linkPath}>
          <Button
            tabIndex={0}
            as={"span"}
            variant={"text"}
            textVariant={"label-large"}
            spacing={{
              p: theme.tyle.spacing.s,
            }}
          >
            {linkText}
          </Button>
        </PlainLink>
      </Box>
    </Box>
  );
};

export default ErrorMessage;
