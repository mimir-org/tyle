import { useTheme } from "styled-components";
import { Button } from "../../../complib/buttons";
import { Box } from "../../../complib/layouts";
import { Heading, Text } from "../../../complib/text";
import { PlainLink } from "../../utils/PlainLink";

interface NotFoundProps {
  title: string;
  subtitle: string;
  status: string;
  linkText: string;
  linkPath: string;
}

/**
 * Simple not found component offering navigation via link/button
 *
 * @param title
 * @param subtitle
 * @param status
 * @param linkText
 * @param linkPath
 * @constructor
 */
export const NotFound = ({ title, subtitle, status, linkText, linkPath }: NotFoundProps) => {
  const theme = useTheme();

  return (
    <Box display={"flex"} flexWrap={"wrap"} alignItems={"center"} m={"0 auto"} p={theme.tyle.spacing.xxxl}>
      <Box display={"flex"} flexDirection={"column"} gap={theme.tyle.spacing.xxxl} maxWidth={"60ch"}>
        <Heading variant={"display-large"} fontWeight={theme.tyle.typography.ref.typeface.weights.bold}>
          {title}
        </Heading>
        <Heading as={"h2"} variant={"display-medium"}>
          {subtitle}
        </Heading>
        <Text variant={"title-medium"}>{status}</Text>
        <PlainLink tabIndex={-1} to={linkPath}>
          <Button tabIndex={0} as={"span"}>
            {linkText}
          </Button>
        </PlainLink>
      </Box>
    </Box>
  );
};
