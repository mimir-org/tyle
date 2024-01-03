import { Spinner } from "@mimirorg/component-library";
import Flexbox from "components/Flexbox";
import Text from "components/Text";
import { useTheme } from "styled-components";

interface RegisterProcessingProps {
  children?: string;
}

const Processing = ({ children }: RegisterProcessingProps) => {
  const theme = useTheme();

  return (
    <Flexbox
      flex={1}
      flexDirection={"column"}
      justifyContent={"center"}
      alignItems={"center"}
      gap={theme.tyle.spacing.xl}
    >
      <Text variant={"title-medium"}>{children}</Text>
      <Spinner disabled={false} />
    </Flexbox>
  );
};

export default Processing;
